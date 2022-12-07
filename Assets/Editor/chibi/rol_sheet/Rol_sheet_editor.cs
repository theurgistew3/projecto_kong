using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using chibi.manager.collision;
using chibi.motor.npc;
using chibi.rol_sheet;


namespace chibi.editor.rol_sheet
{
	[CustomEditor( typeof( Rol_sheet ) )]
	public class Rol_sheet_editor : Chibi_behavior_editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			base.OnInspectorGUI();
			Rol_sheet rol_sheet = ( Rol_sheet )target;

			draw_attributes( rol_sheet );
			serializedObject.Update();
			if ( EditorGUI.EndChangeCheck() )
			{
				EditorUtility.SetDirty( rol_sheet );
			}
			serializedObject.ApplyModifiedProperties();
		}

		public void draw_attributes( Rol_sheet rol_sheet )
		{
			//EditorGUILayout.BeginHorizontal();
			//EditorGUILayout.EndHorizontal();

			/*
			rol_sheet.show_attributes_editor = EditorGUILayout.Foldout(
				rol_sheet.show_attributes_editor, "Attributos", true );
			if ( rol_sheet.show_attributes_editor )
			{
				EditorGUILayout.IntField( "Fuerza", 10 );
				EditorGUILayout.IntField( "Dextreza", 10 );
				EditorGUILayout.IntField( "Percepcion", 10 );
				EditorGUILayout.IntField( "Inteligencia", 10 );
			}
			*/
		}

		protected override string[] ignore_properties()
		{
			var ignore = base.ignore_properties();
			return ignore;
		}
	}
}
