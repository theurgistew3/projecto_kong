using UnityEngine;

namespace snippet
{
	namespace objects
	{
		public class Position {

			protected Vector2 _min, _max;
			protected Transform _transform;
			protected Bounds _bounds;

			public Position( Transform transform, Bounds bounds ) {
				_transform = transform;
				this.bounds = bounds;
			}

			public Bounds bounds {
				get {
					return _bounds;
				}
				set {
					_bounds = value;
					_min = _transform.InverseTransformPoint( bounds.min );
					_max = _transform.InverseTransformPoint( bounds.max );
				}
			}

			public Vector3 top {
				get {
					return new Vector2( 0,  _max.y );
				}
			}
			public Vector3 bottom {
				get {
					return new Vector2( 0, _min.y );
				}
			}

			public Vector3 rigth {
				get {
					return new Vector2( _max.x, 0 );
				}
			}
			public Vector3 left {
				get {
					return new Vector2( _min.x, 0 );
				}
			}
		}
	}
}