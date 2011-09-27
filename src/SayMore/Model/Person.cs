using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SayMore.Model.Files;
using SayMore.Properties;

namespace SayMore.Model
{
	/// ----------------------------------------------------------------------------------------
	public class Person : ProjectElement
	{
		public enum Status
		{
			Incoming = 0,
			Finished,
		}

		/// ------------------------------------------------------------------------------------
		public Person(string parentElementFolder, string id,
			Action<ProjectElement, string, string> idChangedNotificationReceiver,
			PersonFileType personFileType, Func<ProjectElement, string, ComponentFile> componentFileFactory,
			FileSerializer fileSerializer, ProjectElementComponentFile.Factory prjElementComponentFileFactory)
			: base(parentElementFolder, id, idChangedNotificationReceiver, personFileType,
				componentFileFactory, fileSerializer, prjElementComponentFileFactory)
		{
			if (string.IsNullOrEmpty(MetaDataFile.GetStringValue("status", null)))
			{
				// REVIEW: Should we report anything if there's an error message returned?
				string errMsg;
				MetaDataFile.SetStringValue("status", Status.Incoming.ToString(), out errMsg);
				Save();
			}
		}

		[Obsolete("For Mocking Only")]
		public Person(){}

		/// ------------------------------------------------------------------------------------
		public override string RootElementName
		{
			get { return "Person"; }
		}

		/// ------------------------------------------------------------------------------------
		protected override string ExtensionWithoutPeriod
		{
			get { return Settings.Default.PersonFileExtension.TrimStart('.'); }
		}

		/// ------------------------------------------------------------------------------------
		public override string DefaultElementNamePrefix
		{
			get { return Program.GetString("Model.MiscPeopleViewMessages.NewPersonNamePrefix", "New Person"); }
		}

		/// ------------------------------------------------------------------------------------
		protected override string NoIdSaveFailureMessage
		{
			get { return Program.GetString("Model.MiscPeopleViewMessages.NoIdSaveFailureMessage", "You must specify a name."); }
		}

		/// ------------------------------------------------------------------------------------
		protected override string AlreadyExistsSaveFailureMessage
		{
			get
			{
				return Program.GetString("Model.MiscPeopleViewMessages.AlreadyExistsSaveFailureMessage",
					"Could not rename from {0} to {1} because there is already a person by that name.");
			}
		}

		/// ------------------------------------------------------------------------------------
		public override string DefaultStatusValue
		{
			get { return Status.Incoming.ToString(); }
		}

		/// ------------------------------------------------------------------------------------
		public Image GetInformedConsentImage()
		{
			var componentFile = GetInformedConsentComponentFile();
			if (componentFile == null)
				return Resources.NoInformedConsent;

			if (componentFile.FileType.IsAudio)
				return Resources.AudioInformedConsent;

			if (componentFile.FileType.IsVideo)
				return Resources.VideoInformedConsent;

			return Resources.WrittenInformedConsent;
		}

		/// ------------------------------------------------------------------------------------
		public int GetInformedConsentSortKey()
		{
			var componentFile = GetInformedConsentComponentFile();

			if (componentFile == null)
				return 0;

			if (componentFile.FileType.IsAudio)
				return 1;

			if (componentFile.FileType.IsVideo)
				return 2;

			return 3;
		}

		/// ------------------------------------------------------------------------------------
		public string GetToolTipForInformedConsentType()
		{
			var componentFile = GetInformedConsentComponentFile();

			if (componentFile == null)
				return Program.GetString("Model.MiscPeopleViewMessages.InformedConsent-None", "No Informed Consent");

			if (componentFile.FileType.IsAudio)
				return Program.GetString("Model.MiscPeopleViewMessages.InformedConsent-Audio", "Informed Consent is Audio File");

			if (componentFile.FileType.IsVideo)
				return Program.GetString("Model.MiscPeopleViewMessages.InformedConsent-Video", "Informed Consent is Video File");

			return Program.GetString("Model.MiscPeopleViewMessages.InformedConsent-Written", "Informed Consent is Written");
		}

		/// ------------------------------------------------------------------------------------
		public virtual ComponentFile GetInformedConsentComponentFile()
		{
			foreach (var component in GetComponentFiles())
			{
				if (component.GetAssignedRoles().FirstOrDefault(x => x.Id == "consent") != null)
					return component;
			}

			return null;
		}

		#region Static methods
		/// ------------------------------------------------------------------------------------
		public static IEnumerable<string> GetStatusNames()
		{
			return Enum.GetNames(typeof(Status)).Select(x => x.ToString().Replace('_', ' '));
		}

		#endregion
	}
}
