using System;
using UnityEngine;

namespace chibi.animator
{
	public class Animator_side_scroll : Animator_base
	{
		public float speed
		{
			get { return animator.GetFloat( "speed" ); }
			set { animator.SetFloat( "speed", Math.Abs( value ) ); }
		}

		public float vertical_speed
		{
			get { return animator.GetFloat( "vertical_speed" ); }
			set { animator.SetFloat( "vertical_speed", value ); }
		}

		public bool is_grounded
		{
			get { return animator.GetBool( "is_grounded" ); }
			set { animator.SetBool ( "is_grounded", value ); }
		}

		public bool is_walled
		{
			get { return animator.GetBool( "is_walled" ); }
			set { animator.SetBool ( "is_walled", value ); }
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
	}
}
