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
			string idle = "Assets/Resources/Traductor/Animaciones/idle.anim";

			var controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath(
				"Assets/Resources/Traductor/animations/cosa.controller" );
			controller.AddParameter( "letra", AnimatorControllerParameterType.Int );
			var root_state_machine = controller.layers[ 0 ].stateMachine;
			var state = root_state_machine.AddState( "idle", Vector3.zero );
			//root_state_machine.


			var states = new Dictionary<string, UnityEditor.Animations.AnimatorState>();
			var idle_state = state;

			var motion = load_animation( idle );
			idle_state.motion = motion;


			DirectoryInfo path = new DirectoryInfo( alfabeto );

			if ( !path.Exists )
			{
				Debug.LogError(
					string.Format( "el path de animaciones no exite '{0}'", alfabeto ) );
				return;
			}

			foreach ( var file in path.GetFiles( "*.anim" ) )
			{
				string name = Path.GetFileNameWithoutExtension( file.Name );
				state = root_state_machine.AddState( name );

				motion = load_animation( file );
				state.motion = motion;

				states.Add( name, state );
			}

			var keys = new List<string>( states.Keys );
			for ( int i = 0; i < keys.Count; i++ )
			{

				var current_key = keys[ i ];
				var current_state = states[ keys[i] ];

				idle_transition( idle_state, current_state, i + 1 );

				for ( int j = 0; j < keys.Count; j++ )
				{
					var inner_key = keys[ j ];
					//inner_key = "b";
					//int id_animation = funcion_magica( inner_key );
					int id_animation = j + 1;
					if ( inner_key == current_key )
						continue;

					var inner_state = states[ keys[j] ];
					var transition = current_state.AddTransition( inner_state, true );
					transition.AddCondition( UnityEditor.Animations.AnimatorConditionMode.Equals, id_animation, "letra" );
				}
			}
		}


		static public void idle_transition( AnimatorState idle, AnimatorState current, int id_animation )
		{
			var transition = idle.AddTransition( current, true );
			transition.AddCondition( UnityEditor.Animations.AnimatorConditionMode.Equals, id_animation, "letra" );
			transition = current.AddTransition( idle, true );
			transition.AddCondition( UnityEditor.Animations.AnimatorConditionMode.Equals, 0, "letra" );
		}

		static public Motion load_animation( FileInfo file )
		{
			var full_path = file.FullName.Replace( @"\", "/" );
			var relative_path = full_path.Replace( Application.dataPath, "" );
			string asset_path = "Assets" + relative_path;

			var motion = AssetDatabase.LoadAssetAtPath<Motion>( asset_path );
			return motion;
		}
		static public Motion load_animation( string file )
		{
			var result = new FileInfo( file );
			return load_animation( result );
		}
	}
}
