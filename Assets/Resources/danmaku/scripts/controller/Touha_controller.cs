using System.Collections.Generic;
using UnityEngine;
using chibi.controller.weapon.gun.bullet;
using danmaku.controller.weapon.gun;

namespace danmaku.controller.npc
{
	public class Touha_controller : chibi.controller.npc.Controller_npc
	{
		public chibi.rol_sheet.Rol_sheet rol;

		public GameObject guns_patter_container;
		public Controller_gun_pattern current_patter;
		public List< Controller_gun_pattern > patters;

		public bool automatic_shot
		{
			get
			{
				return current_patter.automatic_shot;
			}
			set
			{
				current_patter.automatic_shot = value;
			}
		}

		public List<Controller_bullet> shot()
		{
			List<Controller_bullet> bullets = current_patter.shot();
			return bullets;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !rol )
				rol = GetComponent<chibi.rol_sheet.Rol_sheet>();
			if ( !rol )
				debug.error( "no encontro un 'Rol_sheet'" );

			if ( !guns_patter_container )
				debug.error( "no esta asignado el contenedor de los gun patters" );
			find_gun_patters();

			if ( !current_patter )
			{
				debug.warning(
					"no esta asignado el actual patron de disparo se usara "
					+ "el primer elemento en los patroners" );
				current_patter = patters[ 0 ];
			}
			debug.log( this.name + " " + "init_cache" );
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
		}

		protected virtual void find_gun_patters()
		{
			patters = new List<Controller_gun_pattern>(
				guns_patter_container.transform.GetComponentsInChildren<
					Controller_gun_pattern >() );
			if ( patters.Count == 0 )
				debug.error( "no se encontraron 'controller_gun_patters'" );
			foreach ( var pattern in patters )
			{
				pattern.owner = rol;
			}
		}
	}
}
