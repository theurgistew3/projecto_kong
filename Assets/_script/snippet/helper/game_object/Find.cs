using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace helper
{
	namespace game_object
	{
		public class Find
		{
			/// <summary>
			/// busca los hijos del gameobject de manera recursiva
			/// </summary>
			/// <param name="obj">padre en el qeu se buscaran</param>
			/// <param name="name">nombre del object qe se busca</param>
			/// <returns>hijo encontrado</returns>
			public static GameObject _( GameObject obj, string name )
			{
				Transform result = _( obj.transform, name );
				if ( result != null )
					return result.gameObject;
				return null;
			}

			public static List<GameObject> regex( GameObject obj, string string_regex )
			{
				var r = new Regex( string_regex, RegexOptions.Compiled );
				return regex( obj, r );
			}

			public static List<GameObject> regex( GameObject obj, Regex r )
			{
				var transforms = regex( obj.transform, r );
				var result = new List<GameObject>();
				if ( transforms.Count > 0 )
				{
					foreach ( Transform t in transforms )
						result.Add( t.gameObject );
				}
				return result;
			}

			public static List<Transform> regex( Transform obj, Regex r )
			{
				var result = new List<Transform>();
				for ( int i = 0; i < obj.childCount; ++i )
				{
					var child = obj.GetChild( i );
					if ( r.IsMatch( child.name ) )
						result.Add( child );
					var child_result = regex( child, r );
					if ( child_result.Count > 0 )
						result.AddRange( child_result );
				}
				return result;
			}

			public static Transform _( Transform obj, string name )
			{
				Transform result = obj.Find( name );
				if ( result != null )
					return result;
				for ( int i = 0; i < obj.childCount; ++i )
				{
					result = _( obj.GetChild( i ), name );
					if ( result != null )
						return result;
				}
				return null;
			}

			public static ( Transform, Transform ) _(
				Transform obj, string name1, string name2 )
			{
				return ( _( obj, name1 ), _( obj, name2 ) );
			}
			public static ( Transform, Transform, Transform ) _(
				Transform obj, string name1, string name2, string name3 )
			{
				return (
					_( obj, name1 ), _( obj, name2 ), _( obj, name3 ) );
			}

			public static T _<T>( GameObject obj, string name )
				where T : MonoBehaviour
			{
				GameObject result = _( obj, name );
				if ( result != null )
					return result.GetComponent<T>();
				return null;
			}

			public static List<T> regex<T>( GameObject obj, string r )
				where T : MonoBehaviour
			{
				var game_objects = regex( obj, r );
				var results = new List<T>();
				if ( game_objects.Count > 0 )
					foreach ( var t in game_objects )
					{
						var component = t.GetComponent<T>();
						if ( component )
							results.Add( component );
					}
				return results;
			}

			public static ( T, T ) _<T>(
				GameObject obj, string name1, string name2 )
				where T : MonoBehaviour
			{
				return (
					_<T>( obj, name1 ),
					_<T>( obj, name2 )
				);
			}

			public static ( T, T, T ) _<T>(
				GameObject obj, string name1, string name2, string name3 )
				where T : MonoBehaviour
			{
				return (
					_<T>( obj, name1 ),
					_<T>( obj, name2 ),
					_<T>( obj, name3 )
				);
			}

			public static ( T, T, T, T ) _<T>(
				GameObject obj, string name1, string name2, string name3,
				string name4 )
				where T : MonoBehaviour
			{
				return (
					_<T>( obj, name1 ),
					_<T>( obj, name2 ),
					_<T>( obj, name3 ),
					_<T>( obj, name4 )
				);
			}

			public static ( T, T, T, T, T ) _<T>(
				GameObject obj, string name1, string name2, string name3,
				string name4, string name5 )
				where T : MonoBehaviour
			{
				return (
					_<T>( obj, name1 ),
					_<T>( obj, name2 ),
					_<T>( obj, name3 ),
					_<T>( obj, name4 ),
					_<T>( obj, name5 )
				);
			}

			public static ( T, T, T, T, T, T ) _<T>(
				GameObject obj, string name1, string name2, string name3,
				string name4, string name5, string name6 )
				where T : MonoBehaviour
			{
				return (
					_<T>( obj, name1 ),
					_<T>( obj, name2 ),
					_<T>( obj, name3 ),
					_<T>( obj, name4 ),
					_<T>( obj, name5 ),
					_<T>( obj, name6 )
				);
			}

			public static GameObject[] all( string name )
			{
				GameObject[] result = GameObject.FindObjectsOfType<GameObject>();
				return result.Where( obj => obj.name == name ).ToArray();
			}

			public static GameObject[] all_regex( string regex )
			{
				GameObject[] result = GameObject.FindObjectsOfType<GameObject>();
				var r = new Regex( regex, RegexOptions.Compiled );
				return result.Where( obj => r.IsMatch( obj.name ) ).ToArray();
			}
		}
	}
}