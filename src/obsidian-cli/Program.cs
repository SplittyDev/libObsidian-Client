using System;
using System.Collections.Generic;
using libObsidian.Client;

namespace obsidiancli
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Obsidian Command Line Interface");
			var data = JsonResponse.Factory.Create (
				Status.success,
				"Image upload successful.",
				new Dictionary<string, string> {
					{ "filename", "xihF1GVd.jpeg" },
					{
						"url",
						"http://exp.nikx.io/img/xihF1GVd.jpeg"
					}
				}
			);
			var json = data.Serialize ();
			var response = JSON.ParseResponse (json);
			Console.WriteLine ("[INPUT]\n{0}", json);
			Console.WriteLine ("[OUTPUT]");
			Console.WriteLine ("Status: {0}", response.Status);
			Console.WriteLine ("Message: {0}", response.Message);
			Console.WriteLine ("Data:");
			foreach (var kvp in response.Data)
				Console.WriteLine (" {0}: {1}", kvp.Key, kvp.Value);
		}
	}
}
