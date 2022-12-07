using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using chibi.manager.collision;


namespace chibi.editor.manager.collision
{

	[CustomEditor( typeof( Chibi_collision_manager ), true )]
	public class Chibi_collision_manager_editor : chibi.editor.Chibi_behavior_editor
	{
		public override void OnInspectorGUI()
		{
			Chibi_collision_manager t = ( Chibi_collision_manager )target;
			if ( t.manager_collisions != null )
			{
				int old_ident = EditorGUI.indentLevel;
				EditorGUI.indentLevel += 1;
				foreach ( var item in t.manager_collisions.collisions_by_name )
				{
					GUILayout.Label( item.Key );
					EditorGUI.indentLevel += 1;
					foreach ( var i_item in item.Value )
					{
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.ObjectField( i_item.Key, typeof( GameObject ), true );
						EditorGUILayout.FloatField( i_item.Value.slope_angle );
						EditorGUILayout.EndHorizontal();
					}
					EditorGUI.indentLevel -= 1;
				}
				EditorGUI.indentLevel = old_ident;
			}
			base.OnInspectorGUI();
		}
	}
}
