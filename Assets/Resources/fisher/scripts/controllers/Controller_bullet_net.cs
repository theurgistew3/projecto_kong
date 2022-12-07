using chibi.controller.weapon.gun.bullet;
using fisher.motor.weapons.gun.bullet;

namespace fisher.controller
{
	public class Controller_bullet_net : Controller_bullet_yoyo
	{
		public Bullet_motor_net net_motor
		{
			get {
				return motor as Bullet_motor_net;
			}
		}
		public virtual Fisher_controller owner
		{
			get {
				return net_motor.owner;
			}
			set {
				net_motor.owner = value;
			}
		}
	}
}
