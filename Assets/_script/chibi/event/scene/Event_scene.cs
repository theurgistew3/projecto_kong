using UnityEngine;


namespace chibi.events.scene
{
	public class Event_scene : chibi.Chibi_behaviour
	{
		public GameObject prefab_event_scene;
		public Transform where_is_goin_open;

		public virtual GameObject open()
		{
			GameObject obj;
			if ( where_is_goin_open != null )
				obj = helper.instantiate._(
					prefab_event_scene, where_is_goin_open.position );
			else
				obj = helper.instantiate._( prefab_event_scene );
			return obj;
		}

		protected virtual void OnDrawGizmos()
		{
			BoxCollider collider = GetComponent<BoxCollider>();
			Gizmos.color = Color.red;
			Gizmos.DrawCube( collider.transform.position, collider.size );
		}
	}
}