
namespace NamespaceUpdater.Commands
{
	public class AddUsingCommand : Command
	{
		public string Namespace { get; private set; }

		public AddUsingCommand(string @namespace, UpdaterContext context)
			: base(Constants.Commands.ADD, context)
		{
			this.Namespace = @namespace;
		}

		public override void Execute()
		{
			Context.NewUsings.Add(this.Namespace);
		}
	}
}
