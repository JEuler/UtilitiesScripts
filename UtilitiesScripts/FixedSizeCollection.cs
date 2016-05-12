using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesScripts {
	public class FixedSizeCollection {
		/// <summary>
		/// Constructor that increments static counter
		/// and sets the maximum number of items
		/// </summary>
		/// <param name="maxItems"></param>
		public FixedSizeCollection( int maxItems ) {
			FixedSizeCollection.InstanceCount++;
			this.Items = new object[ maxItems ];
		}

		/// <summary>
		/// Add an item to the class whose type
		/// is unknown as only object can hold any type
		/// </summary>
		/// <param name="item">item to add</param>
		/// <returns>the index of the item added</returns>
		public int AddItem( object item ) {
			if ( this.ItemCount < this.Items.Length ) {
				this.Items[ this.ItemCount ] = item;
				return this.ItemCount++;
			}
			else {
				throw new Exception( "Item queue is full." );
			}
		}

		/// <summary>
		/// Get an item from the class
		/// </summary>
		/// <param name="index">the index of the item to get</param>
		/// <returns>an item of type object</returns>
		public object GetItem( int index ) {
			if ( index >= this.Items.Length && index >= 0 ) {
				throw new ArgumentOutOfRangeException( nameof( index ) );
			}
			return this.Items[ index ];
		}

		#region Properties
		/// <summary>
		/// Static instance counter hangs off of the type for
		/// StandardClass
		/// </summary>
		public static int InstanceCount { get; set; }

		/// <summary>
		/// The count of items in the class holds
		/// </summary>
		public int ItemCount { get; private set; }

		/// <summary>
		/// The items in the class
		/// </summary>
		private object[] Items { get; set; }

		#endregion

		/// <summary>
		/// ToString override for details of underlying stuff
		/// </summary>
		/// <returns></returns>
		public override string ToString() {
			return $"There are {FixedSizeCollection.InstanceCount} instances of { this.GetType() } and this instance contains { this.ItemCount } items…";
		}
	}
}
