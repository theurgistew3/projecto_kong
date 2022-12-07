using chibi.controller.weapon.gun.bullet;
using chibi.motor.weapons.gun.bullet;

namespace chibi.weapon.gun
{
	public abstract class Step_gun : Linear_gun
	{
		public bool move_with_shots = true;

		public override Controller_bullet shot()
		{
			if ( move_with_shots )
			{
				step();
			}
			var result = base.shot();
			return result;
		}

		public abstract void step();
	}
}
