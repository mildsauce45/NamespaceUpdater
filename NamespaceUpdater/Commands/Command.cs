
namespace NamespaceUpdater.Commands
{
	public abstract class Command
	{
		public string Name { get; private set; }
		public UpdaterContext Context { get; protected set; }		

		public Command(string name, UpdaterContext context)
		{
			Name = name;
			Context = context;
		}

		public abstract void Execute();
	}
}
