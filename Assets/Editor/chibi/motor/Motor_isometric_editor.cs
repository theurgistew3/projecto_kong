using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using chibi.manager.collision;
using chibi.motor.npc;
using chibi.motor;


namespace chibi.editor.motor.npc
{
	[CustomEditor( typeof( Motor_isometric ) )]
	public class Motor_isometric_editor : Motor_editor
	{
		public static bool show_jump_forces = false;

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			is_going_to_draw_gravity = false;
			base.OnInspectorGUI();
			Motor_isometric motor = ( Motor_isometric )target;

			draw_jump_control( motor );
			serializedObject.Update();
			if ( EditorGUI.EndChangeCheck() )
			{
				EditorUtility.SetDirty( motor );
			}
			serializedObject.ApplyModifiedProperties();
		}

		protected override void draw_gravity( Motor motor_old )
		{
			Motor_isometric motor = ( Motor_isometric )motor_old;
			var old_width = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 70f;
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField( "gravity:", motor.gravity.ToString() );
			EditorGUILayout.LabelField(
				"max jump:", motor.max_jump_velocity.ToString() );
			EditorGUILayout.LabelField(
				"min jump:", motor.min_jump_heigh.ToString() );
			EditorGUILayout.EndHorizontal();
			EditorGUIUtility.labelWidth = old_width;
		}

		public virtual void draw_jump_control( Motor_isometric motor )
		{
			EditorGUILayout.Space();
			EditorGUILayout.LabelField( "jump", EditorStyles.boldLabel );
			draw_gravity( motor );

			EditorGUILayout.BeginHorizontal();
			motor.max_jump_heigh = EditorGUILayout.FloatField(
				"max jump height", motor.max_jump_heigh );
			motor.min_jump_heigh = EditorGUILayout.FloatField(
				"min jump height", motor.min_jump_heigh );
			EditorGUILayout.EndHorizontal();
		}

		protected override string[] ignore_properties()
		{
			var ignore = base.ignore_properties();
			string[] ignore_2 = new string[] { "gravity" };
			return ignore.Union( ignore_2 ).ToArray();
		}
	}
}
