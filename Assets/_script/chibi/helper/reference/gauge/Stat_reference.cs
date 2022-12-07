using System;
using chibi.rol_sheet.stat;


namespace chibi.tool.reference
{
	[Serializable]
	public class Stat_reference
	{
		public bool use_constant = true;
		public float _current, _max;
		public Stat variable;

		public Stat_reference()
		{ }

		public Stat_reference( float current, float max )
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
	}
}
