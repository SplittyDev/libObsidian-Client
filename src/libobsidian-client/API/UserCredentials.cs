using System;
using System.Net;

namespace libObsidian.Client
{
	/// <summary>
	/// User credentials.
	/// </summary>
	public class UserCredentials
	{
		static UserCredentials unauthenticated;

		readonly string username;
		readonly string password;

		/// <summary>
		/// Gets the username.
		/// </summary>
		/// <value>The username.</value>
		public string Username { get { return username; } }

		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password { get { return password; } }

		/// <summary>
		/// Unauthenticated user singleton.
		/// </summary>
		/// <value>The unauthenticated.</value>
		public static UserCredentials Unauthenticated {
			get {
				if (unauthenticated == null)
					unauthenticated = new UserCredentials (string.Empty, string.Empty);
				return unauthenticated;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="libObsidian.Client.UserCredentials"/> class.
		/// </summary>
		/// <param name="username">Username.</param>
		/// <param name="password">Password.</param>
		public UserCredentials (string username, string password) {
			this.username = username;
			this.password = password;
		}

		public static explicit operator NetworkCredential (UserCredentials creds) {
			return new NetworkCredential (creds.Username, creds.Password);
		}
	}
}

