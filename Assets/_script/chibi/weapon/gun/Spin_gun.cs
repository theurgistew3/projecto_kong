using UnityEngine;
using chibi.controller.weapon.gun.bullet;
using chibi.motor.weapons.gun.bullet;

namespace chibi.weapon.gun
{
	public class Spin_gun : Step_gun
	{
		public float angle_by_step = 1f;

		public override void step()
		{
			transform.RotateAround(
				transform.position, transform.up, angle_by_step );
		}
	}
}
