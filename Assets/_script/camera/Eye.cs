using UnityEngine;


namespace controller
{
	namespace eye
	{
		public class Eye : MonoBehaviour {

			public Transform look_at;
			public GameObject eye;
			protected Transform _transform;

			protected void Awake() {
				_init_cache();
			}

			protected void LateUpdate() {
				if ( look_at == null )
					return;
				update_position();
			}

			/// <summary>
			/// mueve la camara a la posicion deseada
			/// </summary>
			protected void update_position() {
				Vector3 desire_position = look_at.position;
				desire_position.z = _transform.position.z;
				_transform.position = desire_position;
			}


			/// <summary>
			/// asigna la camara principal en caso de no existir crea una camara principal
			/// </summary>
			protected void _use_existing_or_create_new_camera() {
				if ( Camera.main != null ) {
					eye = Camera.main.gameObject;
				}
				else {
					eye = new GameObject( "main camera" );
					eye.AddComponent<Camera>();
					eye.tag = "MainCamera";
				}
			}

			/// <summary>
			/// inicializa el chache del script
			/// </summary>
			protected virtual void _init_cache() {
				_transform = transform;
				if ( eye == null )
					_use_existing_or_create_new_camera();
			}

		}
	}
}
