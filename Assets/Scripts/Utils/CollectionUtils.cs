using System;
using System.Collections.Generic;
using System.Collections;

namespace Utils
{
	public static class CollectionUtils
	{
		public static List<string> AsList(string[] array){
			if (array == null)
				return null;
			List<string> list = new List<string>();
			foreach(string item in array)
				list.Add(item);
					
			return list;
		} 
		
		public static HashSet<string> AsSet(string[] array){
			if (array == null)
				return null;
			HashSet<string> list = new HashSet<string>();
			foreach(string item in array)
				list.Add(item);
			
			return list;
		} 
		
		public static string[] AsArray(ICollection<string> collection){
			if (collection == null)
				return null;
			string[] array = new string[collection.Count];
			int index = 0;
			foreach(string item in collection){
				array[index] = item;
				index+=1;
			}
			return array;			
		}
		
		public static string[] AsArray(IEnumerable<string> collection){
			if (collection == null)
				return null;
			string[] array = new string[Count(collection)];
			int index = 0;
			foreach(string item in collection){
				array[index] = item;
				index+=1;
			}
			return array;			
		}
		
		public static int Count<TSource>(IEnumerable<TSource> source)
		{
			if (source == null)
				return -1;
				
			ICollection<TSource> is2 = source as ICollection<TSource>;
			if (is2 != null)
			{
				return is2.Count;
			}
			ICollection is3 = source as ICollection;
			if (is3 != null)
			{
				return is3.Count;
			}
			int num = 0;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num++;
				}
			}
			return num;
		}
	}
}

