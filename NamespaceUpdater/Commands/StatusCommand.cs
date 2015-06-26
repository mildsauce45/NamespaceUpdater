using System;
using System.Linq;

namespace NamespaceUpdater.Commands
{
	public class StatusCommand : Command
	{
		public StatusCommand(UpdaterContext context)
			: base(Constants.Commands.STATUS, context)
		{
		}

		public override void Execute()
		{
			Console.WriteLine();
			Console.WriteLine("-- Update Context --");
			Console.WriteLine(string.Format("Target Directory: {0}", Context.TargetDirectory));
			Console.WriteLine(string.Format("Recursive: {0}", Context.IsRecursive));
			Console.WriteLine(string.Format("File Search Pattern: {0}", Context.FileSearchPattern));
			
			WriteNewNamespaces();
			WriteReplacements();

			Console.WriteLine();
		}

		private void WriteNewNamespaces()
		{
			Console.WriteLine();
			Console.WriteLine("New Namespaces:");

			if (!Context.NewUsings.Any())
				Console.WriteLine("None");
			else
			{
				foreach (var ns in Context.NewUsings)
					Console.WriteLine(string.Format("    {0}", ns));
			}
		}

		private void WriteReplacements()
		{
			Console.WriteLine();
			Console.WriteLine("Replacing Namespaces:");

			if (!Context.ReplaceUsings.Any())
				Console.WriteLine("None");
			else
			{
				foreach (var ns in Context.ReplaceUsings)
					Console.WriteLine(string.Format("    {0} -> {1}", ns.Item1, ns.Item2));
			}
		}
	}
}
