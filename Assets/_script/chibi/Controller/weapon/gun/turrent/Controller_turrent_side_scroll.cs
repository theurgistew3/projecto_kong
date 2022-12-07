using UnityEngine;

namespace chibi.controller.weapon.gun.turrent
{
	public class Controller_turrent_side_scroll : Controller_turrent
	{
		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				//base.desire_direction = value;
				base.desire_direction = new Vector3( value.z, value.y, value.x );
			}
		}
	}
}
