using UnityEngine;

namespace SMKD.animator
{
	public class Animator_dodger : chibi.animator.Animator_base
	{
		protected bool _is_dodge, _is_dead, _has_the_ball;
		protected Vector3 _direction;

		public SMKD.motor.Dodger_motor motor;

		#region propiedades public
		public bool is_dodge
		{
			get {
				return _is_dodge;
			}
			set {
				_is_dodge = value;
				animator.SetBool( "is_dodge", _is_dodge );
			}
		}

		public bool is_dead
		{
			get {
				return _is_dead;
			}
			set {
				_is_dead = value;
				animator.SetBool( "is_dead", _is_dead );
			}
		}

		public bool has_the_ball
		{
			get {
				return _has_the_ball;
			}
			set {
				_has_the_ball = value;
				animator.SetBool( "has_the_ball", _has_the_ball );
			}
		}

		public Vector3 direction
		{
			get {
				return _direction;
			}
			set {
				_direction = new Vector3( value.x, value.z, 0 );
				animator.SetFloat( "horizontal", _direction.x );
				animator.SetFloat( "vertical", _direction.y );
			}
		}
		#endregion

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !motor )
			{
				motor = GetComponent<SMKD.motor.Dodger_motor>();
				if ( !motor )
					debug.error( "no se encontro el Dodger_motor" );
			}
		}

		public void on_end_died()
		{
			motor.on_end_died();
		}
	}
}
