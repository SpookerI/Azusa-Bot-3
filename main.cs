using Azusa.bot_3.Core;

namespace Azusa.bot_3
{
	class main
	{
		private static void Main(string[] args)
		{
			new Bot().MainAsync().GetAwaiter().GetResult();
		}
	}
}