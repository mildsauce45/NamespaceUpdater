using System;
using System.Collections.Generic;
using System.IO;

namespace NamespaceUpdater
{
	public class UpdaterContext
	{
		public bool IsRecursive { get; set; }
		public string FileSearchPattern { get; set; }
		public DirectoryInfo TargetDirectory { get; set; }

		public IList<string> NewUsings { get; private set; }
		public IList<Tuple<string, string>> ReplaceUsings { get; private set; }

		public UpdaterContext()
		{
			NewUsings = new List<string>();
			ReplaceUsings = new List<Tuple<string, string>>();

			FileSearchPattern = Constants.Defaults.DEFAULT_PATTERN;
		}

		public void Reset()
		{
			IsRecursive = false;
			TargetDirectory = null;
			FileSearchPattern = Constants.Defaults.DEFAULT_PATTERN;

			NewUsings.Clear();
			ReplaceUsings.Clear();
		}
	}
}
