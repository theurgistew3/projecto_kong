using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace chibi.controller.handler
{
	public class Handler_behaviour_delay: Chibi_behaviour
	{
		public List<Handler> handlers = new List<Handler>();
		public float delay;

		protected IEnumerator __delay;

		protected virtual IEnumerator do_delay()
		{
			yield return new WaitForSeconds( delay );

			var controller = GetComponent< Controller_motor >();
			if ( !controller )
			{
				controller = GetComponentInParent< Controller_motor >();
			}

			foreach ( var handler in handlers )
				handler.action( controller );

			Destroy( this );
		}

		public void run()
		{
			__delay = do_delay();
			StartCoroutine( __delay );
		}
	}
}
