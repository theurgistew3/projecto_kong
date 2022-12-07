namespace SMKD
{
	public class game_manager_arkanoid : chibi.Chibi_behaviour
	{
		public motor.Dodger_motor dodger;
		public tool.Dodger_set dodgers;

		protected override void Start()
		{
			base.Start();
			if ( dodger )
				dodger.load_gun();
			else
				dodgers.list[ 0 ].dodger_motor.load_gun();
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !dodgers )
			{
				debug.info( "no esta defino el set de los dodgers" );
			}
		}
	}
}
