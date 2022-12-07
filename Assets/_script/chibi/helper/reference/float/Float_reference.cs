using System;

namespace chibi.tool.reference
{
	[Serializable]
	public class Float_reference
	{
		public bool use_constant = true;
		public float constant_value;
		public Float variable;

		public Float_reference()
		{ }

		public Float_reference( float value )
		{
			use_constant = true;
			constant_value = value;
		}

		public float Value
		{
			get {
				if ( use_constant )
					return constant_value;
				return variable.value;
			}
		}

		public static implicit operator float( Float_reference reference )
		{
			return reference.Value;
		}
	}
}
