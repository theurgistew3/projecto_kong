using UnityEngine;


namespace weapon
{
	namespace stat
	{
		[ CreateAssetMenu( menuName="weapon/stat/base") ]
		public class Gun_stat : chibi.Chibi_object
		{
			public float rate_fire = 1f;
			public int burst_amount = 1;

			public override string path_of_the_default
			{
				get { return "object/weapon/stat/default"; }
			}
		}
	}
}
