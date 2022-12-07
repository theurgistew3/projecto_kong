using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace aventure_time.dialog
{
	[ CreateAssetMenu( menuName="aventure_time/dialogue/dialogue" ) ]
	public class Dialogue: chibi.Chibi_object
	{
		public List<Message> messages;
	}
}
