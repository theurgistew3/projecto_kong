using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using chibi.tool;

namespace tests.tool
{
	public class Test_cache_object : helper.tests.Scene_test
	{
		public override void Instanciate_scenary()
		{
		}

		[UnityTest]
		public IEnumerator clear_when_is_not_in_the_cache_should_be_true()
		{
			var cache = new Cache_object<object>();
			Assert.AreEqual( cache.cache_objects.Count, 0 );
			bool result = cache.clear( new object() );
			Assert.IsTrue( result );
			yield return new WaitForSeconds( 0.2f );
		}

		[UnityTest]
		public IEnumerator clear_when_is_cache_but_no_expired_should_be_false()
		{
			var cache = new Cache_object<object>();
			object a = new object();
			cache.add( a, Time.time, 1.1f );
			yield return new WaitForSeconds( 0.1f );
			bool result = cache.clear( a );
			Assert.IsFalse( result );
		}

		[UnityTest]
		public IEnumerator clear_when_is_cache_and_expired_should_be_true()
		{
			var cache = new Cache_object<object>();
			object a = new object();
			cache.add( a, Time.time, 0.1f );
			yield return new WaitForSeconds( 0.2f );
			bool result = cache.clear( a );
			Assert.IsTrue( result );
		}

		[UnityTest]
		public IEnumerator add_if_no_in_list_should_be_addded()
		{
			var cache = new Cache_object<object>();
			Assert.AreEqual( cache.cache_objects.Count, 0 );
			bool result = cache.add( new object(), Time.time, 0.1f );
			Assert.AreEqual( cache.cache_objects.Count, 1 );
			Assert.IsTrue( result );
			yield return new WaitForSeconds( 0.1f );
		}

		[UnityTest]
		public IEnumerator add_if_no_expired_should_return_false()
		{
			var cache = new Cache_object<object>();
			object a = new object();
			cache.add( a, Time.time, 1.1f );
			yield return new WaitForSeconds( 0.1f );
			bool result = cache.add( a, Time.time, 0.1f );
			Assert.IsFalse( result );
		}

		[UnityTest]
		public IEnumerator add_when_is_expired_should_add_the_new_one ()
		{
			var cache = new Cache_object<object>();
			object a = new object();
			cache.add( a, Time.time, 0.1f );
			yield return new WaitForSeconds( 0.2f );
			bool result = cache.add( a, Time.time, 0.1f );
			Assert.AreEqual( cache.cache_objects.Count, 1 );
			Assert.AreEqual( cache.cache_objects[0].Item2, Time.time );
			Assert.IsTrue( result );
		}
	}
}
