using System.Net;
using Azusa.bot_3.Core;

namespace Azusa.bot_3
{
	class main
	{
		public static bool debugmode = false;
		private static void Main(string[] args)
		{
			ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
			foreach(var arg in args)
			{
				switch(arg)
				{
					case "-debug":
						debugmode = true;
						continue;
					default:
						break;
				}
			}

			new Bot().MainAsync().GetAwaiter().GetResult();
		}
	}
}