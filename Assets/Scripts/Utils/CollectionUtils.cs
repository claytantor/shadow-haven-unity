using System;
using System.Collections.Generic;

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
	}
}

