using UnityEngine;

namespace helper
{
	public static class layer_mask
	{
		public static bool game_object_is_in_mask(
			GameObject obj, LayerMask mask )
		{
			var damage_layer_in_bit = 1 << obj.layer;
			return ( mask.value & damage_layer_in_bit ) > 0;
		}

		public static LayerMask def
		{
			get {
				return LayerMask.GetMask( "Default" );
			}
		}

		public static LayerMask item
		{
			get {
				return LayerMask.GetMask( "item" );
			}
		}
	}
}
