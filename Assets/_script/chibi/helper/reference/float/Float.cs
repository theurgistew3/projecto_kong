using UnityEngine;

namespace chibi.tool.reference
{
	[ CreateAssetMenu( menuName="chibi/tool/reference/float" ) ]
	public class Float : Chibi_object
	{
#if UNITY_EDITOR
		[Multiline]
		public string developer_descriptor = "";
#endif
		public float value;

		public void set_value( float value )
		{
			this.value = value;
		}

		public void set_value( Float value )
		{
			this.value = value.value;
		}

		public void apply_change( float amount )
		{
			value += amount;
		}

		public void apply_change( Float amount )
		{
			value += amount.value;
		}
	}
}