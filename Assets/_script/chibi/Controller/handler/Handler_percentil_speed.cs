using UnityEngine;

namespace chibi.controller.handler
{
	[ CreateAssetMenu( menuName="chibi/controller/handler/speed_persentil" ) ]
	public class Handler_percentil_speed : Handler
	{
		[ Range( 0, 1 ) ]
		public float amount;

		public override void action( Controller_motor controller )
		{
			float max_speed = controller.max_speed;
			float final_value = amount * max_speed;

			switch ( type )
			{
				case types.minus:
					controller.speed -= final_value;
					break;
				case types.sum:
					controller.speed += final_value;
					break;
				case types.set:
					controller.speed = final_value;
					break;
				default:
					Debug.LogError( string.Format(
						"[handler_persentil_speed] no hay definicion "
						+ "para el type: '{0}'",
						type ) );
					throw new System.NotImplementedException();
			}
		}
	}
}
