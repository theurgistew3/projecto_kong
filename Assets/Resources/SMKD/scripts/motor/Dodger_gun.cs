using chibi.controller.weapon.gun.bullet;
using chibi.weapon.gun;

namespace SMKD.weapon.gun
{
	public class Dodger_gun : Linear_gun
	{
		public Controller_bullet bullet;
		public float distan_of_the_shot = 0.5f;

		public bool is_load = false;

		/*
		public override Controller_bullet shot()
		{
			if ( !bullet )
				Debug.LogError( string.Format(
					"[Dodger_gun] no tiene bala",
					helper.game_object.name.full( this ) ), this.gameObject );

			var bullet_position = ( direction_shot.normalized * distan_of_the_shot )
				+ transform.position;
			bullet.transform.position = bullet_position;
			bullet.desire_direction = direction_shot;
			var result = bullet;
			bullet = null;
			return result;
		}
		*/

		public override Controller_bullet shot()
		{
			if ( is_load )
			{
				var bullet = base.shot();
				var bullet_position = (
					direction_shot.normalized * distan_of_the_shot )
					+ transform.position;
				bullet.transform.position = bullet_position;
				is_load = false;
				bullet.motor.enabled = true;
				return bullet;
			}
			return null;
		}

		public void load()
		{
			is_load = true;
		}
	}
}
