using System;

namespace NamespaceUpdater.Commands
{
	public class RecursiveCommand : Command
	{
		public RecursiveCommand(UpdaterContext context)
			: base(Constants.Commands.RECURSIVE, context)
		{
		}

		public override void Execute()
		{
			Console.WriteLine("Update is now recursive");

			Context.IsRecursive = true;
		}
	}
}
