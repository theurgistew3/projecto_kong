using UnityEngine;
using System.Collections.Generic;

namespace chibi.spawner
{
	public class Spawner : chibi.Chibi_behaviour
	{
		public List<GameObject> objects;
		public bool is_continuos = false;
		public float frequency = 1f;
		public int object_index = 0;

		public virtual GameObject spawn()
		{
			var result = _instance( next() );
			return result;
		}

		protected virtual GameObject next()
		{
			var result = objects[ object_index++ ];
			object_index %= objects.Count;
			return result;
		}

		protected virtual GameObject _instance( GameObject obj )
		{
			return helper.instantiate._( obj, transform.position );
		}

		protected virtual GameObject _instance( GameObject obj, Vector3 position )
		{
			return helper.instantiate._( obj, position );
		}
	}
}