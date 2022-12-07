using System.Collections;
using UnityEngine;
using weapon.stat;
using weapon.ammo;

using chibi.controller.weapon.gun.bullet;


namespace chibi.weapon.gun
{
	public enum Auto_aim_type
		{ aim, shunt }

	public abstract class Gun : Weapon
	{
		[ Header( "gun stats" ) ]
		public Gun_stat stat;
		public Ammo ammo;
		public Transform position_of_shot;

		[ Header( "auto aim settings" ) ]
		public chibi.tool.reference.Game_object_reference auto_aim_target;
		public Auto_aim_type auto_aim_type;

		[ Header( "burst options" ) ]
		public Burst_by_bullet_option burst_bullet_option;

		protected IEnumerator __automatic_shot;
		private bool _automatic_shot = false;
		protected Vector3 _aim_direction;

		protected int burst_amount = 0;
		protected int amount_of_automatic_shot = 0;


		public Vector3 direction_shot
		{
			get {
				var direction = transform.forward.normalized;
				if ( auto_aim_type == Auto_aim_type.shunt )
					return -direction;
				return direction;
			}
		}

		public float rate_fire
		{
			get {
				return 1 / stat.rate_fire;
			}
		}

		public Vector3 aim_direction
		{
			get {
				return _aim_direction;
			}
			set {
				_aim_direction = transform.position + value;
				transform.LookAt( _aim_direction );
			}
		}

		public virtual void aim_to( GameObject target )
		{
			aim_to( target.transform );
		}

		public virtual void aim_to( Transform target )
		{
			aim_to( target.position );
		}

		public virtual void aim_to( Vector3 position )
		{
			aim_direction = position - transform.position;
		}

		public bool automatic_shot
		{
			get{
				return _automatic_shot;
			}

			set {
				_automatic_shot = value;
				if ( automatic_shot )
				{
					__automatic_shot = do_automatic_shot();
					StartCoroutine( __automatic_shot );
				}
				else if ( __automatic_shot != null )
				{
					StopCoroutine( __automatic_shot );
				}
			}
		}

		public abstract Controller_bullet shot();

		public Controller_bullet shot( bool commit )
		{
			var bullet = shot();
			if ( commit )
				bullet.ready();
			return bullet;
		}

		public override void attack()
		{
			shot();
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !ammo )
			{
				ammo = load_default_ammo() as Ammo;
			}
			if ( !stat )
			{
				stat = load_default_stat() as Gun_stat;
			}
		}

		protected virtual chibi.Chibi_object load_default_ammo()
		{
			return Ammo.CreateInstance<Ammo>().find_default<Ammo>();
		}

		protected virtual chibi.Chibi_object load_default_stat()
		{
			return Gun_stat.CreateInstance<Gun_stat>()
				.find_default<Gun_stat>();
		}

		protected void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			if ( position_of_shot )
			{
				Gizmos.DrawWireSphere( position_of_shot.position, 0.2f );
				Gizmos.color = Color.red;
				helper.draw.arrow.gizmo( position_of_shot.position, direction_shot );
			}
			else
			{
				Gizmos.DrawWireSphere( transform.position, 0.2f );
				Gizmos.color = Color.red;
				helper.draw.arrow.gizmo( transform.position, direction_shot );
			}
		}

		protected virtual IEnumerator do_automatic_shot()
		{
			while( true )
			{
				var bullet = shot( false );
				++amount_of_automatic_shot;
				if ( burst_amount > 0 )
				{
					burst_bullet_option.update( bullet );
					if ( amount_of_automatic_shot >= burst_amount )
					{
						burst_bullet_option.reset();
						burst_amount = 0;
						amount_of_automatic_shot = 0;
						automatic_shot = false;
					}
				}
				bullet.ready();
				yield return new WaitForSeconds( rate_fire );
			}
		}

		public void burst()
		{
			burst_bullet_option.reset();
			burst_amount = stat.burst_amount;
			amount_of_automatic_shot = 0;
			automatic_shot = true;
		}
	}
}
