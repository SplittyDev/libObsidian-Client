using NUnit.Framework;
using System;

namespace libobsidiantests
{
	using libObsidian.Client;

	[TestFixture]
	public class Test
	{
		const string APIKEY = "";

		[Test]
		public void CreateUserAccount () {
			var con = Connector.Factory.CreateAuth (UserCredentials.Unauthenticated, APIKEY);
			var cred = new UserCredentials ("obsidian_api_test", "obsidian_api_test");
			var response = false;
			Assert.DoesNotThrow (() => response = con.CreateUser (cred));
			Assert.That (response);
		}

		[Test]
		public void DeleteUserAccount () {
			var cred = new UserCredentials ("obsidian_api_test", "obsidian_api_test");
			var con = Connector.Factory.CreateAuth (cred, APIKEY);
			var response = false;
			Assert.DoesNotThrow (() => response = con.DeleteUser (cred));
			Assert.That (response);
		}
	}
}

