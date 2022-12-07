using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using chibi.manager.collision;
using chibi.motor;

namespace chibi.editor.motor
{
	[CustomEditor( typeof( Motor ), true )]
	public class Motor_editor : Chibi_behavior_editor
	{
		SerializedProperty current_speed;

		protected bool is_going_to_draw_gravity = true;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUI.BeginChangeCheck();
			DrawPropertiesExcluding( serializedObject, ignore_properties() );

			Motor motor = ( Motor )target;

			if ( motor.debug_mode )
				draw_debug_mode( motor );

			draw_speed_control( motor );

			draw_steering( motor );
			if ( is_going_to_draw_gravity )
				draw_gravity( motor );
			if ( EditorGUI.EndChangeCheck() )
			{
				EditorUtility.SetDirty( motor );
				//EditorSceneManager.MarkSceneDirty( EditorSceneManager.GetActiveScene() );
			}
			serializedObject.ApplyModifiedProperties();
		}

		public override void draw_debug_mode( Chibi_behaviour target )
		{
			Motor motor = ( Motor )target;
			current_speed = serializedObject.FindProperty( "current_speed" );
			EditorGUILayout.PropertyField( current_speed );
		}

		public virtual void draw_speed_control( Motor motor )
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField( "speed control", EditorStyles.boldLabel );
			// EditorGUILayout.BeginHorizontal();
			motor.desire_speed = EditorGUILayout.Slider(
				"desire speed", motor.desire_speed, 0, motor.max_speed );
			// EditorGUILayout.EndHorizontal();
			motor.max_speed = EditorGUILayout.FloatField( "max speed", motor.max_speed );
			motor.velocity_acceleration = EditorGUILayout.Vector3Field(
				"velocity acceleration", motor.velocity_acceleration );
		}

		public virtual void draw_steering( Motor motor )
		{
			motor.is_steering = EditorGUILayout.Foldout(
				motor.is_steering, "is steering", true );
			if ( motor.is_steering )
			{
				motor.steering_mass = EditorGUILayout.FloatField(
					"mass", motor.steering_mass );
			}
		}

		protected override string[] ignore_properties()
		{
			var i = base.ignore_properties();
			string[] ignore = new string[] {
				"current_speed", "is_steering", "steering_mass", "desire_speed",
				"max_speed", "velocity_acceleration" };
			return i.Union( ignore ).ToArray();
		}

		protected virtual void draw_gravity( Motor motor )
		{
			motor.gravity = EditorGUILayout.FloatField(
				"gravity", motor.gravity );
		}
	}
}
