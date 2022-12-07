using UnityEngine;


namespace helper
{
	namespace life
	{
		[CreateAssetMenu(menuName="helper/life/life_span")]
		public class Life_span : chibi.Chibi_object
		{
			public float seconds_of_life = 60f;
		}
	}
}
