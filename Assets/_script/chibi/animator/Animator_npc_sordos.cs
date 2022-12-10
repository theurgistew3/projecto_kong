using UnityEngine;

namespace traductor.animator
{
	public class Animator_npc_sordos : chibi.animator.Animator_base
	{
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
			get
			{
				var x = animator.GetFloat("horizontal");
				var z = animator.GetFloat("vertical");
				return new Vector3(x, 0, z);
			}
			set
			{
				var dir = new Vector3(value.x, value.z, 0);
				animator.SetFloat("horizontal", dir.x);
				animator.SetFloat("vertical", dir.y);
			}
		}

		public int letra
		{
			get
			{
				return animator.GetInteger("letra");
			}
			set
			{
				animator.SetInteger("letra", value);
			}
		}
	}
}
