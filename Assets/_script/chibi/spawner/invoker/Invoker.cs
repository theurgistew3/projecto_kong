using UnityEngine;

namespace chibi.spawner.invoker
{
	public class Invoker : chibi.Chibi_behaviour
	{
		public Spawner target;

		protected override void _init_cache()
		{
			base._init_cache();
			if ( target == null )
				target = GetComponent<Spawner>();
			if ( target == null )
				Debug.LogWarning( string.Format(
					"the gameobject '{0}' no tiene taget se esperaba" +
					" un Spwan_point", helper.game_object.name.full( this ) ) );
		}
	}
}