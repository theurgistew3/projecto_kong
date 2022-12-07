using System.Collections.Generic;
using UnityEngine;
using chibi.controller.avatar;

namespace aventure_time.dialog
{
	public enum Owner
	{
		emiter,
		receptor
	}

	public class Message : chibi.Chibi_object
	{
		public Owner owner;
	}
}
