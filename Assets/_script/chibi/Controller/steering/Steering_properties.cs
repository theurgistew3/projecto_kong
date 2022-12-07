using System.Collections.Generic;
using UnityEngine;

namespace chibi.controller.steering
{
	public class Steering_properties
	{
		public float time = 0f;
		public Vector3 last_target;
		public Vector3 last_direction;

		public List<Vector3> waypoints;
		public int current_waypoint;

		public chibi.radar.Radar radar;

		public float x;
	}
}
