using System;
using chibi.rol_sheet.need;


namespace chibi.tool.reference
{
	[Serializable]
	public class Need_reference
	{
		public bool use_constant = true;
		public float _current = 0f, _max = 1f;
		public Need variable;

		public Need_reference()
		{ }

		public Need_reference( float current, float max )
		{
			use_constant = true;
			_current = current;
			_max = max;
		}

		public float current
		{
			get {
				if ( use_constant )
					return _current;
				return variable.current;
			}
			set {
				if ( use_constant )
					_current = value;
				else
					variable.current = value;
			}
		}

		public float max
		{
			get {
				if ( use_constant )
					return _max;
				return variable.max;
			}
			set {
				if ( use_constant )
					_max = value;
				else
					variable.max = value;
			}
		}

		public bool is_empty
		{
			get {
				if ( use_constant )
					return _current <= 0;
				return variable.is_empty;
			}
		}

		public bool is_full
		{
			get {
				if ( use_constant )
					return _current >= max;
				return variable.is_full;
			}
		}
	}
}
