using UnityEngine;
using System.Collections.Generic;
using weapon.ammo;
using chibi.motor.weapons.gun.bullet;

namespace singleton
{
	namespace object_pool
	{
		public class Ammo_pool : Singleton<Ammo_pool>
		{
			public string container_name
			{
				get {
					return "ammo_pool";
				}
			}

			public Transform container;

			protected Dictionary<Ammo, Stack<Bullet_motor>> _dict;

			public Stack<Bullet_motor> this[ Ammo key ] {
				get {
					Stack<Bullet_motor> result;
					_dict.TryGetValue( key, out result );
					return result;
				}
			}

			protected virtual void OnEnable()
			{
				_dict = new Dictionary<Ammo, Stack<Bullet_motor>>();
				if ( !container )
					container = helper.game_object.prepare.stuff_container(
						container_name ).transform;
			}

			public virtual Bullet_motor pop( Ammo key )
			{
				Bullet_motor result = null;
				if ( _dict.ContainsKey( key ) )
				{
					var stack = _dict[ key ];
					if ( stack.Count > 0 )
						result = stack.Pop();
				}
				if ( result == null )
				{
					// result = helper.instantiate.inactive._( key );
					result = instantiate( key );
				}
				else
				{
					result.transform.parent = null;
					result.gameObject.SetActive( true );
				}
				return result;
			}

			public virtual void push( Bullet_motor value )
			{
				Ammo key = get_key( value );
				move_to_container( value );
				if ( _dict.ContainsKey( key ) )
					_dict[ key ].Push( value );
				else
				{
					Stack<Bullet_motor> stack_tmp =
						new Stack< Bullet_motor>();
					stack_tmp.Push( value );
					_dict.Add( key, stack_tmp );
				}
			}

			protected virtual void move_to_container( Bullet_motor obj )
			{
				obj.transform.parent = container;
				obj.gameObject.SetActive( false );
			}

			public Bullet_motor instantiate( Ammo key )
			{
				var prefab = key.prefab_bullet;
				if ( prefab == null )
				{
					Debug.Log( string.Format(
						"el Ammo ( '{0}' ) no tiene prefab defino", key.name ) );
					return null;
				}
				else
				{
					Bullet_motor obj = helper.instantiate._( prefab );
					obj.ammo = key;
					return obj;
				}
			}

			public void clean_container_immediate()
			{
				while ( container.childCount > 0 )
				{
					var child = container.GetChild( 0 );
					GameObject.DestroyImmediate( child.gameObject );
				}
				_dict = new Dictionary<Ammo, Stack<Bullet_motor>>();
			}

			protected Ammo get_key( Bullet_motor value )
			{
				var motor = value.GetComponent<Bullet_motor>();
				return motor.ammo;
			}
		}
	}
}
