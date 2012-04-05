using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using SayMore.Properties;

namespace SayMore.Transcription.Model
{
	/// ----------------------------------------------------------------------------------------
	public class TierCollection : Collection<TierBase>
	{
		/// ------------------------------------------------------------------------------------
		public static TierCollection LoadFromAnnotationFile(string eafFilePath)
		{
			var helper = AnnotationFileHelper.Load(eafFilePath);
			var collection = helper.GetTierCollection();
			collection.AnnotatedMediaFile = helper.GetFullPathToMediaFile();

			return collection;
		}

		/// ------------------------------------------------------------------------------------
		public TierCollection()
		{
		}

		/// ------------------------------------------------------------------------------------
		public TierCollection(string annotatedMediaFile)
		{
			AnnotatedMediaFile = annotatedMediaFile;
			Add(new TimeTier(annotatedMediaFile));
			Add(new TextTier(TextTier.ElanTranscriptionTierId));
			Add(new TextTier(TextTier.ElanTranslationTierId));
		}

		/// ------------------------------------------------------------------------------------
		public string AnnotatedMediaFile { get; private set; }

		/// ------------------------------------------------------------------------------------
		public TierCollection Copy()
		{
			var copy = new TierCollection();

			copy.AnnotatedMediaFile = AnnotatedMediaFile;

			foreach (var tier in this)
				copy.Add(tier.Copy());

			return copy;
		}

		/// ------------------------------------------------------------------------------------
		public void AddTextTierWithEmptySegments(string id)
		{
			var newTier = new TextTier(id);
			Add(newTier);

			for (int i = 0; i < GetTimeTier().Segments.Count; i++)
				newTier.AddSegment(string.Empty);
		}

		/// ------------------------------------------------------------------------------------
		public bool GetDoTimeSegmentsExist()
		{
			return (GetTimeTier() != null && GetTimeTier().Segments.Count > 0);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Removes the segment in each tier at the specified index.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public bool RemoveTierSegments(int index)
		{
			if (Count == 0 || index < 0 || index >= this[0].Segments.Count)
				return false;

			for (int i = 0; i < Count; i++)
				this[i].RemoveSegment(index);

			return true;
		}

		/// ------------------------------------------------------------------------------------
		public BoundaryModificationResult InsertTierSegment(float boundary)
		{
			var timeTier = GetTimeTier();

			if (boundary <= 0f || timeTier == null || timeTier.GetSegmentHavingEndBoundary(boundary) != null)
				return BoundaryModificationResult.SegmentWillBeTooShort;

			var result = timeTier.InsertSegmentBoundary(boundary);
			if (result == BoundaryModificationResult.Success)
			{
				var newSeg = timeTier.GetSegmentHavingEndBoundary(boundary);
				var i = timeTier.GetIndexOfSegment(newSeg);

				foreach (var tier in this.OfType<TextTier>())
					tier.Segments.Insert(i, new Segment(tier, string.Empty));
			}

			return result;
		}

		/// ------------------------------------------------------------------------------------
		public bool HasAdjacentAnnotation(float boundary)
		{
			var timeTier = GetTimeTier();
			if (!timeTier.Segments.Any())
				return false;

			var segment = timeTier.GetSegmentHavingEndBoundary(boundary);
			if (segment == null && boundary > timeTier.Segments.Last().End)
				return false;

			int i = timeTier.GetIndexOfSegment(segment);

			if (Settings.Default.PreventSegmentBoundaryMovingWhereTextAnnotationsAreAdjacent)
			{
				foreach (TextTier textTier in this.OfType<TextTier>())
				{
					var segments = textTier.Segments;
					if (segments != null && segments.Count > i)
					{
						if (!string.IsNullOrEmpty(segments[i].Text) ||
							segments.Count > i + 1 && !string.IsNullOrEmpty(segments[i + 1].Text))
							return true;
					}
				}
			}

			if (File.Exists(segment.GetFullPathToCarefulSpeechFile()) ||
				File.Exists(segment.GetFullPathToOralTranslationFile()))
				return true;

			if (timeTier.Segments.Count > i + 1)
			{
				segment = timeTier.Segments[i + 1];
				return segment.GetHasOralAnnotation();
			}

			return false;
		}

		#region Methods for Saving tiers to EAF file
		/// ------------------------------------------------------------------------------------
		public string Save()
		{
			Debug.Assert(AnnotatedMediaFile != null);
			Debug.Assert(File.Exists(AnnotatedMediaFile));
			return Save(AnnotatedMediaFile);
		}

		/// ------------------------------------------------------------------------------------
		public string Save(string annotatedMediaFile)
		{
			return AnnotationFileHelper.Save(annotatedMediaFile, this);
		}

		#endregion

		#region Methods for getting various tiers
		/// ------------------------------------------------------------------------------------
		public TimeTier GetTimeTier()
		{
			return this.FirstOrDefault(t => t.TierType == TierType.Time) as TimeTier;
		}

		/// ------------------------------------------------------------------------------------
		public TextTier GetTranscriptionTier()
		{
			return this.FirstOrDefault(t => t.TierType == TierType.Transcription) as TextTier;
		}

		/// ------------------------------------------------------------------------------------
		public TextTier GetFreeTranslationTier()
		{
			return this.FirstOrDefault(t => t.TierType == TierType.FreeTranslation) as TextTier;
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Gets all the text tiers other than the transcription tier.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public IEnumerable<TextTier> GetDependentTextTiers()
		{
			return this.OfType<TextTier>().Where(t => t.TierType != TierType.Transcription);
		}

		/// ------------------------------------------------------------------------------------
		/// <summary>
		/// Gets all the text tiers other than the transcription and free translation.
		/// </summary>
		/// ------------------------------------------------------------------------------------
		public IEnumerable<TextTier> GetUserDefinedTextTiers()
		{
			return this.OfType<TextTier>().Where(t => t.TierType != TierType.Transcription &&
				t.TierType != TierType.FreeTranslation);
		}

		#endregion
	}
}
