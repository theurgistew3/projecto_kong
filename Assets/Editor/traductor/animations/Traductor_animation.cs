using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using tactic.board;

namespace traductor.editor.board
{
	public class Traductor_animation: chibi.editor.Chibi_behavior_editor
	{
		[MenuItem( "traductor/create_animator" )]
		static public void bake()
		{
			string alfabeto = "Assets/Resources/Traductor/Animaciones/Alfabeto";

			var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(
				"Assets/Resources/Traductor/animations/cosa.controller" );
			controller.AddParameter( "letra", AnimatorControllerParameterType.Int );
			var root_state_machine = controller.layers[ 0 ].stateMachine;
			Debug.Log( root_state_machine );
			var state = root_state_machine.AddState( "idle" );


			var states = new Dictionary<string, UnityEditor.Animations.AnimatorState>();


			DirectoryInfo path = new DirectoryInfo( alfabeto );

			if ( !path.Exists )
			{
				Debug.LogError(
					string.Format( "el path de animaciones no exite '{0}'", alfabeto ) );
				return;
			}

			foreach ( var file in path.GetFiles() )
			{
				if ( file.Extension == ".anim" )
				{
					string name = Path.GetFileNameWithoutExtension( file.Name );
					state = root_state_machine.AddState( name );
					var full_path = file.FullName.Replace( @"\", "/" );
					var relative_path = full_path.Replace( Application.dataPath, "" );
					string asset_path = "Assets" + relative_path;
					var motion = AssetDatabase.LoadAssetAtPath<Motion>( asset_path );
					state.motion = motion;

					states.Add( name, state );
				}
				//Debug.Log( file.ToString() );
			}

			var keys = states.Keys;
			for ( int i = 0; i < keys.Count, ++i )
			{
				state = states[ key ];
				var transition = state.AddExitTransition();
				transition.AddCondition( UnityEditor.Animations.AnimatorConditionMode.Equals, i + 1, "letra" );
				return;
			}
		}

		[MenuItem( "traductor/create_animator_2", false, 155 )]
		static public void bake_2( MenuCommand context )
		{
			string path;
			if ( Selection.assetGUIDs.Length == 0 )
			{
				Debug.Log( "Assets/pudrete" );
			}
			else
			{
				var a = AssetDatabase.GUIDToAssetPath( Selection.assetGUIDs[ 0 ] );
				Debug.Log( a );
			}
		}


		[ ContextMenu( "qqqqqqqq" ) ]
		//[ CreateAssetMenu( menuName="aventure_time/dialogue/dialogue" ) ]
		static public void bake_2()
		{
			Debug.Log( "hello 2" );
		}

		private void OnEnable()
		{
			//board = ( Board )target;
			//board.extert_init_cache();
		}
	}
}
