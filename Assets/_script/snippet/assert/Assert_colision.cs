using UnityEngine;
using System.Collections.Generic;


namespace helper.test.assert
{
	public class Assert_colision : chibi.Chibi_behaviour
	{
		public List<obj.Assert_collision_event> collisions_enters;
		public List<obj.Assert_collision_event> collisions_exits;

		protected override void Awake()
		{
			base.Awake();
			collisions_enters = new List<obj.Assert_collision_event>();
			collisions_exits = new List<obj.Assert_collision_event>();
		}

		public void assert_collision_enter()
		{
			if ( collisions_enters.Count == 0 )
				raise_a_fail( "ningun collider entro" );
		}

		public void assert_collision_enter( GameObject obj )
		{
			foreach ( var e in this.collisions_enters )
				if ( e.game_object == obj )
					return;
			string msg = string.Format(
				"el gameobject {0} no entro en el collider {1}", obj, name );
			raise_a_fail( msg );
		}

		public void assert_collision_enter( MonoBehaviour obj )
		{
			assert_collision_enter( obj.gameObject );
		}

		public void assert_collision_enter( int amount )
		{
			if ( collisions_enters.Count != amount )
				raise_a_fail( string.Format(
					"el numero de collisiones fueron {0} se esperaban {1} en {2}",
					collisions_enters.Count, amount, name ) );
		}

		public void assert_collision_enter_less_that( int amount )
		{
			if ( collisions_enters.Count >= amount )
				raise_a_fail( string.Format(
					"el numero de collisiones fueron {0} se esperaban menos de {1}" +
					" en {2}",
					collisions_enters.Count, amount, name ) );
		}

		public void assert_collision_enter_less_or_equal_that( int amount )
		{
			if ( collisions_enters.Count >= amount )
				raise_a_fail( string.Format(
					"el numero de collisiones fueron {0} se esperaban menos de {1}" +
					" en {2}",
					collisions_enters.Count, amount, name ) );
		}

		public void assert_not_collision_enter()
		{
			if ( collisions_enters.Count > 0 )
			{
				var list_of_names = build_list_of_names( collisions_enters );
				string names = compact_list_of_names( list_of_names );
				string msg = string.Format(
					"se encontraron colisiones :: {0}", names );
				raise_a_fail( msg );
			}
		}

		public void assert_not_collision_enter( GameObject obj )
		{
			foreach ( var e in collisions_enters )
				if ( e.game_object == obj )
				{
					string msg = string.Format(
						"el gameobject {0} entro en el collider", obj );
					raise_a_fail( msg );
				}
		}

		public void assert_not_collision_exit( GameObject obj )
		{
			foreach ( var e in collisions_exits )
				if ( e.game_object == obj )
				{
					string msg = string.Format(
						"el gameobject {0} nunca salio del collider",
						helper.game_object.name.full( obj ) );
					raise_a_fail( msg );
				}
		}

		public void assert_not_collision_exit( MonoBehaviour obj )
		{
			assert_not_collision_exit( obj.gameObject );
		}

		public void assert_not_collision_enter( MonoBehaviour obj )
		{
			assert_not_collision_enter( obj.gameObject );
		}

		private void OnCollisionEnter( Collision collision )
		{
			collisions_enters.Add( new obj.Assert_collision_event( collision ) );
		}

		private void OnTriggerEnter( Collider other )
		{
			collisions_enters.Add( new obj.Assert_collision_event( other ) );
		}

		private void OnCollisionExit( Collision collision )
		{
			collisions_exits.Add( new obj.Assert_collision_event( collision ) );
		}

		private void OnTriggerExit( Collider other )
		{
			collisions_exits.Add( new obj.Assert_collision_event( other ) );
		}

		private void raise_a_fail( string msg )
		{
			throw new System.Exception( msg );
		}

		protected List<string> build_list_of_names(
			List<obj.Assert_collision_event> collisions )
		{
			List<string> result = new List<string>();
			foreach ( var e in collisions_enters )
			{
				string name = helper.game_object.name.full( e.game_object );
				result.Add( name );
			}
			return result;
		}

		protected string compact_list_of_names( List<string> names )
		{
			return string.Join<string>( ", ", names );
		}
	}
}