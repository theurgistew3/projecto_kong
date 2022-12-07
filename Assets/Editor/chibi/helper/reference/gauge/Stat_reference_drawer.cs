using UnityEditor;
using UnityEngine;
using chibi.tool.reference;

namespace chibi.editor.tool.reference
{
	[ CustomPropertyDrawer( typeof( Stat_reference ) ) ]
	public class Stat_reference_drawer : PropertyDrawer
	{
		/// <summary>
		/// Options to display in the popup to select constant or variable.
		/// </summary>
		private readonly string[] popupOptions =
			{ "Use Constant", "Use Variable" };

		const string kVectorMinName = "x";
		const string kVectorMaxName = "y";
		const float kFloatFieldWidth = 30f;
		const float kSpacing = 2f;

		/// <summary> Cached style to use to draw the popup button. </summary>
		private GUIStyle popupStyle;

		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			if ( popupStyle == null )
			{
				popupStyle = new GUIStyle( GUI.skin.GetStyle( "PaneOptions" ) );
				popupStyle.imagePosition = ImagePosition.ImageOnly;
			}

			label = EditorGUI.BeginProperty( position, label, property );
			position = EditorGUI.PrefixLabel( position, label );

			EditorGUI.BeginChangeCheck();

			// Get properties
			SerializedProperty useConstant = property.FindPropertyRelative( "use_constant" );
			SerializedProperty _current = property.FindPropertyRelative( "_current" );
			SerializedProperty _max = property.FindPropertyRelative( "_max" );
			SerializedProperty variable = property.FindPropertyRelative( "variable" );

			// Calculate rect for configuration button
			Rect buttonRect = new Rect( position );
			buttonRect.yMin += popupStyle.margin.top;
			buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
			position.xMin = buttonRect.xMax;

			// Store old indent level and set it to 0, the PrefixLabel takes care of it
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			int result = EditorGUI.Popup( buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle );

			useConstant.boolValue = result == 0;

			if ( useConstant.boolValue )
			{
				// Debug.Log( position );
				Rect current_position = new Rect( position );
				current_position.yMin += popupStyle.margin.top;
				current_position.width = ( position.width - buttonRect.width ) / 2;
				EditorGUI.PropertyField( current_position, _current, GUIContent.none );

				Rect max_position = new Rect( position );
				max_position.yMin += popupStyle.margin.top;
				max_position.xMin = current_position.xMax;
				max_position.width = ( position.width - buttonRect.width ) / 2;
				EditorGUI.PropertyField( max_position, _max, GUIContent.none );
			}
			else
			{
				EditorGUI.PropertyField( position, variable, GUIContent.none );
			}

			if ( EditorGUI.EndChangeCheck() )
				property.serializedObject.ApplyModifiedProperties();

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}