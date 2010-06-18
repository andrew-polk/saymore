using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SayMore.Model.Files.DataGathering
{
	/// <summary>
	/// Gets all the metadata settings found in the whole project,
	/// for the purpose of automatically making presets
	/// </summary>
	public class PresetGatherer : BackgroundFileProcessor<PresetData>
	{
		public delegate PresetGatherer Factory(string rootDirectoryPath);

		public PresetGatherer(string rootDirectoryPath, IEnumerable<FileType> allFileTypes,  PresetData.Factory presetFactory)
			:	base(rootDirectoryPath,
					allFileTypes.Where(t=>t.IsAudioOrVideo),
					path=>presetFactory(path))
		{
		}

		protected override bool GetDoIncludeFile(string path)
		{
			if (_typesOfFilesToProcess.Any(t => t.IsMatch(path))
				|| _typesOfFilesToProcess.Any(t => t.IsMatch(path.Replace(".meta", ""))))
			{
				var p = GetActualPath(path);
				return File.Exists(p);
			}
			return false;
		}

		/// <summary>
		/// subclass can override this to, for example, use the path of a sidecar file
		/// </summary>
		protected override string GetActualPath(string path)
		{
			if(!path.Contains(".meta"))
			{
				return path + ".meta";
			}
			return path;
		}


		public IEnumerable<KeyValuePair<string, Dictionary<string, string>>> GetPresets()
		{
			var suggestor = new UniqueCombinationsFinder(
				from d in _fileToDataDictionary.Values
				select d.Dictionary);
			var suggestions= suggestor.GetSuggestions();
			if(suggestions.Count()==0)
			{
				yield return new KeyValuePair<string, Dictionary<string, string>>("No presets yet",new Dictionary<string, string>());
			}
			else
			{
				foreach (var keyValuePair in suggestions)
				{
					yield return keyValuePair;
				}
			}
		}
	}

	/// <summary>
	/// The preset which would be derived from this file
	/// </summary>
	public class PresetData
	{
		public delegate PresetData FactoryForTest(string path, Func<string, Dictionary<string, string>> pathToDictionaryFunction);
		public delegate PresetData Factory(string path);
		public Dictionary<string, string> Dictionary { get; private set; }

		/// <summary>
		/// Notice, it's up to the caller to give us files which make sense.
		/// E.g., media files have sidecars with data that makes sense as a presets.
		/// </summary>
		public PresetData(string path, ComponentFile.Factory componentFileFactory)
		{
			//this is feeling a bit hacky... but sometimes we are called with the path to the
			//media file, and sometimes with the metadata file itself (i.e., when it is modified)
			//so for now we just always get to the path of the media file
			var pathToAnnotatedFile = path.Replace(".meta","");

			var f = componentFileFactory(pathToAnnotatedFile);
			Dictionary =  f.MetaDataFieldValues.ToDictionary(field => field.FieldDefinitionKey,
															field => field.Value);
		}


		/// <summary>
		/// for test only... probably was a waste of time
		/// </summary>
		public PresetData(string path, Func<string, Dictionary<string, string>> pathToDictionaryFunction)
		{
			Dictionary = pathToDictionaryFunction(path);
		}
	}
}