using UnityEngine;

namespace chibi.spawner
{
	public class Spawner_pool : Spawner
	{
		// public chibi.pool.Pool_behaviour pool;

		protected override GameObject _instance( GameObject obj )
		{
			return pool.pop();
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !pool )
				debug.error( "el spawner no tiene un pool" );
		}
	}
}
