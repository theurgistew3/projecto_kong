using System.Collections.Generic;
using chibi.controller.weapon.gun.bullet;


namespace chibi.controller.weapon.gun.single
{
	public class Controller_gun_burst_single : Controller_gun_single
	{
		public override List<Controller_bullet> shot()
		{
			gun.burst();
			return null;
		}
	}
}
