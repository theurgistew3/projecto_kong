using UnityEngine;
using UnityEditor;
using chibi.motor.weapons.gun.turrent;

namespace chibi.editor.motor.weapons.gun.turrent
{
	[CustomEditor( typeof( Turrent ), true )]
	public class Turrent_inspector : Editor
	{
		public override void OnInspectorGUI()
		{
			Turrent component = ( Turrent )target;
			DrawDefaultInspector();
		}

		protected virtual void OnSceneGUI()
		{
			Turrent component = ( Turrent )target;

			var q = Quaternion.AngleAxis(
				-component.max_rotation_angle / 2, component.rotation_vector );
			Vector3 from = q * component.transform.forward;
			Handles.color = Color.green;

			Handles.DrawSolidArc(
				component.transform.position, component.rotation_vector,
				from, component.max_rotation_angle, 0.2f );
		}
	}
}
