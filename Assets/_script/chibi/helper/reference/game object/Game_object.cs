using UnityEngine;

namespace chibi.tool.reference
{
	[ CreateAssetMenu( menuName="chibi/tool/reference/gameobject" ) ]
	public class Game_object : Chibi_object
	{
#if UNITY_EDITOR
		[Multiline]
		public string developer_descriptor = "";
#endif
		public GameObject value;

		public void set_value( GameObject value )
		{
			this.value = value;
		}

		public void set_value( Game_object value )
		{
			this.value = value.value;
		}
	}
}