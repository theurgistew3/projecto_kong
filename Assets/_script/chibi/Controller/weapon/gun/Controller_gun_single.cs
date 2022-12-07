using System.Collections.Generic;
using chibi.weapon.gun;
using chibi.controller.weapon.gun.bullet;


namespace chibi.controller.weapon.gun.single
{
	public class Controller_gun_single : Controller_gun
	{
		public Gun gun;
		public bool burst_mode = false;

		public override List<Controller_bullet> shot()
		{
			if ( burst_mode )
			{
				gun.burst();
				return null;
			}
			else
				return new List<Controller_bullet>{ gun.shot() };
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !gun )
				gun = GetComponent< chibi.weapon.gun.Gun >();
			if ( !gun )
				debug.error( "no se encontro un 'Gun'" );
		}
	}
}
