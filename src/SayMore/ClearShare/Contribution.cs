using System;
using System.Collections.Generic;

namespace SayMore.ClearShare
{
	/// <summary>
	/// Records a single contribution of a single individual to a single "work".  If the person performed another role,
	/// they get another contribution record.
	/// </summary>
	public sealed class Contribution
	{
		/// <summary>
		/// This will sometimes come from a controlled vocabulary of known people, but we don't need to model that here
		/// </summary>
		public string ContributorName { get; set; }

		/// <summary>
		/// The choices for this will come from the owning Work, but we don't model that here.
		/// </summary>
		public License ApprovedLicense{ get; set; }

		/// <summary>
		/// This will sometimes come from a controlled vocabulary, but that must be ensured elsewhere
		/// </summary>
		public Role Role { get; set; }

		/// <summary>
		/// The date the contribution was made. Seems like it would be rarely used, but an early
		/// SayMore user asked for this specifically.
		/// </summary>
		/// <remarks>olac would like us to control the format of this, but we would either
		/// * need UI which can be flexible (just a year, or a date), or
		/// * parse the string from the UI in such a way that we get it into a standard format</remarks>
		public string Date { get; set; }

		/// <summary>
		/// Normally a short note about the contribution (e.g., a more specific description of the role played)
		/// </summary>
		public string Notes { get; set; }

		public Contribution(string name, Role role)
		{
			ContributorName = name;
			Role = role;
		}
	}
}