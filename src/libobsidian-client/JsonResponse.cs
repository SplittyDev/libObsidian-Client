using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

// MonoDevelop analysis rules
// Analysis disable FieldCanBeMadeReadOnly.Local

namespace libObsidian.Client
{
	/// <summary>
	/// Deserialized JSON response from the Obsidian API.
	/// </summary>
	[DataContract (Name = "obsidian", Namespace = "obsidian")]
	public class JsonResponse
	{
		#region DataMembers
		#pragma warning disable 169

		[DataMember (Name = "status", IsRequired = true, Order = 1)]
		protected Status status;

		[DataMember (Name = "message", IsRequired = false, Order = 2)]
		protected string message;

		[DataMember (Name = "data", IsRequired = true, Order = 3)]
		protected Dictionary<string, string> data;

		#pragma warning restore 169
		#endregion

		#region Properties

		/// <summary>
		/// The <see cref="libObsidian.Client.Status"/> of the operation.
		/// </summary>
		/// <value>The status.</value>
		public Status Status { get { return status; } }

		/// <summary>
		/// An optional message, further describing the status of the operation.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get { return message; } }

		/// <summary>
		/// A Dictionary of data returned by this API call.
		/// </summary>
		/// <value>The data.</value>
		public Dictionary<string, string> Data { get { return data; } }

		#endregion

		#if DEBUG
		/// <summary>
		/// A JsonResponse factory for debugging purposes.
		/// You shouldn't see this in the Release configuration.
		/// </summary>
		public static class Factory
		{
			/// <summary>
			/// Creates a new <see cref="JsonResponse"/> using the specified status, message and data.
			/// </summary>
			/// <param name="status">Status.</param>
			/// <param name="message">Message.</param>
			/// <param name="data">Data.</param>
			public static JsonResponse Create (Status status, string message, Dictionary<string, string> data) {
				return new JsonResponse {
					status = status,
					message = message,
					data = data
				};
			}
		}
		#endif
	}
}

