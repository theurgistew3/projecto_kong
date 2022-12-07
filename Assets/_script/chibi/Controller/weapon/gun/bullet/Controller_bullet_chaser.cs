using UnityEngine;

namespace chibi.controller.weapon.gun.bullet
{
	public class Controller_bullet_chaser : Controller_bullet
	{
		public Transform target;

		private void Update()
		{
			if ( !target )
			{
				Debug.LogError( string.Format(
					"[Controller_bullet_chaser] la bala '{0}' no tiene objetivo",
					helper.game_object.name.full( this ), gameObject ) );
				return;
			}
			desire_direction = target.position - transform.position;
		}
	}
}
