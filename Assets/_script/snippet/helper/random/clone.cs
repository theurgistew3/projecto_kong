using UnityEngine;

namespace helper.random {
	public static class quaternion
	{
		public static Quaternion _()
		{
			return Quaternion.Euler(
				Random.Range( 0, 360 ), Random.Range( 0, 360 ),
				Random.Range( 0, 360 ) 
			);
		}
	}
}
