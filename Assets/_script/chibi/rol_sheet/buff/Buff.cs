using UnityEngine;


namespace chibi.rol_sheet.buff
{
	[ CreateAssetMenu( menuName="chibi/rol sheet/buff/base" ) ]
	public abstract class Buff : chibi.Chibi_object
	{
		public float delta = 0.05f;
		public float duration = 1f;
		public bool no_duration_limit = false;

		public virtual void effect_in_rol_sheet( Rol_sheet rol_sheet )
		{
			Debug.Log( string.Format(
				"[Buff] se calcula el efecto de '{0}' a {1}",
				name, helper.game_object.name.full( rol_sheet ) ),
				rol_sheet.gameObject );
		}

		public virtual void attach( Rol_sheet rol_sheet )
		{
			Debug.Log( string.Format(
				"[Buff] se agrega el buffo '{0}' a {1}",
				name, helper.game_object.name.full( rol_sheet ) ),
				rol_sheet.gameObject );
		}

		public virtual void unattach( Rol_sheet rol_sheet )
		{
			Debug.Log( string.Format(
				"[Buff] se remueve el buffo '{0}' a {1}",
				name, helper.game_object.name.full( rol_sheet ) ),
				rol_sheet.gameObject );
		}
	}
}