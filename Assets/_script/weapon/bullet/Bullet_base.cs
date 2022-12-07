using UnityEngine;


namespace weapon
{
	namespace bullet
	{
		[ RequireComponent( typeof( Rigidbody ) ) ]
		public class Bullet_base : chibi.Chibi_behaviour
		{
			public float max_speed = 1;

			protected Rigidbody _rigidbody;

			public float current_speed
			{
				get {
					return _rigidbody.velocity.magnitude;
				}
			}

			public Vector3 direction
			{
				get {
					return _rigidbody.velocity.normalized;
				}
			}

			public void shot()
			{
				shot( transform.forward );
			}

			public void shot( Vector3 direction )
			{
				Vector3 velocity = direction * max_speed;
				_rigidbody.velocity = velocity;
			}

			protected override void _init_cache()
			{
				base._init_cache();
				_rigidbody = GetComponent< Rigidbody >();
			}

			protected override void Start()
			{
				base.Start();
				_rigidbody.useGravity = false;
			}
		}
	}
}
