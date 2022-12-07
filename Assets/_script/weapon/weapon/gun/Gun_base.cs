using weapon.stat;
using weapon.ammo;


namespace weapon
{
	namespace gun
	{
		public abstract class Gun_base : weapon.Weapon_base
		{
			public Gun_stat stat;
			public Ammo ammo;

			protected bool _continue_shotting = false;
			public bool continue_shotting
			{
				get { return _continue_shotting; }
				set {
					_continue_shotting = value;
					if ( value )
						start_shoting();
				}
			}

			public override void attack()
			{
				// shot();
			}

			/*
			public abstract Bullet_controller_3d shot();
			public abstract Bullet_controller_3d shot(
				rol_sheet.Rol_sheet owner );
			*/

			protected override void _init_cache()
			{
				base._init_cache();
				if ( ammo == null )
				{
					ammo = load_default_ammo() as Ammo;
				}
				if ( stat == null )
				{
					stat = load_default_stat() as Gun_stat;
				}
			}

			public virtual void start_shoting()
			{
				// shot( owner );
				if ( continue_shotting )
					Invoke( "start_shoting", 1 / stat.rate_fire );
			}

			// TODO: stop_shotting
			public virtual void stop_shotting()
			{
				continue_shotting = false;
			}

			protected virtual chibi.Chibi_object load_default_ammo()
			{
				return Ammo.CreateInstance<Ammo>().find_default<Ammo>();
			}

			protected virtual chibi.Chibi_object load_default_stat()
			{
				return Gun_stat.CreateInstance<Gun_stat>()
					.find_default<Gun_stat>();
			}
		}
	}
}
