using UnityEngine;

namespace chibi.actuator
{
	public class Teleporter : Actuator
	{
		public Transform destiny;

		public override void action( controller.Controller controller )
		{
			controller.transform.position = destiny.position;
		}
	}
}