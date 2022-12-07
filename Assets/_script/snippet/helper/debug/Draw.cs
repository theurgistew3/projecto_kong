using UnityEngine;

namespace helper
{
	namespace debug
	{
		namespace draw
		{
			public class Draw
			{
				protected MonoBehaviour _instance;

				public bool debuging
				{
					get {
						var a = _instance as chibi.Chibi_behaviour;
						if ( a )
							return a.debug_mode;
						var b = _instance as chibi.Chibi_behaviour;
						if ( b )
							return b.debug_mode;
						return false;
					}
				}

				public Draw( MonoBehaviour instance )
				{
					_instance = instance;
				}

				public void arrow( Vector3 position, Vector3 direction, float duration=0f )
				{
					if ( debuging )
						helper.draw.arrow.debug( position, direction, duration:duration );
				}

				public void line( Vector3 position, Vector3 to_position )
				{
					if ( debuging )
						UnityEngine.Debug.DrawLine( position, to_position );
				}

				public void arrow(
					Vector3 position, Vector3 direction, Color color,
					float duration=0f )
				{
					if ( debuging )
						helper.draw.arrow.debug(
							position, direction, color, duration:duration );
				}

				public void line(
					Vector3 position, Vector3 to_position, Color color,
					float duration=0f )
				{
					if ( debuging )
						UnityEngine.Debug.DrawLine(
							position, to_position, color, duration:duration );
				}

				public void arrow( Vector3 direction, Color color, float duration=0f )
				{
					arrow( _instance.transform.position, direction, color, duration );
				}

				public void line(
					Vector3 to_position, Color color, float duration=0f )
				{
					line(
						_instance.transform.position, to_position, color,
						duration:duration );
				}

				public void line( Vector3 to_position )
				{
					line( _instance.transform.position, to_position );
				}

				public void arrow( Vector3 direction, float duration=0f )
				{
					arrow( _instance.transform.position, direction, duration:duration );
				}

				public void arrow_to( Vector3 position, Vector3 to_position )
				{
					arrow( position, to_position - position );
				}

				public void arrow_to(
					Vector3 position, Vector3 to_position, Color color )
				{
					arrow( position, to_position - position, color );
				}

				public void arrow_to( Vector3 to_position )
				{
					Vector3 direction = to_position - _instance.transform.position;
					arrow( _instance.transform.position, direction );
				}

				public void arrow_to( Vector3 to_position, Color color )
				{
					Vector3 direction = to_position - _instance.transform.position;
					arrow( _instance.transform.position, direction, color );
				}

				public void sphere(
					Vector3 position, Color color, float radius, float duration=0f,
					bool depth_test=true )
				{
					if ( debuging )
						helper.draw.sphere.debug(
							position, color, radius, duration, depth_test );
				}

				public void cube(
					Vector3 position, Vector3 size, Color color,
					float duration=0f )
				{
					if ( debuging )
						helper.draw.cube.debug(
							position, size, color, duration:duration );
				}
			}
		}
	}
}
