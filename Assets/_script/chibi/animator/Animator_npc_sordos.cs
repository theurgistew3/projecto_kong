using UnityEngine;

namespace chibi.animator
{
	public class Animator_npc_sordos : Animator_base
	{
		public int letra = 0;
		public chibi.pomodoro.Pomodoro_obj pomodoro;
		public float speed
		{
			get{
				return animator.GetFloat( "speed" );
			}
			set {
				animator.SetFloat( "speed", value );
			}
		}

		public Vector3 direction
		{
			get {
				var x = animator.GetFloat( "horizontal" );
				var z = animator.GetFloat( "vertical" );
				return new Vector3( x, 0, z );
			}
			set {
				var dir = new Vector3( value.x, value.z, 0 );
				animator.SetFloat( "horizontal", dir.x );
				animator.SetFloat( "vertical", dir.y );
			}
		}

		protected virtual void FixedUpdate()
		{
			if ( pomodoro.tick() )
			{
				letra++;
				pomodoro.reset();
			}
			animator.SetInteger( "letra", letra );
		}
	}
}
