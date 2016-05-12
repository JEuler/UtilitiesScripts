using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesScripts {
	static class CollectionExtMethods {
		public static IEnumerable<T> GetAll<T>( this List<T> myList, T searchValue ) =>
			myList.Where( t => t.Equals( searchValue ) );

		public static T[] BinarySearchGetAll<T>( this List<T> myList, T searchValue ) {
			List<T> retObjs = new List<T>();

			int center = myList.BinarySearch( searchValue );
			if ( center > 0 ) {
				retObjs.Add( myList[ center ] );
				int left = center;

				while ( left > 0 && myList[ left - 1 ].Equals( searchValue ) ) {
					left--;
					retObjs.Add( myList[ left ] );
				}
				int right = center;
				while ( right < ( myList.Count - 1 ) && myList[ right + 1 ].Equals( searchValue ) ) {
					right++;
					retObjs.Add( myList[ right ] );
				}
			}
			return retObjs.ToArray();
		}

		public static int CountAll<T>( this List<T> myList, T seachValue ) =>
			myList.GetAll( seachValue ).Count();

		public static int BinarySearchCountAll<T>( this List<T> myList, T searchValue ) =>
			BinarySearchGetAll( myList, searchValue ).Count();

		public static void SerializeToFile<T>(T obj, string dataFile) {
			using (FileStream fileStream = File.Create(dataFile)) {
				BinaryFormatter binFormatter = new BinaryFormatter();
				binFormatter.Serialize(fileStream, obj);
			}
		}

		public static T DeserializeFromFile<T>(string dataFile) {
			T obj;
			using (FileStream fileStream = File.OpenRead(dataFile)) {
				BinaryFormatter binFormatter = new BinaryFormatter();
				obj = (T) binFormatter.Deserialize(fileStream);
			}
			return obj;
		}

		public static byte[] SerializeToBytes<T>(T obj) {
			using (MemoryStream memStream = new MemoryStream()) {
				BinaryFormatter binFormatter = new BinaryFormatter();
				binFormatter.Serialize(memStream, obj);
				return memStream.ToArray();
			}
		}

		public static T Deserialize<T>(byte[] serializedObj) {
			T obj = default(T);
			using (MemoryStream memStream = new MemoryStream(serializedObj)) {
				BinaryFormatter binFormatter = new BinaryFormatter();
				obj = (T) binFormatter.Deserialize(memStream);
			}
			return obj;
		}
	}
}
