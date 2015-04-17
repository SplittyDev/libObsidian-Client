using System;

namespace libObsidian.Client
{
	/// <summary>
	/// Status of an operation.
	/// </summary>
	public enum Status : byte {

		/// <summary>
		/// Unknown/undefined response
		/// </summary>
		undefined = 0,

		/// <summary>
		/// (Generic) failure
		/// </summary>
		failure,

		/// <summary>
		/// (Generic) success
		/// </summary>
		success,
	}
}

