using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using tactic.board;

namespace traductor.editor.board
{
	public class Traductor_animation : chibi.editor.Chibi_behavior_editor
	{
		[MenuItem( "traductor/create_animator" )]
		static public void bake()
		{
			string alfabeto = "Assets/Resources/Traductor/Animaciones/Alfabeto";
			string idle = "Assets/Resources/Traductor/Animaciones/idle.anim";
			string numbers = "Assets/Resources/Traductor/Animaciones/Numeros 1-20";
			string saludos = "Assets/Resources/Traductor/Animaciones/Saludos";
			string sujetos = "Assets/Resources/Traductor/Animaciones/Sujetos";
			string adjetivo = "Assets/Resources/Traductor/Animaciones/Adjetivos";
			string verbo = "Assets/Resources/Traductor/Animaciones/Verbos";
			string colores = "Assets/Resources/Traductor/Animaciones/Colores";

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
			int current_id = 0;
			current_id = create_animation_from_directory(
				idle_state, alfabeto, root_state_machine, ref states, current_id ) ;
			current_id = create_animation_from_directory(
				idle_state, numbers, root_state_machine, ref states, current_id ) ;
			current_id = create_animation_from_directory(
				idle_state, saludos, root_state_machine, ref states, current_id ) ;
			current_id = create_animation_from_directory(
				idle_state, sujetos, root_state_machine, ref states, current_id ) ;
			current_id = create_animation_from_directory(
				idle_state, adjetivo, root_state_machine, ref states, current_id);
			current_id = create_animation_from_directory(
				idle_state, verbo, root_state_machine, ref states, current_id);
			current_id = create_animation_from_directory(
				idle_state, colores, root_state_machine, ref states, current_id);
			//copia  %LOCALAPPDATA%\Unity\Editor\Editor.log 


		}

		static public string generador_de_palabras( string current, string name, int id_animation )
		{
			return current + string.Format( "new struct_animation(\"{0}\", {1}),\n", name, id_animation );
		}

		static public int create_animation_from_directory(
			UnityEditor.Animations.AnimatorState idle, string folder,
			AnimatorStateMachine root,
			ref Dictionary<string, UnityEditor.Animations.AnimatorState> states,
			int current_id )
		{
			AnimatorState state;
			DirectoryInfo path = new DirectoryInfo( folder );
			Motion motion;

			if ( !path.Exists )
			{
				throw new FileLoadException(
					string.Format( "el path de animaciones no exite '{0}'", folder ) );
			}

			foreach ( var file in path.GetFiles( "*.anim" ) )
			{
				string name = Path.GetFileNameWithoutExtension( file.Name );
				state = root.AddState( name );

				motion = load_animation( file );
				state.motion = motion;

				states.Add( name, state );
				Debug.Log( string.Format( "cargando animacion {0}", name ) );
			}

			string a = "";
			var keys = new List<string>( states.Keys );
			for ( int i = 0; i < keys.Count; i++ )
			{

				var current_key = keys[ i ];
				var current_state = states[ keys[ i ] ];

				idle_transition( idle, current_state, i + 1 );

				for ( int j = 0; j < keys.Count; j++ )
				{
					var inner_key = keys[ j ];
					int id_animation = current_id + j + 1;
					a = generador_de_palabras( a, inner_key, id_animation );
					if ( inner_key == current_key )
						continue;

					var inner_state = states[ keys[ j ] ];
					var transition = current_state.AddTransition( inner_state, true );
					transition.AddCondition( UnityEditor.Animations.AnimatorConditionMode.Equals, id_animation, "letra" );
				}
			}
			Debug.Log( a );
			return current_id;
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




