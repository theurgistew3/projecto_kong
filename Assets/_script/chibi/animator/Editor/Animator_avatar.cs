using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


namespace chibi.animator.avatar.editor
{
	[CustomEditor( typeof( Animator_avatar ), true )]
	public class AnimatorAvatarInspector : Editor
	{
		SerializedProperty m_emotion;

		protected virtual void OnEnable()
		{
			m_emotion = serializedObject.FindProperty( "_emotion" );
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			this.serializedObject.Update();
			Animator_avatar component = ( Animator_avatar )target;

			EditorGUI.BeginChangeCheck();

			var emotion = ( Emotions )m_emotion.enumValueIndex;
			emotion = (Emotions)EditorGUILayout.EnumPopup( "emotion", emotion );

			if ( EditorGUI.EndChangeCheck() )
			{
				if ( !EditorApplication.isPlaying )
				{
					EditorSceneManager.MarkSceneDirty( SceneManager.GetActiveScene() );
					Undo.RecordObject( component, "change emotion" );
				}
				m_emotion.enumValueIndex = ( int )emotion;
				component.emotion = emotion;
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
