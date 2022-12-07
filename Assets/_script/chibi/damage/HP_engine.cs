using UnityEngine;

namespace chibi.damage
{
	namespace motor
	{
		public class HP_engine : chibi.Chibi_behaviour
		{
			public chibi.rol_sheet.Rol_sheet rol;
			public bool use_hp_from_rol = true;
			public chibi.tool.reference.Stat_reference stat;

			public delegate void on_died_delegate();
			public event on_died_delegate on_died;

			public virtual bool is_dead
			{
				get {
					return stat.current <= 0;
				}
			}

			protected virtual void OnTriggerEnter( Collider other )
			{
				if ( helper.consts.tags.damage == other.tag )
				{
					Damage damage = other.GetComponent<Damage>();
					proccess_damage( damage );
				}
			}

			private void OnCollisionEnter( Collision collision )
			{
				if ( helper.consts.tags.damage == collision.gameObject.tag )
				{
					Damage damage = collision.gameObject.GetComponent<Damage>();
					proccess_damage( damage );
				}
			}

			protected override void _init_cache()
			{
				base._init_cache();
				if ( use_hp_from_rol )
				{
					if ( !rol )
						rol = GetComponent<chibi.rol_sheet.Rol_sheet>();
					if ( !rol )
						Debug.LogError( string.Format(
							"[hp_engine] no se encontro un Rol_sheet en '{0}'",
							helper.game_object.name.full( this ) ) );
					stat = rol.hp;
				}
			}

			protected virtual bool is_my_damage( Damage damage )
			{
				if ( damage.owner && rol )
				{
					if ( damage.owner.sheet && damage.owner.sheet.faction
							&& rol.sheet )
						return damage.owner.sheet.faction == rol.sheet.faction;

					if ( rol )
						return damage.owner == rol;
				}
				return false;
			}

			protected virtual void proccess_damage( Damage damage )
			{
				if ( damage == null )
				{
					Debug.LogError(
						"[hp engine] el gameobject de danno no tiene el damage" );
					return;
				}
				if ( is_my_damage( damage ) )
					return;
				take_damage( damage );
			}

			public virtual void take_damage( Damage damage )
			{
				take_damage( damage.damage, (int)damage.amount, damage.owner );
			}

			public virtual void take_damage(
				damage.Damage damage, int amount, rol_sheet.Rol_sheet owner )
			{
				debug.info( "recibio {0} danno de parte de {1}", amount, owner );
				if ( is_dead )
				{
					debug.info( "ya estaba muerto, ignorando logica de danno" );
					return;
				}
				stat.current -= amount;
				if ( is_dead )
				{
					died( true );
				}
			}

			public virtual void died()
			{
				stat.current = 0f;
				died( true );
				if ( on_died != null )
					on_died();
			}

			public virtual void died( bool is_internal )
			{
				if ( on_died != null )
					on_died();
				else
				{
					debug.log(
						"murio, {0}",
						helper.game_object.name.full( this ) );
				}
			}
		}
	}
}
