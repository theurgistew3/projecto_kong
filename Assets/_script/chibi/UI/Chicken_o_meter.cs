namespace chibi.UI.chicken_o_meter
{
	public class Chicken_o_meter : Chibi_behaviour
	{
		public chibi.tool.reference.Reference_gauge gauge;
		public UnityEngine.UI.Slider slider;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !slider )
			{
				debug.error( "no se asigno el slider" );
			}
		}
	}
}
