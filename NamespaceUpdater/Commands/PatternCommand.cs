using System;

namespace NamespaceUpdater.Commands
{
	public class PatternCommand : Command
	{
		public string Pattern { get; private set; }

		public PatternCommand(string pattern, UpdaterContext context)
			: base(Constants.Commands.PATTERN, context)
		{
			this.Pattern = pattern;
		}

		public override void Execute()
		{
			if (!string.IsNullOrWhiteSpace(Pattern))
			{
				Context.FileSearchPattern = Pattern;
				Console.WriteLine("File search pattern updated to " + Pattern);
			}
		}
	}
}
