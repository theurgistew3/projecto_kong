using UnityEngine;
using System.Collections.Generic;

namespace helper.game_object
{
	public class get_components
	{
		public static List<T> _<T>( GameObject obj )
			where T : MonoBehaviour
		{
			return _<T>( obj.transform );
		}

		public static List<T> _<T>( Transform transform )
			where T : MonoBehaviour
		{
			List<T> result = new List<T>();
			foreach ( Transform t in transform )
			{
				var obj = t.GetComponent<T>();
				if ( obj )
					result.Add( obj );
			}
			return result;
		}
	}
}