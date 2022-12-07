using System.Collections.Generic;
using chibi.damage;
using UnityEngine;

namespace chibi.controller.weapon.gun.bullet
{
	public class Controller_bullet : Controller_motor
	{
		public Damage[] damages
		{
			get {
				var damage = GetComponent<Damage>();
				var damages = GetComponentsInChildren<Damage>();
				var result = new List<Damage>( damages );
				if ( damage != null )
					result.Add( damage );
				return result.ToArray();
			}
		}

		public bool is_ready
		{
			get;
			set;
		}

		public bool is_not_ready
		{
			get { return !is_ready; }
		}

		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				base.desire_direction = value;
				motor.desire_speed = motor.max_speed;
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			is_ready = false;
		}

		public void ready()
		{
			motor.enabled = true;
			is_ready = true;
		}
	}
}
