using UnityEngine;

namespace helper
{
	public static class clone
	{
		public static Vector3 _( Vector3 v )
		{
			return new Vector3( v.x, v.y, v.z );
		}
	}
}
