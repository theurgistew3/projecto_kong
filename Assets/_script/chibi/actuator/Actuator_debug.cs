using UnityEngine;

namespace chibi.actuator
{
	public class Actuator_debug : Actuator
	{

		public override void action( controller.Controller controller )
		{
			Debug.Log(
				string.Format( "[Actuador] {0} acionado por {1}",
				helper.game_object.name.full( this ),
				helper.game_object.name.full( controller ) ) );
		}
	}
}