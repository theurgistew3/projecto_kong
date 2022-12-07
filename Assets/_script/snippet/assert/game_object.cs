using UnityEngine;


namespace tests_tool
{
	namespace assert
	{
		public class game_object
		{
			public static void is_null( GameObject obj )
			{
				if ( !helper.game_object.comp.is_null( obj ) )
					raise_a_fail( string.Format( "El object es no es null" ) );
			}

			public static void is_null( MonoBehaviour obj )
			{
				is_null( obj.gameObject );
			}

			public static void is_not_null( GameObject obj )
			{
				if ( helper.game_object.comp.is_null( obj ) )
					raise_a_fail( string.Format( "El object es null" ) );
			}

			public static void is_not_null( MonoBehaviour obj )
			{
				is_not_null( obj.gameObject );
			}

			private static void raise_a_fail( string msg )
			{
				throw new System.Exception( msg );
			}
		}
	}
}