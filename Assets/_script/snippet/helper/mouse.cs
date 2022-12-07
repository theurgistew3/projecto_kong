using UnityEngine;

namespace helper
{
	public class mouse {

		public static float time_doble_click = 0.3f;
		public static float last_click = 0f;

		public static float time_for_hold_click = 0.5f;
		public static bool click_once = false;

		public static bool enable_double_click = true;

		static bool click_LB_up = false;
		static float click_LB_last_up = 0f;

//		static Vector3 dead_zone = new Vector3(0.2f, 0.2f, 0);

		/// <summary>
		/// Obtiene el eje X del movimiento del mouse
		/// </summary>
		public static float axis_x{
			get{
				return Input.GetAxis("mouse x");
			}
		}

		/// <summary>
		/// Obtiene el eje Y del movimiento del mouse
		/// </summary>
		public static float axis_y{
			get{
				return Input.GetAxis("mouse y");
			}
		}

		/// <summary>
		/// Regresa un vector de 2 dimenciones representado el movimiento del mouse
		/// y no su posicion
		/// </summary>
		public static Vector2 axis {
			get {
				return new Vector2( axis_x, axis_y );
			}
		}

		public static float wheel{
			get{
				return Input.GetAxis("mouse scrollwheel");
			}
		}

		public static bool LB{
			get {
				if (LB_down){
					return false;
				}
				else if (LB_up){
					click_LB_up = true;
					click_LB_last_up = Time.time;
					return false;
				}
				else if (click_LB_up && helper.time.get_delta_time(click_LB_last_up) > time_doble_click){
//					Debug.Log(helper.time.get_delta_time(click_LB_last_up));
//					Debug.Log("time", click_LB_last_up);
//					click_LB_last_up = Time.time + 10;
					click_LB_up = false;
					return true;
				}
				return false;
			}
		}

		public static bool LB_down{
			get{
				return Input.GetMouseButtonDown(0);
			}
		}
		
		public static bool LB_up{
			get{
				return Input.GetMouseButtonUp(0);
			}
		}

		public static bool LB_double{
			get{
				if (enable_double_click)
				if (LB_down){
					if (click_once && time_of_last_click < time_doble_click){
						click_once = false;
						return true;
					}
					last_click = Time.time;
					click_once = true;
				}
				if (LB_up){
					if (time_of_last_click > time_doble_click)
						click_once = false;
				}
				return false;
			}
		}

		public static bool RB{
			get{
				return Input.GetMouseButton(1);
			}
		}
		public static bool MB{
			get{
				return Input.GetMouseButton(3);
			}
		}

		public static Vector3 convert_axis_to_world(Vector3 axis_mouse){
			return new Vector3(axis_mouse.x, 0, axis_mouse.y);
		}

		protected static float time_of_last_click{
			get{
				return Time.time - last_click;
			}
		}

		public static Vector3 get_mouse_axis(){
			return new Vector3(axis_x, axis_y, 0);
		}
	}
}