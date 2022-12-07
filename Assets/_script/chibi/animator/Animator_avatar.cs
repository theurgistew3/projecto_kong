using UnityEngine;
using chibi.dialog;

namespace chibi.animator.avatar
{
	public enum Emotions
	{
		normal, normal_close_eye, angry, hurt, surprised, happy,
	}

	public class Animator_avatar : Animator_base
	{
		public struct animator_vars
		{
			public const string EMOTION = "emotion";
		}

		public Transform transform_avatar;

		[SerializeField]
		[HideInInspector]
		protected Emotions _emotion = Emotions.normal;

		#region Var public
		[SerializeField]
		public Emotions emotion
		{
			get {
				return _emotion;
				// return (Emotions)animator.GetInteger( animator_vars.EMOTION );
			}
			set {
				_emotion = value;
				animator.SetInteger( animator_vars.EMOTION, (int)value );
			}
		}
		#endregion

		public virtual void set_properties( Actor_propeties property )
		{
			if ( !transform_avatar )
				Debug.LogError( string.Format(
					"el '{0}' no tiene asignado el transform del avatar",
					helper.game_object.name.full( this ) ) );
			if ( property.mirrored )
				transform_avatar.localScale = new Vector3(
					-transform_avatar.localScale.x,
					transform_avatar.localScale.y,
					transform_avatar.localScale.z );
			else
				transform_avatar.localScale = new Vector3(
					Mathf.Abs( transform_avatar.localScale.x ),
					transform_avatar.localScale.y,
					transform_avatar.localScale.z );
		}

		protected override void _init_cache()
		{
			base._init_cache();
			if ( !transform_avatar )
			{
				Debug.LogError( string.Format(
					"el avatar '{0}' no tiene modelo asignado se intade de buscar" +
					"uno",
					helper.game_object.name.full( this ) ) );
				transform_avatar = transform.Find( "model" );
			}
			emotion = emotion;
		}
	}
}
