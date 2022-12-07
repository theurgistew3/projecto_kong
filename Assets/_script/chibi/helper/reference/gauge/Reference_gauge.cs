using System;
using chibi.UI.chicken_o_meter;


namespace chibi.tool.reference
{
	[Serializable]
	public class Reference_gauge
	{
		public bool use_constant = true;
		public float _current, _max;
		public Gauge variable;

		public Reference_gauge()
		{ }

		public Reference_gauge( float current, float max )
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
