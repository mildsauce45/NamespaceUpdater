using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NamespaceUpdater.Commands
{
	public class RunCommand : Command
	{
		public RunCommand(UpdaterContext context)
			: base(Constants.Commands.RUN, context)
		{
		}

		public override void Execute()
		{
			if (!Context.NewUsings.Any() && !Context.ReplaceUsings.Any())
			{
				Console.WriteLine("There are no namespaces to add or update.");
				return;
			}

			if (Context.TargetDirectory == null)
			{
				Console.WriteLine("You have not specified a target directory.");
				return;
			}

			Console.WriteLine("Adding and replacing namespaces");

			UpdateDirectory(Context.TargetDirectory);

			Context.Reset();

			Console.WriteLine("Finished updating files. Context reset");
		}

		private void UpdateDirectory(DirectoryInfo directory)
		{
			Console.WriteLine(string.Format("Updating {0} files in directory: {1}", Context.FileSearchPattern, directory.FullName));

			foreach (var file in directory.EnumerateFiles(Context.FileSearchPattern))
				UpdateFile(file);

			if (Context.IsRecursive)
			{
				foreach (var subdir in directory.EnumerateDirectories())
					UpdateDirectory(subdir);
			}
		}

		private void UpdateFile(FileInfo file)
		{
			var contents = ReadFile(file);

			var firstNamespaceDeclaration = contents.FirstOrDefault(s => s.StartsWith(Constants.Keywords.NAMESPACE));
			if (firstNamespaceDeclaration == null)
				return;

			var indexOfNamespace = GetIndexOf(firstNamespaceDeclaration, contents);

			if (indexOfNamespace < 0) // This should never happen
				return;

			var lastUsingIndex = GetLastUsingIndex(indexOfNamespace, contents);

			int newNamespaceIndex = lastUsingIndex < 0 ? 0 : lastUsingIndex + 1;

			// Preserve a list of what the file has
			var usings = contents.Take(newNamespaceIndex).ToList();

			var newContents = contents.ToList();

			// In this case -1 means that there are no usings and the namespace is at the top a perfectly valid case
			foreach (var nn in Context.NewUsings)
			{
				var @namespace = string.Format("{0} {1};", Constants.Keywords.USING, nn);

				// We really only want to add the namespace if it hasn't already been added
				if (!usings.Any(s => s == @namespace))
				{
					newContents.Insert(newNamespaceIndex, @namespace);

					usings.Add(@namespace);

					Console.WriteLine(string.Format("Adding namespace {0} to file {1}", @namespace, file.FullName));
				}
			}

			foreach (var rn in Context.ReplaceUsings)
			{
				var oldNamespace = string.Format("{0} {1};", Constants.Keywords.USING, rn.Item1);
				var newNamespace = string.Format("{0} {1};", Constants.Keywords.USING, rn.Item2);

				var oldUsingIndex = newContents.IndexOf(oldNamespace);
				if (oldUsingIndex >= 0)
				{
					newContents.RemoveAt(oldUsingIndex);
					newContents.Insert(oldUsingIndex, newNamespace);

					Console.WriteLine(string.Format("Replacing namespace {0} with {1} in file {2}", oldNamespace, newNamespace, file.FullName));
				}
			}

			WriteFile(file, newContents);
		}

		private string[] ReadFile(FileInfo file)
		{
			var sr = new StreamReader(file.OpenRead());

			var contents = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			sr.Close();

			return contents;
		}

		private void WriteFile(FileInfo file, IEnumerable<string> contents)
		{
			var sw = new StreamWriter(file.OpenWrite());

			foreach (var line in contents)
				sw.WriteLine(line);

			sw.Flush();
			sw.Close();
		}

		private int GetIndexOf(string search, string[] fullFile)
		{
			for (int i = 0; i < fullFile.Length; i++)
			{
				if (fullFile[i] == search)
					return i;
			}

			return -1;
		}

		private int GetLastUsingIndex(int startingIndex, string[] contents)
		{
			for (int i = startingIndex; i >= 0; i--)
			{
				if (contents[i].StartsWith(Constants.Keywords.USING))
					return i;
			}

			return -1;
		}
	}
}
