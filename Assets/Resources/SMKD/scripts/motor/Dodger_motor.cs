using System.Collections.Generic;
using chibi.controller.weapon.gun.bullet;
using UnityEngine;
using chibi.pomodoro;

namespace SMKD.motor
{
	public class Dodger_motor : chibi.motor.Motor_physical
	{
		public chibi.radar.Radar_box catch_radar;
		public chibi.radar.Radar_box dodge_radar;
		public SMKD.weapon.gun.Dodger_gun gun;

		public bool is_dodging = false;
		public bool has_the_ball = false;

		public float dodge_time = 1f;
		protected float dodge_delta = 0f;

		public GameObject damage_reciver;
		public chibi.damage.motor.HP_engine hp_motor;

		public float counter_time = 2f;
		public Pomodoro counter_pomodoro;

		public SMKD.animator.Animator_dodger animator;

		public bool is_dead
		{
			get {
				return hp_motor.is_dead;
			}
		}

		public override Vector3 desire_direction
		{
			get {
				return base.desire_direction;
			}

			set {
				base.desire_direction = value;
				gun.aim_direction = value;
			}
		}

		public void dodge()
		{
			if ( !is_dodging )
			{
				catch_radar.ping();
				foreach ( var hit in catch_radar.hits )
				{
					if ( hit.transform.GetComponent<Controller_bullet>() )
					{
						grab_ball( hit.transform );
						return;
					}
				}

				dodge_radar.ping();
				foreach ( var hit in dodge_radar.hits )
				{
					if ( hit.transform.GetComponent<Controller_bullet>() )
					{
						dodge_ball( hit.transform );
						return;
					}
				}
			}
		}

		public virtual void grab_ball( Transform transform_ball )
		{
			var bullet_controller = transform_ball.GetComponent<
				chibi.controller.weapon.gun.bullet.Controller_bullet>();
			bullet_controller.recycle();
			load_gun();
			counter_pomodoro.is_enable = true;
		}

		public virtual void give_ball()
		{
			load_gun();
			counter_pomodoro.is_enable = false;
		}

		public virtual void load_gun()
		{
			gun.load();
			has_the_ball = true;
		}

		public virtual void dodge_ball( Transform transform_ball )
		{
			is_dodging = true;
			damage_reciver.SetActive( false );
			var col1 = GetComponent<BoxCollider>();
			col1.enabled = false;
		}

		public List< Controller_bullet > shot()
		{
			if ( has_the_ball && gun.is_load )
			{
				var bullet = gun.shot();
				has_the_ball = false;
				counter_pomodoro.reset();

				var bullet_motor = (
					SMKD.motor.weapons.gun.bullet.Bullet_bounce_motor )bullet.motor;

				// bullet_motor.last_shotter = this;
				// bullet_motor.current_live_time = 0f;

				return new List<Controller_bullet>() { bullet };

			}
			return null;
		}

		private void Update()
		{
			if ( !is_dead && is_dodging )
			{
				dodge_delta += Time.deltaTime;
				if ( dodge_delta > dodge_time )
				{
					dodge_delta = 0f;
					damage_reciver.SetActive( true );
					var col1 = GetComponent<BoxCollider>();
					col1.enabled = true;
					is_dodging = false;
				}
			}

			if ( has_the_ball )
			{
				if ( counter_pomodoro.tick() )
					shot();
			}

			if ( is_dead )
			{
				damage_reciver.SetActive( false );
				foreach ( var collider in GetComponents<Collider>() )
				{
					collider.enabled = false;
				}
			}

			animator.is_dodge = is_dodging;
			animator.is_dead = hp_motor.is_dead;
			animator.has_the_ball = has_the_ball;
			animator.direction = desire_direction;
		}

		protected override void update_motion()
		{
		}

		protected override void _init_cache()
		{
			base._init_cache();

			catch_radar = new chibi.radar.Radar_box( catch_radar );
			dodge_radar = new chibi.radar.Radar_box( dodge_radar );

			hp_motor = GetComponent< chibi.damage.motor.HP_engine >();
			if ( !hp_motor)
				debug.error( "no se encontro un hp_engine" );

			counter_pomodoro = Pomodoro.CreateInstance<Pomodoro>();
			counter_pomodoro.frecuency = counter_time;
			counter_pomodoro.reset();
			hp_motor.on_died += on_died;
		}

		protected override void _dispose_cache()
		{
			base._dispose_cache();
			hp_motor.on_died -= on_died;
		}

		public virtual void on_died()
		{
			debug.info( "murio" );
		}

		public virtual void on_end_died()
		{
			debug.info( "termino de morir" );
			this.recycle();
		}

		protected virtual void OnDrawGizmos()
		{
			catch_radar.draw_gizmos( Color.red );
			dodge_radar.draw_gizmos( Color.blue );
		}
	}
}