using System;

namespace libObsidian.Client
{
	/// <summary>
	/// API URL.
	/// </summary>
	public class ApiUrl
	{
		#region Properties

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public string URL { get; set; }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="libObsidian.Client.ApiUrl"/> class.
		/// </summary>
		/// <param name="url">URL.</param>
		public ApiUrl (string url) {
			URL = url;
		}

		#region Implicit Conversions

		/// <param name="url">URL.</param>
		public static implicit operator string (ApiUrl url) {
			return url.URL;
		}

		/// <param name="url">URL.</param>
		public static implicit operator ApiUrl (string url) {
			return new ApiUrl (url);
		}

		#endregion

		#region Operator Overrides

		/// <param name="src">1st source.</param>
		/// <param name="src2">2nd source.</param>
		public static ApiUrl operator / (ApiUrl src, ApiUrl src2) {
			return new ApiUrl (string.Format ("{0}/{1}", src, src2));
		}

		/// <param name="src">1st source.</param>
		/// <param name="src2">2nd source.</param>
		public static ApiUrl operator / (string src, ApiUrl src2) {
			return new ApiUrl (string.Format ("{0}/{1}", src, src2));
		}

		/// <param name="src">1st source.</param>
		/// <param name="src2">2nd source.</param>
		public static ApiUrl operator / (ApiUrl src, string src2) {
			return new ApiUrl (string.Format ("{0}/{1}", src, src2));
		}

		#endregion

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString () {
			return URL;
		}
	}
}

