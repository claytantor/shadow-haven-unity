using UnityEngine;
using System.Collections;

public class KeyDescription  {

	public string key;
	public string state;
	public string action;
	public string buttonId;	

	public KeyDescription(string key, string state, string action, string buttonId){
		this.key = key;
		this.state = state;
		this.action = action;	
		this.buttonId = buttonId;
	}	

}
