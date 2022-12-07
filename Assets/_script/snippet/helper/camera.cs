using UnityEngine;

namespace helper
{
	public static class camera
	{
		public static Vector3 get_mouse_to_world_position( Vector3 screen_position, Camera camera )
		{
			return camera.ScreenToWorldPoint( screen_position );
		}
	}
}
