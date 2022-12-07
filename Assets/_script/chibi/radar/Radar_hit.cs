using UnityEngine;

namespace chibi.radar
{
	public class Radar_hit
	{
		public Transform transform;
		public float distance;

		public Radar_hit( Transform obj, float distance )
		{
			this.transform = obj;
			this.distance = distance;
		}

		public Radar_hit( RaycastHit hit )
		{
			this.transform = hit.transform;
			this.distance = hit.distance;
		}

		public Radar_hit( Collider hit, Transform origin )
		{
			this.transform = hit.transform;
			this.distance = Vector3.Distance(
				origin.position, transform.position );
		}
	}
}