using UnityEngine;

namespace helper.game_object
{
	public class clean
	{
		public static void scene()
		{
			var objs = GameObject.FindObjectsOfType<GameObject>();
			foreach ( var obj in objs )
				if ( obj.name != "New Game Object" )
					GameObject.Destroy( obj );
		}

		public static void children( GameObject obj )
		{
			children( obj.transform );
		}

		public static void children( Transform transform )
		{
			for ( int i = transform.childCount - 1; i >= 0; --i )
			{
				var child = transform.GetChild( i );
				GameObject.Destroy( child );
			}
		}

		public static void children_immediate( GameObject obj )
		{
			children_immediate( obj.transform );
		}

		public static void children_immediate( Transform transform )
		{
			for ( int i = transform.childCount - 1; i >= 0; --i )
			{
				var child = transform.GetChild( i );
				GameObject.DestroyImmediate( child.gameObject );
			}
		}
	}
}