using System;
using System.Net;

namespace libObsidian.Client
{
	public static class RequestChainingExtensions
	{
		/// <summary>
		/// Set the proxy of the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="proxy">Proxy.</param>
		public static HttpWebRequest Proxy (this HttpWebRequest request, WebProxy proxy) {
			request.Proxy = proxy;
			return request;
		}

		/// <summary>
		/// Set the method of the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="method">Method.</param>
		public static HttpWebRequest Method (this HttpWebRequest request, string method) {
			request.Method = method;
			return request;
		}

		/// <summary>
		/// Set the method of the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="method">Method.</param>
		public static HttpWebRequest Method (this HttpWebRequest request, HttpMethod method) {
			request.Method = Enum.GetName (typeof (HttpMethod), method).ToUpperInvariant ();
			return request;
		}

		/// <summary>
		/// Set the credentials of the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="creds">Creds.</param>
		public static HttpWebRequest Auth (this HttpWebRequest request, NetworkCredential creds) {
			request.Credentials = creds;
			return request;
		}

		/// <summary>
		/// Set the credentials of the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="creds">Creds.</param>
		public static HttpWebRequest Auth (this HttpWebRequest request, UserCredentials creds) {
			request.Credentials = (NetworkCredential)creds;
			return request;
		}

		/// <summary>
		/// Set a header of the specified request.
		/// </summary>
		/// <param name="request">Request.</param>
		/// <param name="header">Header.</param>
		/// <param name="value">Value.</param>
		public static HttpWebRequest Header (this HttpWebRequest request, string header, string value) {
			request.Headers[header] = value;
			return request;
		}

		/// <summary>
		/// Set the apikey header of the specified request.
		/// </summary>
		/// <returns>The key.</returns>
		/// <param name="request">Request.</param>
		/// <param name="apiKey">API key.</param>
		public static HttpWebRequest SignKey (this HttpWebRequest request, string apiKey) {
			return request.Header ("apikey", apiKey.ToBase64 ());
		}
	}
}

