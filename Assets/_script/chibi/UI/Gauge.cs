using UnityEngine;


namespace chibi.UI.chicken_o_meter
{
	[ CreateAssetMenu( menuName="chibi/ui/chicken_o_meter/gauge" ) ]
	public class Gauge : Chibi_object
	{
		public float max = 1f;
		public float current = 1f;

		public bool is_empty
		{
			get {
				return current <= 0;
			}
		}

		public bool is_full
		{
			get {
				return current >= max;
			}
		}
	}
}
