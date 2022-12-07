using UnityEngine;


namespace helper.test.assert
{
	namespace obj
	{

		public class Assert_collision_event
		{
			public GameObject game_object;

			public Assert_collision_event( GameObject game_object )
			{
				this.game_object = game_object;
			}

			public Assert_collision_event( Collision collision )
			{
				this.game_object = collision.gameObject;
			}

			public Assert_collision_event( Collider collider )
			{
				this.game_object = collider.gameObject;
			}
		}
	}
}