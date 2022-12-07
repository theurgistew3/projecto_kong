using UnityEngine;


namespace chibi.events.scene.handler
{
	public class Event_handler : chibi.Chibi_behaviour
	{
		protected virtual void OnTriggerEnter( Collider other )
		{
			Event_scene event_scene = other.GetComponent<Event_scene>();
			if ( event_scene == null )
				return;
			event_scene.open();
			Destroy( event_scene.gameObject );
		}

		protected virtual void OnDrawGizmos()
		{
			//Gizmos.DrawWireCube( collider.transform.position, collider.size );

			var collider = GetComponent<BoxCollider>();
			Matrix4x4 rotationMatrix = Matrix4x4.TRS(
				transform.position, transform.rotation, transform.lossyScale );
			Gizmos.matrix = rotationMatrix;
			Gizmos.color = Color.red;
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube( Vector3.zero, collider.size );
		}
	}
}