using UnityEngine;

namespace chibi.joystick
{
	[CreateAssetMenu( menuName = "chibi/joystick/axis" )]
	public class Axis : chibi.Chibi_object
	{
		public string x_name, y_name;
		public float dead_zone = 0.1f;

		public Vector3 vector;
		public bool pass_dead_zone = false;

		public void update()
		{
			vector = new Vector3(
				get_axis( x_name ), 0, get_axis( y_name ) );
			pass_dead_zone = helper.joystick.pass_dead_zone(
				vector.magnitude, dead_zone );
		}

		public float get_axis( string axis )
		{
			return Input.GetAxisRaw( axis );
		}
	}
}
