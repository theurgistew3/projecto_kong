using System.Collections;
using UnityEngine;
using System;
using weapon.stat;
using weapon.ammo;

using chibi.controller.weapon.gun.bullet;


namespace chibi.weapon.gun
{
	public enum Additipe_type
		{ add, minus, set }

	[Serializable]
	public class Burst_by_bullet_option
	{
		public bool enable = false;
		public float start_speed = 1f;
		public float step_speed = 1f;
		public Additipe_type speed_additype_type;

		protected float current_speed = 0f;

		public void update( Controller_bullet bullet )
		{
			if ( !enable )
				return;
			bullet.speed = current_speed;
			switch( speed_additype_type )
			{
				case Additipe_type.add:
					current_speed += step_speed;
					break;
				case Additipe_type.minus:
					current_speed -= step_speed;
					break;
				case Additipe_type.set:
					current_speed = step_speed;
					break;
			}
		}

		public void reset()
		{
			current_speed = start_speed;
		}
	}
}
