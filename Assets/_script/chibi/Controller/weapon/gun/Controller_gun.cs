using System.Collections.Generic;
using chibi.controller.weapon.gun.bullet;

namespace chibi.controller.weapon.gun
{
	public class Controller_gun : Controller
	{
		protected bool _automatic_shot = false;

		public bool automatic_shot
		{
			get
			{
				return _automatic_shot;
			}
			set
			{
				_automatic_shot = value;
				if ( automatic_shot )
					start_automatic_shot();
				else
					stop_automatic_shot();
			}
		}

		public virtual List<Controller_bullet> shot()
		{
			//debug.info( "shot" );
			throw new System.NotImplementedException();
		}

		public virtual List<Controller_bullet> shot( bool commit )
		{
			var bullets = shot();
			if ( commit  )
				foreach ( var bullet in bullets )
					bullet.ready();
			return bullets;
		}

		public virtual void start_automatic_shot()
		{
			throw new System.NotImplementedException();
		}

		public virtual void stop_automatic_shot()
		{
			throw new System.NotImplementedException();
		}
	}
}
