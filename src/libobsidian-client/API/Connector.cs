using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

// MonoDevelop analysis rules
// Analysis disable FieldCanBeMadeReadOnly.Local

namespace libObsidian.Client
{
	/// <summary>
	/// Obsidian API connector.
	/// </summary>
	public class Connector
	{
		#region API
		const string API_BASE	= "https://api.obsidians.io";
		const string API_AUTH	= API_BASE + "/auth";
		const string API_USERS 	= API_BASE + "/users";
		const string API_FILES	= API_BASE + "/files";
		#endregion

		#region Private
		WebProxy proxy;
		string apikey;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="libObsidian.Client.Connector"/> class.
		/// </summary>
		protected Connector (string apiKey)
		{
			apikey = apiKey;
			proxy = new WebProxy ();
		}

		/// <summary>
		/// Determines if the api base url uses a valid certificate.
		/// </summary>
		/// <returns><c>true</c> if the api base url uses a valid certificate; otherwise, <c>false</c>.</returns>
		public static bool IsValidCertificate () {

			// Create uri
			var uri = new Uri (API_BASE);

			// Get service point
			var sp = ServicePointManager.FindServicePoint (uri);

			// Create connection group
			var group = Guid.NewGuid ().ToString ();

			// Create request
			var request = WebRequest.Create (uri) as HttpWebRequest;
			request.ConnectionGroupName = group;

			// Get response
			using (var response = request.GetResponse ()) {
			}

			// Close connection group
			sp.CloseConnectionGroup (group);

			// Get SSL certificate
			var cert = new X509Certificate2 (sp.Certificate);
			return cert != null && cert.Verify ();
		}

		/// <summary>
		/// Authenticates the specified user.
		/// Should be called to check if the provided credentials are valid.
		/// </summary>
		/// <param name="usercred">User credentials.</param>
		public void Auth (UserCredentials usercred) {

			if (!IsValidCertificate ())
				throw new SecurityException (
					"The validity of the Obsidian SSL certificate couldn't be verified.\n" +
					"A hacker might be trying to impersonate the Obsidian server.");

			// Build request
			var request = WebRequest.CreateHttp (API_AUTH)
				.Proxy (proxy)
				.Method (HttpMethod.GET)
				.Auth (usercred)
				.SignKey (apikey);

			// Get response
			using (var response = request.GetResponse () as HttpWebResponse) {
				switch (response.StatusCode) {

					// Wrong user credentials
					case HttpStatusCode.Forbidden:
						throw new InvalidCredentialException (response.StatusDescription);

						// Something else went wrong
					case HttpStatusCode.Unauthorized:
						throw new AuthenticationException (response.StatusDescription);
				}
			}
		}

		/// <summary>
		/// Obsidian API Connector factory.
		/// </summary>
		public static class Factory {

			/// <summary>
			/// Creates a new <see cref="Connector"/> instance and authenticates.
			/// </summary>
			/// <returns>A ready-to-use <see cref="Connector"/> instance.</returns>
			/// <param name="user">The Obsidian user.</param>
			/// <param name="apiKey">Your unique API key.</param>
			/// <param name="proxy">WebProxy if needed.</param>
			public static Connector CreateAuth (UserCredentials user, string apiKey, WebProxy proxy = null) {

				// Create connector
				var con = new Connector (apiKey);

				// Set proxy
				if (proxy != null)
					con.proxy = proxy;

				// Authenticate
				con.Auth (user);

				// Return connector
				return con;
			}
		}
	}
}

