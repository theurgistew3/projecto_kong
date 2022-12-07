using UnityEngine;

namespace chibi.spawner
{
	[ RequireComponent( typeof( BoxCollider ) ) ]
	public class Spawner_in_box_shotgun : Spawner
	{
		protected BoxCollider box_collider;

		public override GameObject spawn()
		{
			var prefab = next();
			var bounds = box_collider.bounds;
			var max = bounds.max;
			var min = bounds.min;

			var position = new Vector3(
				Random.Range( min.x, max.x ),
				Random.Range( min.y, max.y ),
				Random.Range( min.z, max.z )
			);
			var result = helper.instantiate._( prefab, position );
			return result;
		}

		protected override void _init_cache()
		{
			base._init_cache();
			box_collider = GetComponent<BoxCollider>();
			if ( !box_collider )
				Debug.LogError( string.Format(
					"[Spawner_in_box_shotgun] no se encontro un box collider en '{0}'",
					helper.game_object.name.full( this ) ), this.gameObject );
		}

		protected void OnDrawGizmos()
		{
			var collider = GetComponent<BoxCollider>();
			Matrix4x4 rotationMatrix = Matrix4x4.TRS(
				transform.position, transform.rotation, transform.lossyScale );
			Gizmos.matrix = rotationMatrix;
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube( collider.center, collider.size );
		}
	}
}