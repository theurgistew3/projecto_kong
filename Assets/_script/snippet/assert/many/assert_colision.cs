using UnityEngine;
using System.Collections.Generic;


namespace helper.test.assert.many
{
	public static class assert_colision
	{
		public static void assert_collision_enter(
			params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_collision_enter();
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_collision_enter(
			GameObject obj, params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_collision_enter( obj );
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_collision_enter(
			MonoBehaviour obj, params Assert_colision[] list )
		{
			assert_collision_enter( obj.gameObject, list );
		}

		public static void assert_collision_enter(
			int amount, params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_collision_enter( amount );
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_collision_enter_less_that(
			int amount, params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_collision_enter_less_that( amount );
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_collision_enter_less_or_equal_that(
			int amount, params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_collision_enter_less_or_equal_that( amount );
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_not_collision_enter( params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_not_collision_enter();
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_not_collision_enter(
			GameObject obj, params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_not_collision_enter( obj );
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_not_collision_exit(
			GameObject obj, params Assert_colision[] list )
		{
			List<string> results = new List<string>( list.Length );
			foreach( var assert in list )
				try
				{
					assert.assert_not_collision_exit( obj );
				}
				catch ( System.Exception e )
				{
					results.Add( e.Message );
				}
			raise_a_fail( compact_list_of_names( results ) );
		}

		public static void assert_not_collision_exit(
			MonoBehaviour obj, params Assert_colision[] list )
		{
			assert_not_collision_exit( obj.gameObject, list );
		}

		public static void assert_not_collision_enter(
			MonoBehaviour obj, params Assert_colision[] list )
		{
			assert_not_collision_enter( obj.gameObject, list );
		}

		private static void raise_a_fail( string msg )
		{
			if ( msg.Length > 0 )
				throw new System.Exception( msg );
		}

		private static string compact_list_of_names( List<string> names )
		{
			return string.Join<string>( ", ", names );
		}
	}
}