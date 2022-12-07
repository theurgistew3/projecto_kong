using UnityEngine;

namespace chibi.controller
{
	public class Controller : Chibi_behaviour
	{
		protected Vector3 _desire_direction;
		protected float _speed;
		public bool is_block = false;

		public virtual Vector3 desire_direction
		{
			get {
				return _desire_direction;
			}

			set {
				if ( !is_block )
					_desire_direction = value;
			}
		}

		public virtual float speed {
			get {
				return _speed;
			}

			set {
				if ( !is_block )
					_speed = value;
			}
		}

		public virtual void direction( chibi.joystick.Axis axi )
		{
			direction( axi.name, axi.vector );
		}

		public virtual void direction( string name, Vector3 direction )
		{
			// debug.info( "direction: {0} {1}", name, direction );
			switch ( name )
			{
				case "desire_direction":
					desire_direction = direction;
					break;
			}
		}

		public virtual void action( string name, string e )
		{
			//debug.log( "action '{0}' with the event '{1}'", name, e );
		}

		public virtual void block()
		{
			is_block = false;
			desire_direction = Vector3.zero;
			speed = 0f;
		}

		public virtual void unblock()
		{
			is_block = true;
		}
	}
}
