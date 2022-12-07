using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using chibi.path.old;

namespace chibi.editor.path.old
{
	[CustomEditor( typeof( Path_creator ) )]
	public class Path_editor : Editor
	{
		Path_creator creator;
		Path path;

		bool read_only;

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			if ( GUILayout.Button( "add segment" ) )
			{
				Undo.RecordObject( creator, "add segment" );
				path.add_segment( path[ path.count - 1 ] + Vector3.right );
			}
			if ( GUILayout.Button( "create_new" ) )
			{
				Undo.RecordObject( creator, "create_new" );
				creator.create_path();
				path = creator.path;
			}

			bool is_closed = GUILayout.Toggle( path.is_close, "closed" );
			if ( is_closed != path.is_close )
			{
				Undo.RecordObject( creator, "toggle closed" );
				path.is_close = is_closed;
			}

			bool auto_set_control_points = GUILayout.Toggle(
				path.auto_set_control_points, " auto set control points" );
			if ( auto_set_control_points != path.auto_set_control_points )
			{
				Undo.RecordObject( creator, "atoggle auto set controls" );
				path.auto_set_control_points = auto_set_control_points;
			}

			if ( EditorGUI.EndChangeCheck() )
				SceneView.RepaintAll();

			read_only = GUILayout.Toggle( read_only, "read only" );
			

			DrawDefaultInspector();
		}

		protected void OnSceneGUI()
		{
			draw();
			input();
		}

		protected void input()
		{
			Event gui_event = Event.current;
			Vector2 mouse_position = HandleUtility.GUIPointToWorldRay(
				gui_event.mousePosition ).origin;

			if ( gui_event.type == EventType.MouseDown
				&& gui_event.button == 0 && gui_event.shift )
			{
				// Undo.RecordObject( creator, "add segmend" );
				// path.add_segment(
				//	new Vector3( mouse_position.x, 0, mouse_position.y ) );
			}
			if ( gui_event.type == EventType.MouseDown && gui_event.button == 1 )
			{
				float distance_to_anchor = 0.05f;
				int closest_anchor_index = -1;
				for ( int i = 0; i < path.count; i += 3 )
				{
					float distance = Vector3.Distance( mouse_position, path[ i ] );
					if ( distance < distance_to_anchor )
					{
						distance_to_anchor = distance;
						closest_anchor_index = i;
					}
				}
				if ( closest_anchor_index != -1 )
				{
					Undo.RecordObject( creator, "delete segment" );
					path.delete_segment( closest_anchor_index );
				}
			}
		}

		protected void draw()
		{
			for ( int i = 0; i < path.segment_count; ++i )
			{
				Vector3[] points = path.get_points_in_segment( i );
				Handles.color = Color.black;
				Handles.DrawLine( points[1], points[0] );
				Handles.DrawLine( points[2], points[3] );
				Handles.DrawBezier(
					points[0], points[3], points[1], points[2],
					Color.green, null, 2 );
			}


			if ( !read_only )
				for ( int i = 0; i < path.count; ++i )
				{
					var current_position = path[i];
					/*
					Vector3 new_position = Handles.FreeMoveHandle(
						current_position, Quaternion.identity, 0.1f, Vector3.zero,
						Handles.SphereHandleCap );
					*/
					Vector3 new_position = Handles.PositionHandle(
						current_position, Quaternion.identity );
					if ( current_position != new_position )
					{
						Undo.RecordObject( creator, "move point" );
						path.move_point( i, new_position );
					}
				}
		}

		private void OnEnable()
		{
			creator = ( Path_creator )target;
			if ( creator.path == null || creator.path.count == 0 )
				creator.create_path();
			path = creator.path;
		}
	}
}
