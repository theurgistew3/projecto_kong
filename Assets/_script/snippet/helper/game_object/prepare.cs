using UnityEngine;

namespace helper.game_object
{
	public class prepare
	{
		public static GameObject stuff()
		{
			var _stuff = GameObject.Find( consts.game_object_names.stuff );
			if ( _stuff == null )
				_stuff = new GameObject( consts.game_object_names.stuff );
			return _stuff;
		}

		public static GameObject stuff_container( string name )
		{
			return stuff_container( name, stuff() );
		}

		public static GameObject stuff_container(
			string name, GameObject _stuff )
		{
			return stuff_container( name, _stuff.transform );
		}

		public static GameObject stuff_container(
			string name, Transform _stuff )
		{
			var container = _stuff.Find( name );
			if ( !container )
			{
				var game_object = new GameObject( name );
				container = game_object.transform;
				container.parent = _stuff;
			}
			return container.gameObject;
		}
	}
}
