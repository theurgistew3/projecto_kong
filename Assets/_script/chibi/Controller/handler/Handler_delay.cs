using System.Collections.Generic;
using UnityEngine;

namespace chibi.controller.handler
{
	[ CreateAssetMenu( menuName="chibi/controller/handler/delay" ) ]
	public class Handler_delay : Handler
	{
		public List<Handler> handlers = new List<Handler>();
		public float delay = 1f;

		public override void action( Controller_motor controller )
		{
			var handler_behaviour_delay = controller.gameObject.AddComponent<
				Handler_behaviour_delay>();
			handler_behaviour_delay.handlers = handlers;
			handler_behaviour_delay.delay = delay;
			handler_behaviour_delay.run();
		}
	}
}
