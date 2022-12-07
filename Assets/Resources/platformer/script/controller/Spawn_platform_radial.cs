using UnityEngine;
using chibi.controller.npc;
using System.Collections.Generic;

namespace platformer.controller.platform
{
	public class Spawn_platform_radial : chibi.controller.Controller
	{
		public List<GameObject> platforms = new List<GameObject>();
		public float radius = 2f;

		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				base.desire_direction = new Vector3( value.y, value.z, value.x );
				debug.draw.sphere( position, Color.green, 0.2f, 0.2f );
			}
		}

		public Vector3 position 
		{
			get {
				return transform.position + ( desire_direction * radius );
			}
		}

		public void spawn( int index )
		{
			spawn( position, index, Quaternion.identity );
		}

		public void spawn( Vector3 position, int index, Quaternion rotation )
		{
			helper.instantiate._( platforms[ index ], position, rotation );
		}
	}
}
