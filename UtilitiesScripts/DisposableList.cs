using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesScripts {
	public class DisposableList<T> : IList<T> where T : class, IDisposable {
		public static void TestDisposableListCls() {
			DisposableList<StreamReader> dl = new DisposableList<StreamReader>();
			// Create a few test objects.
			StreamReader tr1 = new StreamReader( "C:\\Windows\\system.ini" );
			StreamReader tr2 = new StreamReader( "c:\\Windows\\vmgcoinstall.log" );
			StreamReader tr3 = new StreamReader( "c:\\Windows\\Starter.xml" );
			// Add the test object to the DisposableList.
			dl.Add( tr1 );
			dl.Insert( 0, tr2 );
			dl.Add( tr3 );
			foreach ( StreamReader sr in dl ) {
				Console.WriteLine( $"sr.ReadLine() == {sr.ReadLine()}" );
			}
			// Call Dispose before any of the disposable objects are
			// removed from the DisposableList.
			dl.RemoveAt( 0 );
			dl.Remove( tr1 );
			dl.Clear();
		}

		private List<T> items = new List<T>();

		private void Delete( T item ) => item.Dispose();

		// IList members
		public int IndexOf( T item ) => items.IndexOf( item );

		public void Insert( int index, T item ) => items.Insert( index, item );

		public T this[ int index ]
		{
			get { return items[ index ]; }
			set { items[ index ] = value; }
		}

		public void RemoveAt( int index ) {
			Delete( this[ index ] );
			items.RemoveAt( index );
		}

		// ICollection members
		public void Add( T item ) => items.Add( item );

		public bool Contains( T item ) => items.Contains( item );

		public void CopyTo( T[] array, int arrayIndex ) =>
			items.CopyTo( array, arrayIndex );

		public int Count => items.Count;

		public bool IsReadOnly => false;

		// IEnumerable<T> members
		public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

		// IEnumerable members
		IEnumerator IEnumerable.GetEnumerator() => items.GetEnumerator();

		// Other members
		public void Clear() {
			foreach ( T item in items ) {
				Delete( item );
			}
			items.Clear();
		}

		public bool Remove( T item ) {
			int index = items.IndexOf( item );
			if ( index >= 0 ) {
				Delete( items[ index ] );
				items.RemoveAt( index );
				return true;
			}
			else {
				return false;
			}
		}
	}
}
