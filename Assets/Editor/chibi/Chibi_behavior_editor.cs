using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using chibi.manager.collision;
using chibi;


namespace chibi.editor
{

	[CustomEditor( typeof( Chibi_behaviour ), true )]
	public class Chibi_behavior_editor : Editor
	{
		SerializedProperty current_speed;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			DrawPropertiesExcluding( serializedObject, ignore_properties() );

			Chibi_behaviour behaviour = ( Chibi_behaviour )target;

			if ( behaviour.debug_mode )
				draw_debug_mode( behaviour );
			serializedObject.ApplyModifiedProperties();
		}

		public virtual void draw_debug_mode( Chibi_behaviour behaviour )
		{
		}


		protected virtual string[] ignore_properties()
		{
			string[] ignore = new string[] { };
			return ignore;
		}
	}
}
