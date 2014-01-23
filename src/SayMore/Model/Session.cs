using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using L10NSharp;
using Palaso.Reporting;
using Palaso.UI.WindowsForms.ClearShare;
using SIL.Archiving.IMDI;
using SayMore.Model.Fields;
using SayMore.Model.Files;
using SayMore.Properties;
using SIL.Archiving;

namespace SayMore.Model
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// A session is recorded, documented, transcribed, etc.
	/// Each session is represented on disk as a single folder, with 1 or more files
	/// related to that session.  The one file it will always have is some meta data about
	/// the session.
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public class Session : ProjectElement, IIMDIArchivable
	{
// ReSharper disable once InconsistentNaming
		public static string kFolderName = "Sessions";

		public enum Status
		{
			Incoming = 0,
			In_Progress,
			Finished,
			Skipped
		}

		//autofac uses this
		public delegate Session Factory(string parentElementFolder, string id);

		private readonly PersonInformant _personInformant;

		[Obsolete("For Mocking Only")]
		public Session() { }

		/// ------------------------------------------------------------------------------------
		public Session(string parentElementFolder, string id,
			Action<ProjectElement, string, string> idChangedNotificationReceiver,
			SessionFileType sessionFileType,
			Func<ProjectElement, string, ComponentFile> componentFileFactory,
			XmlFileSerializer xmlFileSerializer,
			ProjectElementComponentFile.Factory prjElementComponentFileFactory,
			IEnumerable<ComponentRole> componentRoles,
			PersonInformant personInformant)
			: base(parentElementFolder, id, idChangedNotificationReceiver, sessionFileType,
				componentFileFactory, xmlFileSerializer, prjElementComponentFileFactory, componentRoles)
		{
			_personInformant = personInformant;

// ReSharper disable DoNotCallOverridableMethodsInConstructor
			if (string.IsNullOrEmpty(MetaDataFile.GetStringValue(SessionFileType.kGenreFieldName, null)))

			{
				string failureMsg;
				MetaDataFile.SetValue(SessionFileType.kGenreFieldName, GenreDefinition.UnknownType.Name, out failureMsg);
				if (failureMsg == null)
					MetaDataFile.Save();
			}
// ReSharper restore DoNotCallOverridableMethodsInConstructor
			if (_personInformant != null)
				_personInformant.PersonNameChanged += HandlePersonsNameChanged;
		}

		#region Properties
		/// ------------------------------------------------------------------------------------
		public string Title
		{
			get { return MetaDataFile.GetStringValue(SessionFileType.kTitleFieldName, null) ?? Id; }
		}

		/// ------------------------------------------------------------------------------------
		protected override string ExtensionWithoutPeriod
		{
			get { return ExtensionWithoutPeriodStatic; }
		}

		/// ------------------------------------------------------------------------------------
		public override string RootElementName
		{
			get { return "Session"; }
		}

		/// ------------------------------------------------------------------------------------
		protected static string ExtensionWithoutPeriodStatic
		{
			get { return Settings.Default.SessionFileExtension.TrimStart('.'); }
		}

		/// ------------------------------------------------------------------------------------
		public override string DefaultElementNamePrefix
		{
			get { return LocalizationManager.GetString("SessionsView.Miscellaneous.NewSessionNamePrefix", "New Session"); }
		}

		/// ------------------------------------------------------------------------------------
		protected override string NoIdSaveFailureMessage
		{
			get { return LocalizationManager.GetString("SessionsView.Miscellaneous.NoIdSaveFailureMessage", "You must specify a session id."); }
		}

		/// ------------------------------------------------------------------------------------
		protected override string AlreadyExistsSaveFailureMessage
		{
			get
			{
				return LocalizationManager.GetString("SessionsView.Miscellaneous.SessionAlreadyExistsSaveFailureMessage",
					"Could not rename from {0} to {1} because there is already a session by that name.");
			}
		}

		/// ------------------------------------------------------------------------------------
		public override string DefaultStatusValue
		{
			get { return Status.Incoming.ToString(); }
		}

		#endregion

		/// ------------------------------------------------------------------------------------
		public static string GetStatusAsHumanReadableString(string statusAsText)
		{
			return LocalizationManager.GetDynamicString("SayMore", "SessionsView.SessionStatus." +
				statusAsText.Replace("_", string.Empty), statusAsText.Replace('_', ' '));
		}

		/// ------------------------------------------------------------------------------------
		public static string GetStatusAsEnumParsableString(string statusAsText)
		{
			return statusAsText.Replace(' ', '_');
		}

		/// ------------------------------------------------------------------------------------
		public static string GetLocalizedStatus(string statusAsText)
		{
			statusAsText = GetStatusAsEnumParsableString(statusAsText);

			if (statusAsText == Status.Incoming.ToString())
				return LocalizationManager.GetString("SessionsView.SessionStatus.Incoming", "Incoming");

			if (statusAsText == Status.In_Progress.ToString())
				return LocalizationManager.GetString("SessionsView.SessionStatus.InProgress", "In Progress");

			if (statusAsText == Status.Finished.ToString())
				return LocalizationManager.GetString("SessionsView.SessionStatus.Finished", "Finished");

			return LocalizationManager.GetString("SessionsView.SessionStatus.Skipped", "Skipped");
		}

		/// ------------------------------------------------------------------------------------
		public override IEnumerable<ComponentRole> GetCompletedStages()
		{
			return GetCompletedStages(true);
		}

		/// ------------------------------------------------------------------------------------
		public override IEnumerable<ComponentRole> GetCompletedStages(
			bool modifyComputedListWithUserOverrides)
		{
			var list = base.GetCompletedStages(modifyComputedListWithUserOverrides).ToList();

			if (GetShouldReportHaveConsent())
			   list.Insert(0, ComponentRoles.First(r => r.Id == ComponentRole.kConsentComponentRoleId));

			return (modifyComputedListWithUserOverrides ?
				GetCompletedStagesModifedByUserOverrides(list) : list);
		}

		/// ------------------------------------------------------------------------------------
		private bool GetShouldReportHaveConsent()
		{
			var allParticipants = MetaDataFile.GetStringValue(SessionFileType.kParticipantsFieldName, string.Empty);
			var personNames = FieldInstance.GetMultipleValuesFromText(allParticipants).ToArray();
			bool allParticipantsHaveConsent = personNames.Length > 0;

			return personNames.All(name => _personInformant.GetHasInformedConsent(name)) &&
				allParticipantsHaveConsent;
		}

		/// ------------------------------------------------------------------------------------
		public TimeSpan GetTotalDurationOfSourceMedia()
		{
			var totalDuration = TimeSpan.Zero;

			foreach (var duration in GetComponentFiles().Where(file =>
				file.GetAssignedRoles().Any(r =>
				r.Id == ComponentRole.kSourceComponentRoleId))
				.Select(f => f.DurationSeconds))
			{
				totalDuration += duration;
			}

			return totalDuration;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// We get this message from the person informant when a person's name has changed.
		/// When that happens, we need to make sure we update the participant field in case
		/// it contains a name that changed.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		private void HandlePersonsNameChanged(object sender, ElementIdChangedArgs e)
		{
			var allParticipants = GetAllParticipants();
			var newNames = allParticipants.Select(name => (name == e.OldId ? e.NewId : name));

			string failureMessage;
			MetaDataFile.SetStringValue(SessionFileType.kParticipantsFieldName,
				FieldInstance.GetTextFromMultipleValues(newNames), out failureMessage);

			if (failureMessage != null)
				ErrorReport.NotifyUserOfProblem(failureMessage);
		}

		/// ------------------------------------------------------------------------------------
		public virtual IEnumerable<string> GetAllParticipants()
		{
			var allParticipants = MetaDataFile.GetStringValue(SessionFileType.kParticipantsFieldName, string.Empty);
			return FieldInstance.GetMultipleValuesFromText(allParticipants);
		}

		/// ------------------------------------------------------------------------------------
		public IEnumerable<Person> GetAllPersonsInSession()
		{
			return GetAllParticipants().Select(n => _personInformant.GetPersonByName(n)).Where(p => p != null);
		}

		#region Archiving
		/// ------------------------------------------------------------------------------------
		public void ArchiveUsingRAMP()
		{
			var model = new RampArchivingDlgViewModel(Application.ProductName, Title, Id,
				ArchiveInfoDetails, SetFilesToArchive, GetFileDescription);

			model.FileCopyOverride = ArchivingHelper.FileCopySpecialHandler;
			model.AppSpecificFilenameNormalization = CustomFilenameNormalization;
			model.OverrideDisplayInitialSummary = fileLists => DisplayInitialArchiveSummary(fileLists, model);
			model.HandleNonFatalError = (exception, s) => ErrorReport.NotifyUserOfProblem(exception, s);

			SetAdditionalMetsData(model);

			using (var dlg = new ArchivingDlg(model, ApplicationContainer.kSayMoreLocalizationId,
				Program.DialogFont, Settings.Default.ArchivingDialog))
			{
				dlg.ShowDialog();
				Settings.Default.ArchivingDialog = dlg.FormSettings;
			}
		}

		/// ------------------------------------------------------------------------------------
		public void ArchiveUsingIMDI()
		{
			ArchivingHelper.ArchiveUsingIMDI(this);
		}

		/// ------------------------------------------------------------------------------------
		public string ArchiveInfoDetails
		{
			get
			{
				return LocalizationManager.GetString("DialogBoxes.ArchivingDlg.ArchivingInfoDetails",
					"The archive package will include all required files and data related to a session and its contributors.",
					"This sentence is inserted as a parameter in DialogBoxes.ArchivingDlg.xxxxOverviewText");
			}
		}

		/// ------------------------------------------------------------------------------------
		public IEnumerable<string> GetSessionFilesToArchive(Type typeOfArchive)
		{
			var filesInDir = Directory.GetFiles(FolderPath);
			return filesInDir.Where(f => ArchivingHelper.IncludeFileInArchive(f, typeOfArchive, Settings.Default.SessionFileExtension));
		}

		/// ------------------------------------------------------------------------------------
		public string AddingSessionFilesProgressMsg
		{
			get
			{
				return string.Format(LocalizationManager.GetString("DialogBoxes.ArchivingDlg.AddingSessionFilesProgressMsg",
					"Adding Files for Session '{0}'"), Title);
			}
		}

		/// ------------------------------------------------------------------------------------
		public IDictionary<string, IEnumerable<string>> GetParticipantFilesToArchive(Type typeOfArchive)
		{
			Dictionary<string, IEnumerable<string>> d = new Dictionary<string, IEnumerable<string>>();

			foreach (var person in GetAllParticipants().Select(n => _personInformant.GetPersonByName(n)).Where(p => p != null))
			{
				var filesInDir = Directory.GetFiles(person.FolderPath);
				d[person.Id] = filesInDir.Where(f => ArchivingHelper.IncludeFileInArchive(f, typeOfArchive, Settings.Default.PersonFileExtension));
			}
			return d;
		}

		public void InitializeModel(IMDIArchivingDlgViewModel model)
		{
			model.OverrideDisplayInitialSummary = fileLists => DisplayInitialArchiveSummary(fileLists, model);
			ArchivingHelper.SetIMDIMetadataToArchive(this, model);
		}

		/// ------------------------------------------------------------------------------------
		public void SetFilesToArchive(ArchivingDlgViewModel model)
		{
			model.AddFileGroup(string.Empty, GetSessionFilesToArchive(model.GetType()), AddingSessionFilesProgressMsg);

			var fmt = LocalizationManager.GetString("DialogBoxes.ArchivingDlg.AddingContributorFilesProgressMsg", "Adding Files for Contributor '{0}'");

			foreach (var person in GetParticipantFilesToArchive(model.GetType()))
				model.AddFileGroup(person.Key, person.Value, string.Format(fmt, person.Key));
		}

		/// ------------------------------------------------------------------------------------
		private void DisplayInitialArchiveSummary(IDictionary<string, Tuple<IEnumerable<string>, string>> fileLists, ArchivingDlgViewModel model)
		{
			if (fileLists.Count > 1)
			{
				model.DisplayMessage(LocalizationManager.GetString("DialogBoxes.ArchivingDlg.PrearchivingStatusMsg1",
					"The following session and contributor files will be added to your archive."), ArchivingDlgViewModel.MessageType.Normal);
			}
			else
			{
				model.DisplayMessage(LocalizationManager.GetString("DialogBoxes.ArchivingDlg.NoContributorsForSessionMsg",
					"There are no contributors for this session."), ArchivingDlgViewModel.MessageType.Warning);

				model.DisplayMessage(LocalizationManager.GetString("DialogBoxes.ArchivingDlg.PrearchivingStatusMsg2",
					"The following session files will be added to your archive."), ArchivingDlgViewModel.MessageType.Progress);
			}

			var fmt = LocalizationManager.GetString("DialogBoxes.ArchivingDlg.ArchivingProgressMsg", "     {0}: {1}",
				"The first parameter is 'Session' or 'Contributor'. The second parameter is the session or contributor name.");

			foreach (var kvp in fileLists)
			{
				var element = (kvp.Key == string.Empty ?
					LocalizationManager.GetString("DialogBoxes.ArchivingDlg.SessionElementName", "Session") :
					LocalizationManager.GetString("DialogBoxes.ArchivingDlg.ContributorElementName", "Contributor"));

				model.DisplayMessage(string.Format(fmt, element, (kvp.Key == string.Empty ? Title : kvp.Key)),
					ArchivingDlgViewModel.MessageType.Progress);

				foreach (var file in kvp.Value.Item1)
					model.DisplayMessage(Path.GetFileName(file), ArchivingDlgViewModel.MessageType.Bullet);
			}
		}



		/// ------------------------------------------------------------------------------------
		protected override IEnumerable<KeyValuePair<string, string>> GetFilesToCopy(IEnumerable<string> validComponentFilesToCopy)
		{
			if (GetCompletedStages(true).Any(s => s.Id == ComponentRole.kSourceComponentRoleId))
			{
				foreach (var kvp in base.GetFilesToCopy(validComponentFilesToCopy))
					yield return kvp;
			}
			else
			{
				var sourceRole = ComponentRoles.First(r => r.Id == ComponentRole.kSourceComponentRoleId);
				bool foundSourceFile = false;
				foreach (var srcFile in validComponentFilesToCopy)
				{
					var destFile = (foundSourceFile || !sourceRole.IsPotential(srcFile)) ? GetDestinationFilename(srcFile) :
						Path.Combine(FolderPath, sourceRole.GetCanoncialName(Path.GetFileNameWithoutExtension(srcFile), Path.GetFileName(srcFile)));
					if (!File.Exists(destFile))
					{
						yield return new KeyValuePair<string, string>(srcFile, destFile);
						foundSourceFile = true;
					}
				}
			}
		}

		/// ------------------------------------------------------------------------------------
		private void SetAdditionalMetsData(RampArchivingDlgViewModel model)
		{
			model.SetScholarlyWorkType(ScholarlyWorkType.PrimaryData);
			model.SetDomains(SilDomain.Ling_LanguageDocumentation);

			var value = MetaDataFile.GetStringValue(SessionFileType.kDateFieldName, null);
			if (!string.IsNullOrEmpty(value))
				model.SetCreationDate(value);

			// Return the session's note as the abstract portion of the package's description.
			value = MetaDataFile.GetStringValue(SessionFileType.kSynopsisFieldName, null);
			if (!string.IsNullOrEmpty(value))
				model.SetAbstract(value, string.Empty);

			// Set contributors
			var contributions = MetaDataFile.GetValue("contributions", null) as ContributionCollection;
			if (contributions != null && contributions.Count > 0)
				model.SetContributors(contributions);

			// Return total duration of source audio/video recordings.
			TimeSpan totalDuration = GetTotalDurationOfSourceMedia();
			if (totalDuration.Ticks > 0)
				model.SetAudioVideoExtent(string.Format("Total Length of Source Recordings: {0}", totalDuration.ToString()));

			//model.SetSoftwareRequirements("SayMore");
		}

		/// ------------------------------------------------------------------------------------
		private string GetFileDescription(string key, string file)
		{
			var description = (key == string.Empty ? "SayMore Session File" : "SayMore Contributor File");

			if (file.ToLower().EndsWith(Settings.Default.SessionFileExtension))
				description = "SayMore Session Metadata (XML)";
			else if (file.ToLower().EndsWith(Settings.Default.PersonFileExtension))
				description = "SayMore Contributor Metadata (XML)";
			else if (file.ToLower().EndsWith(Settings.Default.MetadataFileExtension))
				description = "SayMore File Metadata (XML)";

			return description;
		}

		/// ------------------------------------------------------------------------------------
		private void CustomFilenameNormalization(string key, string file, StringBuilder bldr)
		{
			if (key != string.Empty)
				bldr.Insert(0, "__Contributors__");
		}
		#endregion

		/// ------------------------------------------------------------------------------------
		public override void Load()
		{
			base.Load();

			StageCompletedControlValues = ComponentRoles.ToDictionary(role => role.Id,
				role => (StageCompleteType)Enum.Parse(typeof(StageCompleteType),
					MetaDataFile.GetValue(SessionFileType.kStageFieldPrefix + role.Id, StageCompleteType.Auto.ToString()) as string));
		}

		/// ------------------------------------------------------------------------------------
		public string GetProjectName()
		{
			// Sessions directory
			var dir = ParentFolderPath;

			// Find the project file
			var file = Directory.GetParent(dir).GetFiles("*" + Settings.Default.ProjectFileExtension).FirstOrDefault();

			// The project name is the same as the project file name
			return file != null ? Path.GetFileNameWithoutExtension(file.Name) : null;
		}
	}

	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// Comparer class to compare two string representations of session status'.
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public class SessionStatusComparer : IComparer<string>
	{
		public int Compare(string x, string y)
		{
			if (x == y)
				return 0;

			if (x == null)
				return 1;

			if (y == null)
				return -1;

			Session.Status xStatus, yStatus;

			if (!Enum.TryParse(Session.GetStatusAsEnumParsableString(x), out xStatus))
				return 1;

			if (!Enum.TryParse(Session.GetStatusAsEnumParsableString(y), out yStatus))
				return -1;

			return (int)xStatus - (int)yStatus;
		}
	}
}
