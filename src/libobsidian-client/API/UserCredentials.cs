using System;
using System.Net;

namespace libObsidian.Client
{
	/// <summary>
	/// User credentials.
	/// </summary>
	public class UserCredentials
	{
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

