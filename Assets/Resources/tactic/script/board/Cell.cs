using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tactic.board
{
	public class Cell : chibi.Chibi_behaviour
	{
		public float size = 0.5f;
		public Vector3 start_point = Vector3.zero;

		public Vector3 corner_1
		{
			get {
				return start_point;
			}
		}

		public Vector3 corner_2
		{
			get {
				return new Vector3(
					start_point.x, start_point.y, start_point.z + size );
			}
		}

		public Vector3 corner_3
		{
			get {
				return new Vector3(
					start_point.x + size, start_point.y, start_point.z + size );
			}
		}

		public Vector3 corner_4
		{
			get {
				return new Vector3(
					start_point.x + size, start_point.y, start_point.z );
			}
		}

		public Vector3 center
		{
			get {
				float size = this.size / 2;
				return new Vector3(
					start_point.x + size, start_point.y, start_point.z + size );
			}
			set {
				float size = this.size / 2;
				start_point = new Vector3(
					value.x - size, value.y, value.z - size );
				transform.position = value;

			}
		}

		public void gizmo( float center_radius, Color border, Color center )
		{
			Color old_color = Gizmos.color;
			Gizmos.color = border;
			Gizmos.DrawLine( corner_1, corner_2 );
			Gizmos.DrawLine( corner_2, corner_3 );
			Gizmos.DrawLine( corner_3, corner_4 );
			Gizmos.DrawLine( corner_4, corner_1 );

			Gizmos.color = center;
			helper.draw.sphere.gizmo( this.center, center_radius );
			Gizmos.color = old_color;
		}

		public void gizmo()
		{
			gizmo( 0.1f, Color.green, Color.black );
		}
	}
}
