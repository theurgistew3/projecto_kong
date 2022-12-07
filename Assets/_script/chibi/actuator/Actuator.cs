namespace chibi.actuator
{
	public abstract class Actuator : chibi.Chibi_behaviour
	{
		public abstract void action( controller.Controller controller );
	}
}