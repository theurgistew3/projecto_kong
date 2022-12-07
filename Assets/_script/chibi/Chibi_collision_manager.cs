using UnityEngine;
using System.Collections.Generic;
using System;
using chibi.motor;
using chibi.motor.npc;

namespace chibi.manager.collision
{
	public class Chibi_collision_manager : Chibi_behaviour
	{
		public manager.collision.Manager_collision manager_collisions;

		protected virtual void OnCollisionEnter( Collision collision )
		{
			manager_collisions.add( new manager.collision.Collision_info(
				"all", collision ) );
			proccess_collision( collision );
		}

		protected virtual void OnCollisionExit( Collision collision )
		{
			manager_collisions.remove( collision.gameObject );
		}

		protected virtual void proccess_collision( Collision collision )
		{
			if ( chibi.tag.consts.is_scenary( collision ) )
			{
				if ( collision.contacts.Length != 0 )
				{
					//__validate_normal_points( collision );
					_process_collision_scenary( collision );
				}
			}
		}

		protected virtual void _process_collision_scenary( Collision collision )
		{
			// _check_is_collision_is_a_floor( collision );
			// _check_is_collision_is_a_wall( collision );
		}

		protected virtual void __validate_normal_points( Collision collision )
		{
			List<Vector3> normal_points = new List<Vector3>();
			foreach ( ContactPoint contact in collision.contacts )
			{
				normal_points.Add( contact.normal );
			}
			Vector3 first = normal_points[ 0 ];
			for ( int i = 1; i < normal_points.Count; ++i )
				if ( first != normal_points[ i ] )
				{
					string msg = string.Format(
						"se encontro una colision en la que los normal points " +
						"no son iguales con {0} y {1}, lista de nomral" +
						"points {2}", this, collision.gameObject, normal_points );
					Debug.LogWarning( msg );
				}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			manager_collisions = new manager.collision.Manager_collision();
		}

		public Dictionary<string, Collision_info> this[ GameObject obj ]
		{
			get
			{
				return manager_collisions[ obj ];
			}
		}

		public bool this[ GameObject obj, string name ]
		{
			get{
				return manager_collisions[ obj, name ];
			}
		}

		public bool this[ string name ]
		{
			get{
				return manager_collisions[ name ];
			}
		}

		public float slope( string name )
		{
			return manager_collisions.slope( name );
		}

		public Vector3 normal( string name )
		{
			return manager_collisions.normal( name );
		}
	}
}
