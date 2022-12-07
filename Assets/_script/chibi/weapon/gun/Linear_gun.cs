using chibi.controller.weapon.gun.bullet;
using chibi.motor.weapons.gun.bullet;

namespace chibi.weapon.gun
{
	public class Linear_gun : Gun
	{
		public override Controller_bullet shot()
		{
			if ( auto_aim_target.Value != null )
				aim_to( auto_aim_target.Value );
			Bullet_motor bullet;
			if ( position_of_shot )
				bullet = ammo.instanciate( position_of_shot.position, owner );
			else
				bullet = ammo.instanciate( transform.position, owner );
			var controller = bullet.GetComponent<Controller_bullet>();
			controller.desire_direction = direction_shot;
			return controller;
		}
	}
}
