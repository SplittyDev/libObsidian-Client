using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace libObsidian.Client
{
	/// <summary>
	/// Helper class for simple JSON deserialization
	/// </summary>
	public static class JSON
	{
		public static JsonResponse ParseResponse (string json) {
			var serializer = new DataContractJsonSerializer (typeof(JsonResponse));
			var data = Encoding.UTF8.GetBytes (json);
			JsonResponse response;
			using (var ms = new MemoryStream (data))
				response = serializer.ReadObject (ms) as JsonResponse;
			return response;
		}

		#if DEBUG
		public static string Serialize (this JsonResponse json) {
			var serializer = new DataContractJsonSerializer (typeof(JsonResponse));
			string result;
			using (var ms = new MemoryStream ()) {
				serializer.WriteObject (ms, json);
				result = Encoding.UTF8.GetString (ms.ToArray ());
			}
			return result;
		}
		#endif
	}
}

