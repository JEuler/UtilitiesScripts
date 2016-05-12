using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesScripts {
	[Serializable]
	public class MinMaxValueDictionary<T, U> where U : IComparable<U> {
		protected Dictionary<T, U> internalDictionary = null;

		public MinMaxValueDictionary( U minValue, U maxValue ) {
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			internalDictionary = new Dictionary<T, U>();
		}

		public U MinValue { get; private set; } = default( U );
		public U MaxValue { get; private set; } = default( U );

		public int Count => internalDictionary.Count;

		public Dictionary<T, U>.KeyCollection Keys => internalDictionary.Keys;
		public Dictionary<T, U>.ValueCollection Values => internalDictionary.Values;

		public U this[ T key ]
		{
			get { return internalDictionary[ key ]; }
			set
			{
				if ( value.CompareTo( MinValue ) >= 0 && value.CompareTo( MaxValue ) <= 0 ) {
					internalDictionary[ key ] = value;
				}
				else {
					throw new ArgumentOutOfRangeException( nameof( value ), value,
						$"Value must be within the range {MinValue} to {MaxValue}" );
				}
			}
		}

		public void Add( T key, U value ) {
			if ( value.CompareTo( MinValue ) >= 0 && value.CompareTo( MaxValue ) <= 0 ) {
				internalDictionary.Add( key, value );
			}
			else {
				throw new ArgumentOutOfRangeException( nameof( value ), value,
					$"Value must be within the range {MinValue} to {MaxValue}" );
			}
		}

		public bool ContainsKey( T key ) => internalDictionary.ContainsKey( key );
		public bool ContainValue( U value ) => internalDictionary.ContainsValue( value );

		public override bool Equals( object obj ) => internalDictionary.Equals( obj );
		public IEnumerator GetEnumerator() => internalDictionary.GetEnumerator();
		public override int GetHashCode() => internalDictionary.GetHashCode();

		public void GetObjectData( SerializationInfo info, StreamingContext context ) {
			internalDictionary.GetObjectData( info, context );
		}

		public void OnDeserialization( object sender ) {
			internalDictionary.OnDeserialization( sender );
		}

		public override string ToString() => internalDictionary.ToString();

		public bool TryGetValue( T key, out U value ) => internalDictionary.TryGetValue( key, out value );

		public void Remove( T key ) {
			internalDictionary.Remove( key );
		}

		public void Clear() {
			internalDictionary.Clear();
		}
	}
}
