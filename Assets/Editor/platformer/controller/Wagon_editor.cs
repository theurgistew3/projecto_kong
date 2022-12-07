using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using platformer.controller.wagon;

namespace platformer.editor.controller.wagon
{
	[CustomEditor( typeof( Wagon ), true )]
	public class Wagon_editor : chibi.editor.Chibi_behavior_editor
	{
		protected Wagon wagon;

		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField( "move wagon" );
			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "<" ) )
			{
				if ( EditorApplication.isPlaying )
				{
					wagon.move_prev_step();
				}
				else
				{
					Undo.RecordObject( wagon, "move prev step" );
					wagon.move_prev_instant_step();
				}
			}
			if ( GUILayout.Button( ">" ) )
			{
				if ( EditorApplication.isPlaying )
				{
					wagon.move_next_step();
				}
				else
				{
					Undo.RecordObject( wagon, "move next step" );
					wagon.move_next_instant_step();
				}
			}
			EditorGUILayout.EndHorizontal();
			base.OnInspectorGUI();
		}

		private void OnEnable()
		{
			wagon = ( Wagon )target;
			if ( !wagon.path )
			{
				var path = helper.game_object.Find._<
					chibi.path.Path_behaviour >( wagon.gameObject, "path" );
				if ( !path )
				{
					wagon.debug.error( "no se encontro el path en el wagon" );
				}
			}

			if ( !wagon.controller )
			{
				wagon.debug.error( "no tiene un control de plataforma" );
			}
			else
			{
				if ( !wagon.controller.motor )
				{
					wagon.controller.prepare_motor();
				}
			}
		}
	}
}
