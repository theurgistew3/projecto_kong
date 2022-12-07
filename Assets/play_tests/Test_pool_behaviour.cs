using UnityEngine;
using NUnit.Framework;
using chibi.pool;

namespace unit_tests.pool.chibi
{
	public class Test_pool_behavior : helper.tests.basic_test
	{
		Pool_behaviour pool_1, pool_2;

		public override void set_up()
		{
			pool_1 = Resources.Load<Pool_behaviour>(
					"tests/object/pool/test_pool_1" );
			pool_2 = Resources.Load<Pool_behaviour>(
					"tests/object/pool/test_pool_2" );
			if ( pool_1 == null )
				Debug.LogError( "no se encontro el pool_1" );
			if ( pool_2 == null )
				Debug.LogError( "no se encontro el pool_2" );
		}

		[Test]
		public void should_exists_the_container_of_the_test_pool()
		{
			var stuff = GameObject.Find(
				helper.consts.game_object_names.stuff );
			Assert.IsNotNull( stuff );
			var container = stuff.transform.Find( pool_1.container_name );
			Assert.IsNotNull( container );
			container = container.transform.Find( pool_1.name );
			Assert.IsNotNull( container );

			container = stuff.transform.Find( pool_2.container_name );
			Assert.IsNotNull( container );
			container = container.transform.Find( pool_2.name );
			Assert.IsNotNull( container );
		}

		[Test]
		public void should_create_the_container()
		{
			var pool = ScriptableObject.CreateInstance<Pool_behaviour>();
			var stuff = GameObject.Find(
				helper.consts.game_object_names.stuff );
			Assert.IsNotNull( stuff );
			var container = stuff.transform.Find( pool.container_name );
			Assert.IsNotNull( container );
			container = container.transform.Find( pool.name );
			Assert.IsNotNull( container );
		}

		[Test]
		public void should_create_new_objects_when_the_pool_is_empty()
		{
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			GameObject obj = pool_1.pop();
			tests_tool.assert.game_object.is_not_null( obj );
			obj = pool_1.pop();
			tests_tool.assert.game_object.is_not_null( obj );
			obj = pool_1.pop();
			tests_tool.assert.game_object.is_not_null( obj );
		}

		[Test]
		public void when_push_should_be_added_to_the_container()
		{
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			GameObject[] objs = new GameObject[]{
				pool_1.pop(), pool_1.pop() };

			pool_1.push( objs[0] );
			Assert.AreEqual( 1, pool_1.count );
			Assert.AreEqual( 1, pool_1.container.childCount );

			pool_1.push( objs[1] );
			Assert.AreEqual( 2, pool_1.count );
			Assert.AreEqual( 2, pool_1.container.childCount );
		}

		[Test]
		public void when_pop_should_get_one_from_the_stack()
		{
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			GameObject[] objs = new GameObject[]{
				pool_1.pop(), pool_1.pop() };

			pool_1.push( objs[0] );
			pool_1.push( objs[1] );

			var obj = pool_1.pop();
			Assert.AreEqual( 1, pool_1.count );
			Assert.AreEqual( 1, pool_1.container.childCount );
			Assert.AreEqual( objs[1], obj );

			obj = pool_1.pop();
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			Assert.AreEqual( objs[0], obj );
		}

		[Test]
		public void the_pool_should_no_share_containers()
		{
			GameObject[] objs_1 = new GameObject[]{
				pool_1.pop(), pool_1.pop() };
			GameObject[] objs_2 = new GameObject[]{
				pool_2.pop(), pool_2.pop() };

			pool_1.push( objs_1[0] );
			pool_1.push( objs_1[1] );
			Assert.AreEqual( 0, pool_2.count );
			Assert.AreEqual( 0, pool_2.container.childCount );

			pool_2.push( objs_2[0] );
			pool_2.push( objs_2[1] );

			Assert.AreEqual( 2, pool_2.count );
			Assert.AreEqual( 2, pool_2.container.childCount );

			var obj = pool_1.pop();
			Assert.AreEqual( 1, pool_1.count );
			Assert.AreEqual( 1, pool_1.container.childCount );
			Assert.AreEqual( 2, pool_2.count );
			Assert.AreEqual( 2, pool_2.container.childCount );
			Assert.AreEqual( objs_1[1], obj );

			obj = pool_1.pop();
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			Assert.AreEqual( 2, pool_2.count );
			Assert.AreEqual( 2, pool_2.container.childCount );
			Assert.AreEqual( objs_1[0], obj );

			obj = pool_2.pop();
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			Assert.AreEqual( 1, pool_2.count );
			Assert.AreEqual( 1, pool_2.container.childCount );
			Assert.AreEqual( objs_2[1], obj );

			obj = pool_2.pop();
			Assert.AreEqual( 0, pool_1.count );
			Assert.AreEqual( 0, pool_1.container.childCount );
			Assert.AreEqual( 0, pool_2.count );
			Assert.AreEqual( 0, pool_2.container.childCount );
			Assert.AreEqual( objs_2[0], obj );
		}
	}
}
