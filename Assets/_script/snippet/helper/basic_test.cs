using UnityEngine;
using System;
using NUnit.Framework;
// using Unity.Entities;

namespace helper.tests
{
	public class basic_test
	{
		// protected World world;
		[SetUp]
		public virtual void set_up()
		{
		}

	}

	public class Scene_test : basic_test
	{
		protected GameObject scene;

		public virtual string scene_dir
		{
			get { throw new NotImplementedException(); }
		}

		[SetUp]
		public virtual void Instanciate_scenary()
		{
			/*
			var rat = typeof( GameObjectArray );
			var hybridhooks = new System.Type[] {
				rat.Assembly.GetType(
					"Unity.Entities.GameObjectArrayInjectionHook" ),
				rat.Assembly.GetType(
					"Unity.Entities.TransformAccessArrayInjectionHook" ),
				rat.Assembly.GetType(
					"Unity.Entities.ComponentArrayInjectionHook" )
			};

			//world = new World( "test world" );
			foreach ( var hook in hybridhooks )
			{
				InjectionHookSupport.RegisterHook(
					Activator.CreateInstance( hook ) as InjectionHook );
			}
			var manager = World.Active.GetOrCreateManager<EntityManager>();
			*/
			scene = Resources.Load( scene_dir ) as GameObject;
			if ( scene == null )
				Assert.Fail(
					string.Format(
						"no se pudo cargar la scena en '{0}'", scene_dir ) );
			scene = instantiate._( scene );
		}

		[TearDown]
		public virtual void clean_scenary()
		{
			game_object.clean.scene();
		}
	}
}
