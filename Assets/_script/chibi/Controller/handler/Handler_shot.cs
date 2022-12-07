using UnityEngine;
using danmaku.controller.npc;

namespace chibi.controller.handler
{
	[ CreateAssetMenu( menuName="chibi/controller/handler/shot" ) ]
	public class Handler_shot : Handler
	{
		public override void action( Controller_motor controller )
		{
			var controller_danmaku = controller as Touha_controller;
			if ( controller_danmaku )
				controller_danmaku.shot();
		}
	}
}
