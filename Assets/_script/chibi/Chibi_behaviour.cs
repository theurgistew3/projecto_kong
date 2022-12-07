using UnityEngine;

namespace chibi
{
	public class Chibi_behaviour : MonoBehaviour {

		protected bool _is_drawing_gizmo;

		protected bool _is_instanciate;

		public bool debug_mode = false;
		public chibi.pool.Pool_behaviour pool;
		public helper.debug.Debug debug;

		protected virtual void Awake()
		{
			debug = new helper.debug.Debug( this );
			//_init_cache();
		}

		protected virtual void Start() {
			debug = new helper.debug.Debug( this );
		}

		protected virtual void _init_cache() {
			debug = new helper.debug.Debug( this );
		}

		protected virtual void _dispose_cache() {
			debug = null;
		}

		public void gizmo_awake() {
			_is_drawing_gizmo = true;
			Start();
			_is_drawing_gizmo = false;
		}

		public void extert_init_cache() {
			_init_cache();
			debug.warning( "se esta llamando el extern_init_cache" );
		}

		public virtual void reset()
		{
		}

		public virtual void recycle()
		{
			if ( pool )
			{
				// debug.info( "el objecto de envia al pool" );
				pool.push( this );
			}
			else
			{
				// debug.info( "el objecto se destruira" );
				Destroy( this.gameObject );
			}
		}

		protected virtual void OnEnable()
		{
			_init_cache();
		}

		protected virtual void OnDisable()
		{
			_dispose_cache();
		}
	}
}
