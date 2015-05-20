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
		#region Exception Messages

		const string EXCEPTION_INVALID_CERTIFICATE =
			"The validity of the Obsidian SSL certificate couldn't be verified.\n" +
			"A hacker might be trying to impersonate the Obsidian server.";

		const string EXCEPTION_USER_ALREADY_EXISTS =
			"The provided credentials matched a user that already exists.\n" +
			"Therefore, the user cannot be created.";

		const string EXCEPTION_INVALID_CREDENTIALS =
			"The provided credentials didn't match any user or were invalid for this operation.";

		#endregion

		#region API URLs

		/// <summary>
		/// API / URL.
		/// </summary>
		readonly static ApiUrl API_BASE		= "https://api.obsidians.io";

		/// <summary>
		/// API /auth URL.
		/// </summary>
		readonly static ApiUrl API_AUTH		= "auth";

		/// <summary>
		/// API /users URL.
		/// </summary>
		readonly static ApiUrl API_USERS	= "users";

		/// <summary>
		/// API /files URL.
		/// </summary>
		readonly static ApiUrl API_FILES	= "files";

		#endregion

		#region Private

		/// <summary>
		/// The web proxy instance that all requests will use.
		/// </summary>
		WebProxy proxy;

		/// <summary>
		/// The API key.
		/// </summary>
		string apikey;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="libObsidian.Client.Connector"/> class.
		/// </summary>
		protected Connector (string apiKey, ApiTarget target = ApiTarget.v1)
		{
			apikey = apiKey;
			proxy = new WebProxy ();
		}

		#region Private Static Members

		/// <summary>
		/// Determines if the api base url uses a valid certificate.
		/// </summary>
		/// <returns><c>true</c> if the api base url uses a valid certificate; otherwise, <c>false</c>.</returns>
		static bool IsValidCertificate () {

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

		#endregion

		#region Public API v1 Functions

		/// <summary>
		/// Authenticates the specified user.
		/// Should be called to check if the provided credentials are valid.
		/// </summary>
		/// <exception cref="SecurityException">
		/// Thrown when the validity of the server SSL certificate cannot be verified.
		/// </exception>
		/// <exception cref="InvalidCredentialException">
		/// Thrown when 403 FORBIDDEN is returned by the server.
		/// This usually means that the provided credentials didn't match any user.
		/// </exception>
		/// <exception cref="AuthenticationException">
		/// Thrown when 401 UNAUTHORIZED is returned by the server.
		/// This means that the user couldn't be authenticated.
		/// The reason for this varies.
		/// </exception>
		/// <param name="cred">User credentials.</param>
		public void Auth (UserCredentials cred) {

			// Verify server SSL certificate
			if (!IsValidCertificate ())
				throw new SecurityException (EXCEPTION_INVALID_CERTIFICATE);

			// Build request
			var request = WebRequest.CreateHttp (API_BASE/API_AUTH)
				.Proxy (proxy)
				.Method (HttpMethod.GET)
				.Auth (cred)
				.SignKey (apikey);

			// Get response
			using (var response = request.GetResponse () as HttpWebResponse) {
				switch (response.StatusCode) {

					// Invalid credentials
					case HttpStatusCode.Forbidden:
						throw new InvalidCredentialException (EXCEPTION_INVALID_CREDENTIALS);

					// Something else went wrong
					case HttpStatusCode.Unauthorized:
						throw new AuthenticationException (response.StatusDescription);
				}
			}
		}

		/// <summary>
		/// Creates the specified user.
		/// </summary>
		/// <exception cref="SecurityException">
		/// Thrown when the validity of the server SSL certificate cannot be verified.
		/// Also thrown when the user couldn't be created for some reason.
		/// Check the exception text for more information.
		/// </exception>
		/// <returns><c>true</c>, if the user was created, <c>false</c> otherwise.</returns>
		/// <param name="cred">Credentials of the user that's going to be created.</param>
		public bool CreateUser (UserCredentials cred) {

			// Verify server SSL certificate
			if (!IsValidCertificate ())
				throw new SecurityException (EXCEPTION_INVALID_CERTIFICATE);

			// Build request
			var request = WebRequest.CreateHttp (API_BASE/API_USERS/cred.Username)
				.Proxy (proxy)
				.Method (HttpMethod.PUT)
				.Auth (cred)
				.SignKey (apikey);

			// Get response
			using (var response = request.GetResponse () as HttpWebResponse) {
				switch (response.StatusCode) {

					// Success
					case HttpStatusCode.OK:
					case HttpStatusCode.Created:
					case HttpStatusCode.Accepted:
						return true;

					// This user already exists
					case HttpStatusCode.Forbidden:
						throw new SecurityException (EXCEPTION_USER_ALREADY_EXISTS);
				}
			}

			return false;
		}

		/// <summary>
		/// Deletes the specified user.
		/// </summary>
		/// <exception cref="SecurityException">
		/// Thrown when the validity of the server SSL certificate cannot be verified.
		/// </exception>
		/// <exception cref="InvalidCredentialException">
		/// Thrown when 403 FORBIDDEN is returned by the server.
		/// This usually means that the provided credentials didn't match any user.
		/// </exception>
		/// <returns><c>true</c>, if the user was deleted, <c>false</c> otherwise.</returns>
		/// <param name="cred">Credentials of the user that's going to be deleted.</param>
		public bool DeleteUser (UserCredentials cred) {

			// Verify server SSL certificate
			if (!IsValidCertificate ())
				throw new SecurityException (EXCEPTION_INVALID_CERTIFICATE);

			// Build request
			var request = WebRequest.CreateHttp (API_BASE/API_USERS/cred.Username)
				.Proxy (proxy)
				.Method (HttpMethod.DELETE)
				.Auth (cred)
				.SignKey (apikey);

			// Get response
			using (var response = request.GetResponse () as HttpWebResponse) {
				switch (response.StatusCode) {

					// Success
					case HttpStatusCode.OK:
					case HttpStatusCode.Accepted:
						return true;

					// Invalid credentials
					case HttpStatusCode.Forbidden:
						throw new InvalidCredentialException (EXCEPTION_INVALID_CREDENTIALS);
					
					// Couldn't delete the user for some reason
					case HttpStatusCode.NotModified:
						return false;
				}
			}

			return false;
		}

		#endregion

		#region Factory

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
				if (user != UserCredentials.Unauthenticated)
					con.Auth (user);

				// Return connector
				return con;
			}
		}

		#endregion
	}
}

