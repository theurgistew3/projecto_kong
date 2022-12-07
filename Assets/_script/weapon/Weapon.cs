using UnityEngine;
using weapon.bullet;


namespace weapon
{
	namespace weapon
	{
		public class Weapon : chibi.Chibi_behaviour
		{
			public GameObject prefab_bullet;
			public Transform bullet_spawn;
			public float rate_fire = 1;

			public Vector3 direction_shot
			{
				get {
					return transform.forward;
				}
			}

			public GameObject shot()
			{
				GameObject bullet_obj = instanciate_bullet();
				Bullet_base bullet = bullet_obj.GetComponent<Bullet_base>();
				bullet.shot( direction_shot );
				return bullet_obj;
			}

			protected GameObject instanciate_bullet()
			{
				GameObject bullet = helper.instantiate.position(
					prefab_bullet, bullet_spawn.position );
				return bullet;
			}
		}
	}
}
