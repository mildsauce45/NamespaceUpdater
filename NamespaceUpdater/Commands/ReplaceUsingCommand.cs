using System;

namespace NamespaceUpdater.Commands
{
	public class ReplaceUsingCommand : Command
	{
		public string OldNamespace { get; private set; }
		public string NewNamespace { get; private set; }

		public ReplaceUsingCommand(string old, string @new, UpdaterContext context)
			: base(Constants.Commands.REPLACE, context)
		{
			OldNamespace = old;
			NewNamespace = @new;
		}

		public override void Execute()
		{
			if (string.IsNullOrWhiteSpace(OldNamespace) || string.IsNullOrWhiteSpace(NewNamespace))
				return;

			Context.ReplaceUsings.Add(Tuple.Create(OldNamespace, NewNamespace));
		}
	}
}
