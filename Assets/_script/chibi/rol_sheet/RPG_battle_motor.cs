using UnityEngine;
using System.Collections.Generic;
using chibi.rol_sheet.buff;
using chibi.inventory.item.damage;


namespace chibi.rol_sheet.motor
{
	public class RPG_battle_motor: chibi.Chibi_behaviour
	{
		public chibi.rol_sheet.Rol_sheet rol_sheet;
		public chibi.rol_sheet.Equipment equipment;

		protected override void _init_cache()
		{
			base._init_cache();
			prepare_rol_sheet();
			prepare_equipment();
		}

		public List<Damage_struct> get_attack_damage()
		{
			List<Damage_struct> result = new List<Damage_struct>();
			List<Damage_struct> damages;

			var weapon = equipment.right_arm;
			if ( weapon.damages == null || weapon.damages.Count == 0 )
			{
				debug.log( "weapon {0} no tiene damages usando default", weapon );
				damages = new List<Damage_struct>();
				damages.Add( get_default_damage() );
			}
			else
				damages = weapon.damages;

			foreach ( var damage in damages )
			{
				var apply_damage = (Damage_struct)damage.Clone();
				apply_damage.owner = rol_sheet;
				result.Add( apply_damage );
			}
			return result;
		}

		public Damage_struct get_default_damage()
		{
			Damage_struct result = new Damage_struct();
			result.damage = chibi.damage.damage.Damage.CreateInstance<
				chibi.damage.damage.Damage>();
			result.damage.name = "default create in rpg battle_motor";
			result.damage.amount = 1f;
			result.amount = 1;
			return result;
		}

		public void take_damage( List<Damage_struct> damages )
		{
			var hp_engine = rol_sheet.hp_engine;
			foreach ( var damage_struct in damages )
			{
				var damage = damage_struct.damage;
				hp_engine.take_damage(
					damage, damage_struct.amount, damage_struct.owner );
			}
		}

		protected virtual void prepare_rol_sheet()
		{
			if ( !rol_sheet )
			{
				debug.warning(
					"no tiene asignado el rol_sheet, se buscara en el padre "
					+ "gameobject uno" );
				rol_sheet = transform.parent.GetComponent< Rol_sheet >();
			}
			if ( !rol_sheet )
			{
				debug.error( "No se encontro el rol sheet" );
			}
		}

		protected virtual void prepare_equipment()
		{
			if ( !equipment )
			{
				debug.warning(
					"no tiene asignado el equipment, se buscara en el padre "
					+ "gameobject uno" );
				equipment = transform.parent.GetComponentInChildren< Equipment >();
			}
			if ( !equipment )
			{
				debug.error( "No se encontro el equipment" );
			}
		}
	}
}