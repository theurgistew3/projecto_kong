using UnityEngine;
using danmaku.controller.npc;

namespace chibi.controller.handler
{
	[ CreateAssetMenu( menuName="chibi/controller/handler/start_automatic_shot" ) ]
	public class Handler_start_automatic_shot : Handler
	{
		public override void action( Controller_motor controller )
		{
			var controller_danmaku = controller as Touha_controller;
			if ( controller_danmaku )
				controller_danmaku.automatic_shot = true;
		}
	}
}
