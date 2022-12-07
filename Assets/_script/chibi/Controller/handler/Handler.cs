using UnityEngine;

namespace chibi.controller.handler
{
	public enum types { sum, minus, set }

	[ CreateAssetMenu( menuName="chibi/controller/handler/base" ) ]
	public class Handler : Chibi_object
	{
		public types type;

		public virtual void action( Controller_motor controller_motor )
		{
			throw new System.NotImplementedException();
		}
	}
}
