using chibi.controller.weapon.gun.bullet;
using chibi.motor.weapons.gun.bullet;
using UnityEngine;

namespace chibi.weapon.gun
{
	public class Orbital_gun : Step_gun
	{
		public int steps = 100;

		[ Range( 0, 1 ) ]
		public float period = 0f;
		[ Range( 0, 1 ) ]
		public float delta_period = 0.1f;
		public float radius_x = 1f;
		public float radius_z = 1f;

		public override void step()
		{
			var desire = helper.shapes.Ellipse.evaluate(
				radius_x, radius_z, period );
			period += delta_period;
			desire += auto_aim_target.Value.transform.position;
			transform.position = desire;
		}
	}
}
