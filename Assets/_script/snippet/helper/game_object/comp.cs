using UnityEngine;

namespace helper
{
	namespace game_object
	{
		public class comp
		{
			public static bool is_null( Object obj )
			{
				return obj == null || obj.ToString() == "null";
				/* basura que encontre en internet
				Component c = obj as Component;
				return !( ( c == null || c.gameObject ) && obj );
				*/
			}
		}
	}
}