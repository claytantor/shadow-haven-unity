using System;

public class Note
{
	private string id;
	private string title;
	private string text;
				
	public Note (string id, string title, string text)
	{
		this.id = id;
		this.title = title;
		this.text = text;
	}
	
	public string GetId(){
		return id;
	}

	public string GetTitle(){
		return title;
	}	
	
	public string GetText(){
		return text;
	}	
	
	public string ToString(){
		return string.Format("id:{0} title:{1} text:{2}", this.id, this.title, this.text);
	}	
	
}