using UnityEngine;
using danmaku.controller.npc;

namespace chibi.controller.handler
{
	[ CreateAssetMenu( menuName="chibi/controller/handler/recycle" ) ]
	public class Handler_recycle : Handler
	{
		public override void action( Controller_motor controller )
		{
			controller.recycle();
		}
	}
}
