namespace chibi.action
{
	public abstract class Action: chibi.Chibi_behaviour
	{
		public abstract void action( controller.Controller controller );

		public abstract void seek();
		public abstract void unseek();
	}
}