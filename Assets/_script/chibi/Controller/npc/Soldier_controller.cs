using System.Collections.Generic;
using UnityEngine;
using chibi.controller.weapon.gun.bullet;
using chibi.rol_sheet;

namespace chibi.controller.npc
{
	public class Soldier_controller : Controller
	{
		public chibi.controller.weapon.gun.turrent.Controller_turrent turrent;
		public chibi.controller.npc.Controller_npc npc;
		public Transform hold_turrent_position;
		public actuator.Actuador_controller actuator_controller;

		public Rol_sheet rol;

		public bool is_using_turrent = false;

		#region funciones de controller
		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				if ( is_using_turrent )
					turrent.desire_direction = value;
				else
					npc.desire_direction = value;
			}
		}

		public override float speed
		{
			get {
				return base.speed;
			}

			set {
				if ( is_using_turrent )
					turrent.speed = value;
				else
					npc.speed = value;
			}
		}
		#endregion

		#region controlles de torreta
		public void release_turrent()
		{
			turrent.owner = null;
			is_using_turrent = false;
		}

		public void grab_turrent()
		{
			if ( turrent )
			{
				turrent.owner = rol;
				is_using_turrent = true;
			}
		}

		public List< Controller_bullet > shot()
		{
			if ( is_using_turrent )
				return turrent.shot();
			return null;
		}
		#endregion

		protected override void _init_cache()
		{
			base._init_cache();
			rol = GetComponent< Rol_sheet >();
			if ( !rol )
				Debug.LogError( string.Format(
					"[soldier controller] no encontro un 'Rol_sheet' en {0}",
					helper.game_object.name.full( this ) ) );
			load_actuator_controller();
		}

		public virtual void activate()
		{
			if ( actuator_controller )
			{
				actuator_controller.action();
			}
			else
			{
				Debug.LogError(
					string.Format(
						"no esta asignado el actuator controller en '{0}'",
						helper.game_object.name.full( this ) ) );
			}
		}

		protected virtual void load_actuator_controller()
		{
			// TODO: deberia de haber una mejor manera de hacer esto
			if ( !actuator_controller )
			{
				actuator_controller = GetComponent<actuator.Actuador_controller>();
				if ( !actuator_controller )
				{
					actuator_controller =
						GetComponentInChildren<actuator.Actuador_controller>();
					if ( !actuator_controller )
					{
						Debug.LogWarning(
							string.Format(
								"no se econtro un controller de actuadores en '{0}'",
								helper.game_object.name.full( this ) ) );
					}
				}
			}
			if ( actuator_controller )
				actuator_controller.controller = this;
			else
				Debug.LogWarning(
					string.Format(
						"no hay un actuador controller en '{0}'",
						helper.game_object.name.full( this ) ) );

		}
	}
}
