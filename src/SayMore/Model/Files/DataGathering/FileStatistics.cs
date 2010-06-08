using System;
using System.IO;
using System.Threading;

namespace SayMore.Model.Files.DataGathering
{
	/// <summary>
	/// A FileStatistics is created for a single file.  It then provides information
	/// about the file, such as the duration, if it is a media file.
	/// </summary>
	public class FileStatistics
	{
		public long LengthInBytes;
		public TimeSpan Duration;

		public FileStatistics(string path)
		{
			Path = path;
			LengthInBytes = new FileInfo(path).Length;
			Duration = GetDuration();
		}

		private TimeSpan GetDuration()
		{
			// TODO: What should we do if DirectX throws an exception (e.g. the path is really
			// not a valid audio or video stream)? For now, just ignore exceptions.
			try
			{
				if (ComponentRole.GetIsAudio(Path))
				{
					using (var audio = new Microsoft.DirectX.AudioVideoPlayback.Audio(Path))
					{
						return TimeSpan.FromSeconds((int) audio.Duration);
					}
				}
				else if (ComponentRole.GetIsVideo(Path))
				{
					using (var video = new Microsoft.DirectX.AudioVideoPlayback.Video(Path))
					{
						return TimeSpan.FromSeconds((int) video.Duration);
					}

				}
			}
			catch(ThreadAbortException)
			{
				//fine, just return
			}
			catch(Exception e)
			{
				Palaso.Reporting.ErrorReport.NotifyUserOfProblem(e,"Could not get duration of "+Path);
			}


			return new TimeSpan();
		}

		public string Path
		{
			get ;
			set;
		}
	}
}