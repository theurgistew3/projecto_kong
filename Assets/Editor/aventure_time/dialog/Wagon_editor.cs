using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using aventure_time.dialog;

namespace aventure_time.editor.dialog
{
	[CustomEditor( typeof( Dialogue ), true )]
	public class Dialogue_editor : chibi.editor.Chibi_behavior_editor
	{
		protected Dialogue dialogue;

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField( "move wagon" );
			EditorGUILayout.LabelField( dialog_path );
			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "<" ) )
			{
				if ( EditorApplication.isPlaying )
				{
				}
				else
				{
					//Undo.RecordObject( dialogue, "move prev step" );
				}
			}
			if ( GUILayout.Button( ">" ) )
			{
				if ( EditorApplication.isPlaying )
				{
				}
				else
				{
					//Undo.RecordObject( dialogue, "move next step" );
				}
			}
			EditorGUILayout.EndHorizontal();
			base.OnInspectorGUI();
		}

		private void OnEnable()
		{
			dialogue = ( Dialogue )target;
		}

		protected string dialog_path
		{
			get {
				return AssetDatabase.GetAssetPath( dialogue );
			}
		}
	}

}
