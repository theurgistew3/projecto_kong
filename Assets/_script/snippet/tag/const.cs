using UnityEngine;


namespace chibi.tag
{
	public class consts
	{
		public static bool is_scenary( GameObject obj )
		{
			return obj.tag == scenary;
		}

		public static bool is_scenary( Collision obj )
		{
			return obj.gameObject.tag == scenary;
		}

		public static string scenary
		{
			get {
				return "scenery";
			}
		}

		public static string player
		{
			get {
				return "Player";
			}
		}
	}
}
