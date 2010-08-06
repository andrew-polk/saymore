
namespace SayMore.Model.Files
{
	/// ----------------------------------------------------------------------------------------
	/// <summary>
	/// Person and Session are different from other files. This subclass allows us to
	/// account for those differences
	/// </summary>
	/// ----------------------------------------------------------------------------------------
	public class ProjectElementComponentFile : ComponentFile
	{
		public new delegate ProjectElementComponentFile Factory(ProjectElement parentElement,
			FileType fileType, FileSerializer fileSerializer, string rootElementName);

		private readonly ProjectElement _parentElement;

		/// ------------------------------------------------------------------------------------
		public ProjectElementComponentFile(ProjectElement parentElement,
			FileType fileType, FileSerializer fileSerializer, string rootElementName, FieldUpdater fieldUpdater)
			: base(parentElement.SettingsFilePath, fileType, rootElementName, fileSerializer, fieldUpdater)
		{
			_parentElement = parentElement;
			PathToAnnotatedFile = parentElement.SettingsFilePath;//same thing, there isn't a pair of files for session/person
			InitializeFileInfo();
		}

		/// ------------------------------------------------------------------------------------
		public override string GetStringValue(string key, string defaultValue)
		{
			return (key != "id" ? base.GetStringValue(key, defaultValue) : _parentElement.Id);
		}

		/// ------------------------------------------------------------------------------------
		public override string TryChangeChangeId(string newId, out string failureMessage)
		{
			failureMessage = null;

			if (_parentElement.Id != newId)
			{
				var oldId = _parentElement.Id;
				if (_parentElement.TryChangeIdAndSave(newId, out failureMessage))
				{
					LoadFileSizeAndDateModified();
					InvokeIdChanged(oldId, newId);
				}
			}

			// Send back whatever the id is now, whether or not renaming succeeded.
			return _parentElement.Id;
		}

		/// ------------------------------------------------------------------------------------
		public override void Save(string path)
		{
			base.Save(path);
			PathToAnnotatedFile = _parentElement.SettingsFilePath;
		}

		/// ------------------------------------------------------------------------------------
		public override void HandleDoubleClick()
		{
			//don't do anything
		}
	}
}
