using System.Collections.Generic;
using chibi.controller.weapon.gun.bullet;
using chibi.controller.weapon.gun;
using chibi.weapon.gun;
using chibi.controller.weapon.gun.single;

namespace danmaku.controller.weapon.gun
{
	public class Controller_gun_pattern : Controller_gun
	{
		protected chibi.rol_sheet.Rol_sheet _owner;
		public List< Gun > guns;

		public List< Controller_gun_single > controllers_guns;

		public chibi.rol_sheet.Rol_sheet owner
		{
			get {
				return _owner;
			}
			set {
				_owner = value;
				update_owner();
			}
		}

		protected override void _init_cache()
		{
			base._init_cache();
			guns = new List<Gun>( transform.GetComponentsInChildren<Gun>() );
			controllers_guns = new List<Controller_gun_single>(
					transform.GetComponentsInChildren<Controller_gun_single>() );
		}

		protected override void Start()
		{
			update_owner();
		}

		public override List<Controller_bullet> shot()
		{
			List<Controller_bullet> bullets = new List<Controller_bullet>();
			foreach( var controller_gun in controllers_guns )
			{
				var new_bullets = controller_gun.shot();
				if ( new_bullets != null )
					bullets.AddRange( new_bullets );
			}

			foreach ( var bullet in bullets )
				if ( bullet.is_not_ready )
					bullet.ready();
			return bullets;
		}

		public override void start_automatic_shot()
		{
			foreach ( var gun in controllers_guns )
				gun.gun.automatic_shot = true;
		}

		public override void stop_automatic_shot()
		{
			foreach ( var gun in controllers_guns )
				gun.gun.automatic_shot = false;
		}

		protected void update_owner( )
		{
			foreach ( var gun in controllers_guns )
				gun.gun.owner = owner;
		}
	}
}
