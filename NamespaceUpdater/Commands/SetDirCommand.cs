using System;
using System.IO;

namespace NamespaceUpdater.Commands
{
	public class SetDirCommand : Command
	{
		public string TargetDirectory { get; private set; }

		public SetDirCommand(string directory, UpdaterContext context)
			: base(Constants.Commands.SETDIR, context)
		{
			this.TargetDirectory = directory;
		}

		public override void Execute()
		{
			if (Directory.Exists(TargetDirectory))
			{
				Console.WriteLine("Target directory successfully set to: " + TargetDirectory);

				Context.TargetDirectory = new DirectoryInfo(TargetDirectory);
			}
			else
			{
				Console.WriteLine("Target directory does not exist.");
			}
		}
	}
}
