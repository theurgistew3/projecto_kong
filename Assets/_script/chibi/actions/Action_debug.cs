using chibi.controller;

namespace chibi.action
{
	public class Action_debug: Action
	{
		public override void action( Controller controller )
		{
			debug.log( "{0} activo {1}", controller, this );
		}

		public override void seek()
		{
			//throw new System.NotImplementedException();
		}

		public override void unseek()
		{
			//throw new System.NotImplementedException();
		}
	}
}