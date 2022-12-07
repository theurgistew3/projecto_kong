using UnityEngine;

namespace helper.game_object
{
	public class name
	{
		public static string full( GameObject obj )
		{
			return obj.name;
		}

		public static string full( MonoBehaviour mono )
		{
			return full( mono.gameObject );
		}
	}
}