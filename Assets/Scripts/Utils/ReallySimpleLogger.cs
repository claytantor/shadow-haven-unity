using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Linq;

namespace ReallySimpleLogger
{
	public class ReallySimpleLogger
	{
		static string m_baseDir = null;
		
		static ReallySimpleLogger()
		{
			m_baseDir = AppDomain.CurrentDomain.BaseDirectory +
				AppDomain.CurrentDomain.RelativeSearchPath;
		}
		
		//returns filename in format: YYYMMDD
		public static string GetFilenameYYYMMDD(string suffix, string extension)
		{
			return System.DateTime.Now.ToString("yyyy_MM_dd") 
				+ suffix 
					+ extension;
		}
		
		public static void WriteLog(Type t, String message)
		{
			//just in case: we protect code with try.
			try
			{
				string filename = m_baseDir
					+ GetFilenameYYYMMDD("_LOG", ".log");
				System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);

				sw.WriteLine(string.Format("{0} - {1} - {2}",System.DateTime.Now.ToString(), t.ToString(), message));
				sw.Close();
			} catch (Exception) {}
		}
		
		public static void WriteLog(Exception ex)
		{
			//just in case: we protect code with try.
			try
			{
				string filename = m_baseDir
					+ GetFilenameYYYMMDD("_LOG", ".log");
				System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);
//				XElement xmlEntry = new XElement("logEntry",
//				                                 new XElement("Date", System.DateTime.Now.ToString()),
//				                                 new XElement("Exception",
//				             new XElement("Source", ex.Source),
//				             new XElement("Message", ex.Message),
//				             new XElement("Stack", ex.StackTrace)
//				             )//end exception
//				                                 );
				sw.WriteLine(string.Format("{0} {1} {2}\r\n{3}",System.DateTime.Now.ToString(), ex.Source, ex.Message, ex.StackTrace));
				//has inner exception?
				if (ex.InnerException != null)
				{
//					xmlEntry.Element("Exception").Add( 
//					                                  new XElement("InnerException",
//					             new XElement("Source", ex.InnerException.Source),
//					             new XElement("Message", ex.InnerException.Message),
//					             new XElement("Stack", ex.InnerException.StackTrace))
//					                                  );
					sw.WriteLine(string.Format("{0} {1} {2}\r\n{3}",System.DateTime.Now.ToString(), 
						ex.InnerException.Source, ex.InnerException.Message, ex.InnerException.StackTrace));
				}
				//sw.WriteLine(xmlEntry);
				sw.Close();
			} catch (Exception) {}
		}
		
	}
}
