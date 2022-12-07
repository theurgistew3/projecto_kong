using System.Collections.Generic;
using UnityEngine;

namespace chibi.controller.handler
{
	public class Handler_behaviour: Chibi_behaviour { public List<Handler> handlers = new List<Handler>(); public bool is_global = true;
		public List<Controller> affected_controller;

		private void OnTriggerEnter( Collider other )
		{
			var controller = other.transform.GetComponent< Controller_motor >();
			if ( !controller )
			{
				controller = other.GetComponentInParent< Controller_motor >();
			}
			if ( controller && is_in_affected_controllers( controller ) )
			{
				foreach ( var handler in handlers )
				{
					handler.action( controller );
				}
			}
		}

		public bool is_in_affected_controllers( Controller controller )
		{
			if ( is_global )
				return true;
			else
				return affected_controller.Contains( controller );
		}

		public void add( Controller controller )
		{
			affected_controller.Add( controller );
		}
	}
}
