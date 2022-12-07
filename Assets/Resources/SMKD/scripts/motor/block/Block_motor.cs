namespace SMKD.motor
{
	public class Block_motor : chibi.motor.Motor_physical
	{
		public chibi.damage.motor.HP_engine hp_motor;

		protected override void _init_cache()
		{
			base._init_cache();

			hp_motor = GetComponent< chibi.damage.motor.HP_engine >();
			if ( !hp_motor )
				debug.error( "no se encontro un hp_engine" );

			hp_motor.on_died += on_died;
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			hp_motor.on_died -= on_died;
		}

		public virtual void on_died()
		{
			recycle();
			// debug.info( "deberia de ser destruido" );
		}
	}
}