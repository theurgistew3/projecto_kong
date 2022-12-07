using UnityEngine;

namespace fisher.controller
{
	public class Fish_controller : chibi.controller.Controller_motor
	{
		public chibi.radar.Radar_box radar;
		public chibi.rol_sheet.Rol_sheet rol;

		public override void action( string name, string e )
		{
			base.action( name, e );
			switch ( name )
			{
				case chibi.joystick.actions.fire_1:
					switch ( e )
					{
						case chibi.joystick.events.down:
							eat();
							break;
					}
					break;
			}
		}

		public virtual void eat()
		{
			radar.ping();
			foreach ( var hit in radar.hits )
			{
				var item = hit.transform.GetComponent< chibi.inventory.Item >();
				item.use( rol );
				Destroy( item.gameObject );
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube( radar.origin.position, radar.size );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			radar = new chibi.radar.Radar_box( radar );
			rol = GetComponent< chibi.rol_sheet.Rol_sheet >();
			if ( !rol )
				Debug.LogError( string.Format(
					"[fish controller] no encontro un 'Rol_sheet' en '{0}'",
					helper.game_object.name.full( this ) ) );
		}
	}
}