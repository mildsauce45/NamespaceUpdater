
namespace NamespaceUpdater.Commands
{
	public class ResetCommand : Command
	{
		public ResetCommand(UpdaterContext context)
			: base(Constants.Commands.RESET, context)
		{
		}

		public override void Execute()
		{
			Context.Reset();
		}
	}
}
