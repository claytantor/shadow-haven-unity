using System.Collections;
using System.IO;

namespace Utils
{
	public static class FileUtils 
	{
		public static string LoadTextFileAsString(string path){
			FileInfo theSourceFile = new FileInfo (path);
			StreamReader reader = theSourceFile.OpenText();
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			string line = "";
			int counter = 0;
			while((line = reader.ReadLine()) != null)
			{
				sb.Append(line);
				counter++;
			}
			
			reader.Close();
			return sb.ToString();		
		}
	}
}

