using UnityEngine;

namespace chibi.motor
{
	[ RequireComponent( typeof( Rigidbody ) ) ]
	public class Simple_gravity : Chibi_behaviour
	{
		public Vector3 gravity = new Vector3( 0, -9.8f, 0);

		protected Rigidbody ridgetbody;

		public Vector3 velocity
		{
			get {
				return ridgetbody.velocity;
			}
		}

		public virtual Vector3 desire_velocity
		{
			get {
				return velocity + gravity;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			ridgetbody = GetComponent<Rigidbody>();
			if ( !ridgetbody )
				Debug.Log( string.Format(
					"no se encontro un ridgetbody en el objeco {0}", name ) );
		}

		protected virtual void FixedUpdate()
		{
			ridgetbody.velocity += gravity * Time.deltaTime;
		}
	}
}