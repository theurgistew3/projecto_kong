using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tactic.board
{
	[System.Serializable]
	public class Column : IList<Cell>
	{
		[SerializeField]
		public List< Cell > cells;

		public Cell this[ int i ]
		{
			get {
				return cells[ i ];
			}
			set {
				cells[ i ] = value;
			}
		}

		Cell IList<Cell>.this[ int index ]
		{
			get {
				return cells[ index ];
			}

			set {
				cells[ index ] = value;
			}
		}

		public int Count
		{
			get {
				return cells.Count;
			}
		}

		public bool IsReadOnly
		{
			get {
				throw new System.NotImplementedException();
			}
		}

		public void Add( Cell item )
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
			throw new System.NotImplementedException();
		}

		public bool Contains( Cell item )
		{
			throw new System.NotImplementedException();
		}

		public void CopyTo( Cell[] array, int arrayIndex )
		{
			throw new System.NotImplementedException();
		}

		public IEnumerator<Cell> GetEnumerator()
		{
			foreach ( var cell in cells )
				yield return cell;
		}

		public int IndexOf( Cell item )
		{
			throw new System.NotImplementedException();
		}

		public void Insert( int index, Cell item )
		{
			throw new System.NotImplementedException();
		}

		public bool Remove( Cell item )
		{
			throw new System.NotImplementedException();
		}

		public void RemoveAt( int index )
		{
			throw new System.NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return cells.GetEnumerator();
		}
	}
}
