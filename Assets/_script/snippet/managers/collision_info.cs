using UnityEngine;

namespace chibi.manager.collision
{
	public class Collision_info {
		public string name;
		public GameObject game_object;
		public UnityEngine.Collision collision;
		public float slope_angle;

		public Collision_info(
				string name, UnityEngine.Collision collision,
				float slope_angle=0f )
			: this( name, collision, collision.gameObject, slope_angle )
		{}

		public Collision_info(
			string name, UnityEngine.Collision collision, GameObject game_object,
			float slope_angle=0f )
		{
			this.name = name;
			this.collision = collision;
			this.game_object = game_object;
			this.slope_angle = slope_angle;
		}
	}
}
