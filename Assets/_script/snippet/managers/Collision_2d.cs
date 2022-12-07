using UnityEngine;
using System.Collections.Generic;

namespace manager
{
	public class Collision_2d {
		public Dictionary<GameObject, Dictionary<string, bool>> collisions;
		public Dictionary<string, bool> general_collisons;

		public Collision_2d()
		{
			collisions = new Dictionary<GameObject, Dictionary<string, bool>>();
			general_collisons = new Dictionary<string, bool>();
		}

		public void add( GameObject obj, string name_status, bool status )
		{
			Dictionary<string, bool> inner_dict;
			if ( collisions.TryGetValue( obj, out inner_dict ) )
			{
				inner_dict[ name_status ] = status;
			}
			else
			{
				inner_dict = new Dictionary<string, bool>();
				collisions.Add( obj, inner_dict );
				inner_dict.Add( name_status, status );
			}

			bool end_bool;
			// colapsando los valores de las collisiones
			if ( general_collisons.TryGetValue( name_status, out end_bool ) )
			{
				if ( status )
					general_collisons[ name_status ] = true;
				else if ( _check_all_collions_for_status( name_status ) )
					general_collisons[ name_status ] = true;
				else
					general_collisons[ name_status ] = false;

			}
			else
			{
				general_collisons.Add( name_status, status );
			}
		}

		public void remove( GameObject obj )
		{
			Dictionary<string, bool> obj_dict = get( obj );
			collisions.Remove( obj );
			if ( obj_dict != null )
			{
				foreach ( string name_status in obj_dict.Keys )
					general_collisons[ name_status ] =
						_check_all_collions_for_status( name_status );
			}
		}

		protected bool _check_all_collions_for_status( string status )
		{
			bool result;
			foreach( Dictionary<string, bool> inner_dict in collisions.Values )
			{
				if ( inner_dict.TryGetValue( status, out result ) )
					if ( result )
						return result;
			}
			return false;
		}

		public Dictionary<string, bool> get( GameObject obj )
		{
			Dictionary<string, bool> inner_dict;
			if ( collisions.TryGetValue( obj, out inner_dict ) )
				return inner_dict;
			else
				return null;
		}

		public bool get( GameObject obj, string name_status )
		{
			Dictionary<string, bool> inner_dict = get( obj );
			bool result;
			if ( inner_dict != null )
			{
				if ( inner_dict.TryGetValue( name_status, out result ) )
					return result;
				else
					return false;
			}
			return false;
		}

		public bool get( string name_status )
		{
			bool result;
			if ( general_collisons.TryGetValue( name_status, out result ) )
				return result;
			else
				return false;
		}
	}
}
