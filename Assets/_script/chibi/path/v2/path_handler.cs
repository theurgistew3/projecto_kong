using System.Collections.Generic;
using chibi.controller.handler;
using UnityEngine;

namespace chibi.path
{
	[System.Serializable]
	public class Path_handler
	{
		public List< Handler > handlers;
		[Range( 0, 1 )]
		public float position_curve = 0f;
		public float raidus = 1f;

		public GameObject make_point( chibi.path.Path path )
		{
			var obj = new GameObject();
			obj.transform.position = path.evaluate( position_curve );
			var collider = obj.AddComponent<SphereCollider>();
			var handler_behaviour = obj.AddComponent<Handler_behaviour>();
			handler_behaviour.handlers = handlers;
			handler_behaviour.is_global = false;

			collider.radius = raidus;
			collider.isTrigger = true;
			return obj;
		}
	}
}
