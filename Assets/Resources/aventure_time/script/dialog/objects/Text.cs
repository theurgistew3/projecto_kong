using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace aventure_time.dialog
{
	[ CreateAssetMenu( menuName="aventure_time/dialogue/text" ) ]
	public class Text: Message
	{
		[TextArea( 3, 10 )]
		public string text;
	}
}
