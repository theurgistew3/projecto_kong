using UnityEngine;
using System.Collections.Generic;

namespace chibi.manager.collision
{
	public class Manager_collision {
		public Dictionary<
			GameObject, Dictionary<string, Collision_info>> collisions;
		public Dictionary<
			string, Dictionary<GameObject, Collision_info>> collisions_by_name;

		public Manager_collision()
		{
			collisions = new Dictionary<
				GameObject, Dictionary<string, Collision_info>>();
			collisions_by_name = new Dictionary<
				string, Dictionary<GameObject, Collision_info>>();
		}

		public void add( Collision_info collision_info )
		{
			var inner_dict = this[ collision_info.game_object ];
			string name = collision_info.name;
			GameObject game_object = collision_info.game_object;
			if ( inner_dict != null )
				inner_dict[ name ] = collision_info;
			else
			{
				inner_dict = new Dictionary<string, Collision_info>();
				inner_dict.Add( name, collision_info );
				collisions.Add( game_object, inner_dict );
			}

			Dictionary<GameObject, Collision_info> inner_dict_by_name;
			if ( collisions_by_name.TryGetValue( name, out inner_dict_by_name ) )
				inner_dict_by_name[ game_object ] = collision_info;
			else
			{
				inner_dict_by_name = new Dictionary<GameObject, Collision_info>();
				inner_dict_by_name.Add( game_object, collision_info );
				collisions_by_name.Add( name, inner_dict_by_name );
			}
		}

		public void remove( GameObject game_object )
		{
			var inner_dict = this[ game_object ];
			if ( inner_dict == null )
				return;
			collisions.Remove( game_object );
			Dictionary<GameObject, Collision_info> dict_by_name;
			foreach ( string key in inner_dict.Keys )
				if ( collisions_by_name.TryGetValue( key, out dict_by_name ) )
				{
					dict_by_name.Remove( game_object );
					if ( dict_by_name.Count == 0 )
						collisions_by_name.Remove( key );
				}
		}

		public Dictionary<string, Collision_info> this[ GameObject obj ]
		{
			get
			{
				Dictionary<string, Collision_info> result;
				collisions.TryGetValue( obj, out result );
				return result;
			}
		}

		public bool this[ GameObject obj, string name ]
		{
			get{
				var inner_dict = this[ obj ];
				Collision_info result;
				if ( inner_dict != null )
					return inner_dict.TryGetValue( name, out result );
				return false;
			}
		}

		public bool this[ string name ]
		{
			get{
				Dictionary<GameObject, Collision_info> result;
				return collisions_by_name.TryGetValue( name, out result );
			}
		}

		public float slope( string name )
		{
			Dictionary<GameObject, Collision_info> result;
			if ( collisions_by_name.TryGetValue( name, out result ) )
			{
				foreach ( var item in result )
				{
					return item.Value.slope_angle;
				}
				return -1f;
			}
			else
			{
				return -1f;
			}
		}

		public Vector3 normal( string name )
		{
			Dictionary<GameObject, Collision_info> result;
			if ( collisions_by_name.TryGetValue( name, out result ) )
			{
				foreach ( var item in result )
				{
					foreach( var contact in item.Value.collision.contacts )
						return contact.normal;
				}
				return Vector3.zero;
			}
			else
			{
				return Vector3.zero;
			}
		}
	}
}
