using System;
using System.Text.RegularExpressions;
using NamespaceUpdater.Commands;

namespace NamespaceUpdater
{
	class Program
	{
		static void Main(string[] args)
		{			
			string str = null;
			var context = new UpdaterContext();

			Console.Write("> ");

			while (!string.IsNullOrWhiteSpace(str = Console.ReadLine()))
			{
				var command = ParseCommand(str, context);

				if (command != null)
					command.Execute();

				Console.Write("> ");
			}
		}

		private static Command ParseCommand(string str, UpdaterContext context)
		{
			var workableString = CollapseWhitespace(str);

			int firstEmpty = workableString.IndexOf(' ');

			if (firstEmpty < 0)
			{
				var lower = str.ToLower();

				switch (lower)
				{
					case Constants.Commands.RECURSIVE:
						return new RecursiveCommand(context);
					case Constants.Commands.RUN:
						return new RunCommand(context);
					case Constants.Commands.STATUS:
						return new StatusCommand(context);
					case Constants.Commands.RESET:
						return new ResetCommand(context);
					default:
						return null;
				}
			}
			else if (firstEmpty < str.Length)
			{
				var commandString = workableString.Substring(0, firstEmpty).ToLower();
				var remainingCommandString = workableString.Substring(firstEmpty + 1);

				switch (commandString)
				{
					case Constants.Commands.SETDIR:
						return new SetDirCommand(remainingCommandString, context);
					case Constants.Commands.ADD:
						return new AddUsingCommand(remainingCommandString, context);
					case Constants.Commands.PATTERN:
						return new PatternCommand(remainingCommandString, context);
					case Constants.Commands.REPLACE:
						var namespacingStrings = remainingCommandString.Split(new char[] { ' ' });
						
						return new ReplaceUsingCommand(namespacingStrings[0], namespacingStrings[1], context);
				}
			}

			return null;
		}

		private static string CollapseWhitespace(string input)
		{
			return string.IsNullOrEmpty(input) ? string.Empty : Regex.Replace(input, @"\s+", " ");
		}
	}
}
