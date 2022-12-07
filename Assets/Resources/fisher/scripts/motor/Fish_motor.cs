using chibi.motor.npc;

namespace fisher.motor.weapons.gun.bullet
{
	public class Fish_motor : Motor_isometric
	{
		protected override void update_motion()
		{
			base.update_motion();
			transform.LookAt(
				transform.position + ridgetbody.velocity );
		}
	}
}
