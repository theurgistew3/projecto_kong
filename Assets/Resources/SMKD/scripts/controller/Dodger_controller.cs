using System.Collections.Generic;
using UnityEngine;
using chibi.controller.weapon.gun.bullet;

namespace SMKD.controller.npc
{
	public class Dodger_controller : chibi.controller.npc.Controller_npc
	{
		public chibi.rol_sheet.Rol_sheet rol;
		public SMKD.tool.Dodger_set dodger_set;

		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				if ( value.magnitude < 0.2 )
					base.desire_direction = transform.forward;
				else
					base.desire_direction = value;
			}
		}

		public SMKD.motor.Dodger_motor dodger_motor
		{
			get {
				return motor as SMKD.motor.Dodger_motor;
			}
		}

		#region funciones de controller
		public override void action( string name, string e )
		{
			if ( !dodger_motor.is_dead )
			{
				//base.action( name, e );
				switch ( name )
				{
					case chibi.joystick.actions.fire_1:
						switch ( e )
						{
							case chibi.joystick.events.down:
								shot();
								break;
						}
						break;
					case chibi.joystick.actions.fire_2:
						switch ( e )
						{
							case chibi.joystick.events.down:
								dodge();
								break;
						}
						break;
				}
			}
		}
		#endregion

		public List< Controller_bullet > shot()
		{
			if ( !dodger_motor.is_dead )
				return dodger_motor.shot();
			return null;
		}

		public void dodge()
		{
			if ( !dodger_motor.is_dead )
				dodger_motor.dodge();
		}

		public virtual void grab_ball( Transform transform_ball )
		{
			if ( !dodger_motor.is_dead )
				dodger_motor.grab_ball( transform_ball );
		}

		public virtual void dodge_ball( Transform transform_ball )
		{
			if ( !dodger_motor.is_dead )
				dodger_motor.dodge_ball( transform_ball );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			rol = GetComponent< chibi.rol_sheet.Rol_sheet >();
			if ( !rol )
				debug.error( "no encontro un 'Rol_sheet'" );
			dodger_set.add( this );
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			dodger_set.remove( this );
		}
	}
}
