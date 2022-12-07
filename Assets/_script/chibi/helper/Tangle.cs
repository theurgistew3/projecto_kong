using UnityEngine;

namespace chibi.tool.tangle
{
	public class Tangle : Chibi_behaviour
	{
		public GameObject obj_1, obj_2;

		protected void Update()
		{
			if ( !obj_1 )
				GameObject.Destroy( obj_2 );
			if ( !obj_2 )
				GameObject.Destroy( obj_1 );
		}

		protected void OnDestroy()
		{
			GameObject.Destroy( obj_2 );
			GameObject.Destroy( obj_1 );
		}
	}
}
