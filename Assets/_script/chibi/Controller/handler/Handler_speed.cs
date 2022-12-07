using UnityEngine;

namespace chibi.controller.handler
{
	[ CreateAssetMenu( menuName="chibi/controller/handler/speed" ) ]
	public class Handler_speed : Handler
	{
		public float amount;

		public override void action( Controller_motor controller )
		{
			switch ( type )
			{
				case types.minus:
					controller.speed -= amount;
					break;
				case types.sum:
					controller.speed += amount;
					break;
				case types.set:
					controller.speed = amount;
					break;
				default:
					Debug.LogError( string.Format(
						"[handler_speed] no hay definicion para el type: '{0}'",
						type ) );
					throw new System.NotImplementedException();
			}
		}
	}
}
