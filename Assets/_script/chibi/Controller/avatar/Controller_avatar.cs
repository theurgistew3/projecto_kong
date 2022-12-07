using UnityEngine;
using chibi.dialog;
using chibi.animator.avatar;

namespace chibi.controller.avatar
{
	public class Controller_avatar : Controller_motor
	{
		public animator.avatar.Animator_avatar animator;

		#region propiedades publicas
		#endregion

		#region functiones publicas
		public virtual void set_properties( Actor_propeties property )
		{
			if ( !animator )
				Debug.LogError( string.Format(
					"el '{0}' no tiene asignado el animator del avatar",
					helper.game_object.name.full( this ) ) );
			animator.set_properties( property );
		}
		#endregion

		protected override void _init_cache()
		{
			base._init_cache();
			load_animators();
		}

		protected virtual void load_animators()
		{
			animator = GetComponent<Animator_avatar>();
			if ( !animator)
			{
				Debug.LogError( string.Format(
					"no se encontro el animator de avatar en '{0}'",
					helper.game_object.name.full( this ) ) );
			}
		}
	}
}
