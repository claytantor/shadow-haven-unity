using System;
using System.Collections.Generic;
using SimpleJSON;

namespace Utils
{
	public static class JSONUtils
	{	
		public static JSONNode ParseFile(string path){
			return JSON.Parse(FileUtils.LoadTextFileAsString(path));			
		}	
		
		public static string[] AsStringArray(JSONArray arrayJson){
			string[] array = new string[arrayJson.Count];
			int index = 0;
			foreach(JSONNode node in arrayJson){
				array[index] = (string)node.ToString();
				index+=1;
			}
			return array;
		}
		
			
	}
}

