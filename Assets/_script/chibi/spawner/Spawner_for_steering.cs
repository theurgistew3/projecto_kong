using UnityEngine;

namespace chibi.spawner
{
	public class Spawner_for_steering : Spawner
	{
		public Transform target;

		protected override GameObject _instance( GameObject obj )
		{
			var instance = helper.instantiate._( obj, transform.position );
			var steering = instance.GetComponent<
				controller.steering.Steering >();
			if ( !steering )
				Debug.LogWarning( string.Format(
					"no se encontro un steering " +
					"controller en el object '{0}'",
					helper.game_object.name.full( instance )
				), instance );
			else
				steering.target = target;
			return instance;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !target )
			{
				Debug.LogWarning( string.Format(
					"el spawner '{0}' no tiene un target",
					helper.game_object.name.full( this )
				), this );
			}
		}
	}
}