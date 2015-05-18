using System;
using System.Text;

namespace libObsidian.Client
{
	public static class APIHelper
	{
		public static string ToBase64 (this string str) {
			return Convert.ToBase64String (Encoding.UTF8.GetBytes (str));
		}

		public static string FromBase64 (this string base64) {
			return Encoding.UTF8.GetString (Convert.FromBase64String (base64));
		}
	}
}

