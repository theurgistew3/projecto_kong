using UnityEngine;
using System.Collections.Generic;

namespace chibi.pool
{
	[ CreateAssetMenu( menuName="chibi/pool/behavior" ) ]
	public class Pool_behaviour : chibi.Chibi_object
	{
		public GameObject prefab;
		public Transform container;
		protected Stack<GameObject> _pool_stack;

		public string container_name
		{
			get {
				return "generic pool";
			}
		}

		public int count
		{
			get {
				return _pool_stack.Count;
			}
		}

		protected virtual void OnEnable()
		{
			//Debug.Log( "WTF" );
			_pool_stack = new Stack<GameObject>();
			if ( !container )
			{
				var generic_pool = helper.game_object.prepare.stuff_container(
					container_name ).transform;

				container = helper.game_object.prepare.stuff_container(
					name, generic_pool ).transform;
			}
		}

		public virtual GameObject pop()
		{
			GameObject result = null;
			var stack = _pool_stack;
			if ( count > 0 )
				result = _pool_stack.Pop();
			if ( result == null )
				result = instantiate();
			result.transform.parent = null;
			return result;
		}

		public virtual void push( GameObject obj )
		{
			move_to_container( obj );
			_pool_stack.Push( obj );
		}

		public virtual void push( MonoBehaviour obj )
		{
			push( obj.gameObject );
		}

		public virtual void move_to_container( GameObject obj )
		{
			obj.gameObject.SetActive( false );
			obj.transform.parent = container;
		}

		protected GameObject instantiate()
		{
			if ( prefab == null )
			{
				Debug.LogError( "no tiene prefab defino" );
				return null;
			}
			var obj = helper.instantiate._( prefab );
			return obj;
		}
	}
}
