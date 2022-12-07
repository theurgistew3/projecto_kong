using UnityEngine;
using chibi.motor.weapons.gun.bullet;

namespace chibi.controller.weapon.gun.bullet
{
	public class Controller_bullet_yoyo : Controller_bullet
	{
		protected Bullet_motor_yoyo yoyo_motor
		{
			get {
				return motor as Bullet_motor_yoyo;
			}
		}

		public Transform target
		{
			get {
				return yoyo_motor.target;
			}
			set {
				yoyo_motor.target = value;
			}
		}

		public Transform origin
		{
			get {
				return yoyo_motor.origin;
			}
			set {
				yoyo_motor.origin = value;
			}
		}
	}
}
