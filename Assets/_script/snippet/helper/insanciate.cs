using UnityEngine;
using System.Linq;

namespace helper {
	public class instantiate {
		public static GameObject position( GameObject original, Vector3 pos ){
			return _( original, pos );
		}

		public static GameObject parent(
			GameObject original, Transform parent_target )
		{
			GameObject result = _( original );
			result.transform.parent = parent_target;
			return result;
		}

		public static GameObject parent(
			GameObject original, Transform parent_target, string name )
		{
			GameObject result = _( original, name );
			result.transform.parent = parent_target;
			return result;
		}

		public static GameObject parent(
			GameObject original, GameObject parent_target )
		{
			return parent( original, parent_target.transform );
		}

		public static GameObject parent(
				GameObject original, MonoBehaviour parent_target )
		{
			return parent( original, parent_target.transform );
		}

		public static GameObject parent(
			GameObject original, Transform parent_target, bool reset_pos )
		{
			GameObject result = parent( original, parent_target );
			if ( reset_pos ){
				result.transform.localPosition = Vector3.zero;
			}
			return result;
		}

		public static GameObject parent(
			GameObject original, Transform parent_target, bool reset_pos,
			string name )
		{
			GameObject result = parent( original, parent_target, name );
			if ( reset_pos ){
				result.transform.localPosition = Vector3.zero;
			}
			return result;
		}

		public static GameObject parent(
				GameObject original, GameObject parent_target, bool reset_pos )
		{
			return parent( original, parent_target.transform, reset_pos );
		}

		public static T parent<T>( T original, Transform parent )
			where T : MonoBehaviour
		{
			T result = _<T>( original );
			result.transform.parent = parent;
			return result;
		}

		public static T parent<T>( T original, Transform parent, string name )
			where T : MonoBehaviour
		{
			T result = parent<T>( original, parent );
			result.name = name;
			return result;
		}

		public static T parent<T>( T original, GameObject parent_target )
			where T : MonoBehaviour
		{
			return parent<T>( original, parent_target.transform );
		}

		public static T parent<T>(
			T original, GameObject parent_target, string name )
			where T : MonoBehaviour
		{
			return parent<T>( original, parent_target.transform, name );
		}

		public static T parent<T>(
			T original, Transform parent_target, bool reset_pos )
			where T : MonoBehaviour
		{
			T result = parent<T>( original, parent_target );
			if ( reset_pos ){
				result.transform.localPosition = Vector3.zero;
			}
			return result;
		}

		public static T parent<T>(
			T original, Transform parent_target, bool reset_pos, string name )
			where T : MonoBehaviour
		{
			T result = parent<T>( original, parent_target, name );
			if ( reset_pos ){
				result.transform.localPosition = Vector3.zero;
			}
			return result;
		}

		public static T parent<T>(
			T original, GameObject parent_target, bool reset_pos )
			where T : MonoBehaviour
		{
			return parent<T>( original, parent_target.transform, reset_pos );
		}

		public static T parent<T>(
			T original, GameObject parent_target, bool reset_pos,
			string name )
			where T : MonoBehaviour
		{
			return parent<T>(
				original, parent_target, reset_pos, name );
		}

		public static T _<T>( T obj ) where T : MonoBehaviour
		{
			return MonoBehaviour.Instantiate( obj ) as T;
		}

		public static T _<T>( T obj, string name ) where T : MonoBehaviour
		{
			var result = _<T>( obj );
			result.name = name;
			return result;
		}

		public static T _<T>( T obj, Vector3 pos ) where T : MonoBehaviour
		{
			T result = _( obj );
			result.transform.position = pos;
			return result;
		}

		public static T _<T>( T obj, Vector3 pos, Quaternion rot )
			where T : MonoBehaviour
		{
			T result = _( obj, pos );
			result.transform.rotation = rot;
			return result;
		}

		public static GameObject _( GameObject obj )
		{
			return MonoBehaviour.Instantiate( obj ) as GameObject;
		}

		public static GameObject _( GameObject obj, string name )
		{
			var result = _( obj );
			result.name = name;
			return result;
		}

		public static GameObject _( GameObject obj, Vector3 pos )
		{
			GameObject result = _( obj );
			result.transform.position = pos;
			return result;
		}

		public static GameObject _(
			GameObject obj, Vector3 pos, Quaternion rot )
		{
			GameObject result = _( obj, pos );
			result.transform.rotation = rot;
			return result;
		}

		public static void destroy( GameObject obj )
		{
			GameObject.Destroy( obj );
		}
		public static void destroy( Transform obj )
		{
			GameObject.Destroy( obj );
		}

		public static void destroy_immediate( GameObject obj )
		{
			GameObject.DestroyImmediate( obj );
		}
		public static void destroy_immediate( Transform obj )
		{
			GameObject.DestroyImmediate( obj.gameObject );
		}

		public static void destroy_childrens( GameObject parent )
		{
			foreach ( Transform child in parent.transform )
				destroy( child );
		}

		public static void destroy_immediate_childrens( GameObject parent )
		{
			var childrens = parent.transform.Cast<Transform>().ToList();
			foreach ( Transform child in childrens )
				destroy_immediate( child );
		}

		public static class inactive
		{
			public static GameObject position(
				GameObject original, Vector3 pos )
			{
				return _( original, pos );
			}

			public static T parent<T>( T original, Transform parent )
				where T : MonoBehaviour
			{
				T result = _<T>( original );
				result.transform.parent = parent;
				return result;
			}

			public static T parent<T>( T original, GameObject parent_target )
				where T : MonoBehaviour
			{
				return parent<T>( original, parent_target.transform );
			}

			public static T parent<T>(
				T original, Transform parent_target, bool reset_pos )
				where T : MonoBehaviour
			{
				T result = parent<T>( original, parent_target );
				if ( reset_pos ){
					result.transform.localPosition = Vector3.zero;
				}
				return result;
			}

			public static GameObject parent(
				GameObject original, Transform parent_target )
			{
				GameObject result = _( original );
				result.transform.parent = parent_target;
				return result;
			}

			public static GameObject parent(
				GameObject original, GameObject parent_target )
			{
				return parent( original, parent_target.transform );
			}

			public static GameObject parent(
				GameObject original, Transform parent_target, bool reset_pos )
			{
				GameObject result = parent( original, parent_target );
				if ( reset_pos )
					result.transform.localPosition = Vector3.zero;
				return result;
			}

			public static GameObject parent(
				GameObject original, GameObject parent_target, bool reset_pos )
			{
				return parent( original, parent_target.transform, reset_pos );
			}

			public static T _<T>( T obj ) where T: MonoBehaviour
			{
				T result = instantiate._<T>( obj );
				result.gameObject.SetActive( false );
				return result;
			}

			public static T _<T>( T obj, Vector3 pos ) where T : MonoBehaviour
			{
				T result = _<T>( obj );
				result.transform.position = pos;
				return result;
			}

			public static T _<T>( T obj, Vector3 pos, Quaternion rot )
				where T : MonoBehaviour
			{
				T result = _<T>( obj, pos );
				result.transform.rotation = rot;
				return result;
			}

			public static GameObject _( GameObject obj )
			{
				GameObject result = instantiate._( obj );
				result.SetActive( false );
				return result;
			}

			public static GameObject _( GameObject obj, Vector3 pos ){
				GameObject result = _( obj );
				result.transform.position = pos;
				return result;
			}

			public static GameObject _(
				GameObject obj, Vector3 pos, Quaternion rot )
			{
				GameObject result = _( obj, pos );
				result.transform.rotation = rot;
				return result;
			}

			public static GameObject _( GameObject obj, string name )
			{
				GameObject result = _( obj );
				result.name = name;
				return result;
			}

			public static GameObject _( GameObject obj, Transform pos )
			{
				return _( obj, pos.position );
			}
		}
	}
}

