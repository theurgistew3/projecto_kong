using UnityEngine;

namespace danmaku.motor
{
	public class Touha_motor : chibi.motor.npc.Motor_isometric
	{
		public chibi.damage.motor.HP_engine hp_motor;

		public GameObject explotion_prefab;

		protected override void _init_cache()
		{
			base._init_cache();

			hp_motor = GetComponent< chibi.damage.motor.HP_engine >();
			if ( !hp_motor )
				debug.error( "no se encontro un hp_engine" );

			hp_motor.on_died += on_died;
			if ( !explotion_prefab )
				debug.warning( "no hay un prefab de explocion" );
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			hp_motor.on_died -= on_died;
		}

		public override void on_died()
		{
			if ( explotion_prefab )
				helper.instantiate._( explotion_prefab, transform.position );
			recycle();
		}

		protected override void _process_jump( ref Vector3 speed_vector )
		{
		}

		protected override void _proccess_gravity( ref Vector3 velocity_vector )
		{
		}
	}
}
