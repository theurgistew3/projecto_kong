using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using tactic.board;

namespace tactic.editor.board
{
	[CustomEditor( typeof( Board ), true )]
	public class Board_editor: chibi.editor.Chibi_behavior_editor
	{
		protected Board board;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.LabelField( "width:", board.width.ToString() );
			EditorGUILayout.LabelField( "height:", board.height.ToString() );
			EditorGUILayout.LabelField( "columns:", board.count_columns.ToString() );
			EditorGUILayout.LabelField( "rows:", board.count_rows.ToString() );
			if ( GUILayout.Button( "bake" ) )
			{
				foreach ( var target in targets )
				{

					var tmp_board = target as Board;
					tmp_board.bake();
				}
				Undo.RecordObject( board, "bake cells" );
			}

			if ( GUILayout.Button( "recovery" ) )
			{
				foreach ( var target in targets )
				{

					var tmp_board = target as Board;
					tmp_board.recovery();
				}
				Undo.RecordObject( board, "recovery cells" );
			}
		}

		private void OnEnable()
		{
			board = ( Board )target;
			board.extert_init_cache();
		}
	}
}
