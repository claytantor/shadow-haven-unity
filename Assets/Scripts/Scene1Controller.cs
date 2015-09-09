using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RuleEngine.Engine;
using Rules;
using SimpleJSON;

public class Scene1Controller : MonoBehaviour {
	
	public string description;
	public string loadScene = null;
	public KeyDescription[] keys;
	public RuleEngine<InventoryState> playerStateRuleEngine = new RuleEngine<InventoryState>();		
	public GameObject playerGameObject;
	public PlayerManger playerManager;
	public TextAsset scene1Json;
	
	private JSONArray states;
	
	//========== mono game methods
	
	void Awake(){
	
		var dict = JSON.Parse(scene1Json.text);
		this.states = dict["states"].AsArray;
		
		//create fake player object if not in game
		playerGameObject = GameObject.Find("/PlayerGameObject");
		if(playerGameObject != null){
			playerManager = playerGameObject.GetComponent<PlayerManger>();
		} else {
			playerGameObject = GameObject.Find("/SceneGameObject");
			playerManager = playerGameObject.AddComponent<PlayerManger>();
		}					
		
		//rules for states
		//if you open the hidden box with the key, then set state to HiddenBox1
		PlayerInventoryStateRule keyBoxRule1 = 
			new PlayerInventoryStateRule(new InventoryState("key0,","HiddenBox0"), "HiddenBox1");
		playerStateRuleEngine.Add(keyBoxRule1);
		//if you have a key you can open the door
		PlayerInventoryStateRule keyDoorRule1 = 
			new PlayerInventoryStateRule(new InventoryState("key0,","Door0"), "Door1");	
		playerStateRuleEngine.Add(keyDoorRule1);
		
	}
	
	void Start () {	
		SetState("SceneStart");		
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		
		for(int i = 0; i < keys.Length; i++)
		{
			KeyDescription kd = keys[i];
			if(Input.GetKey(kd.key)){
				SetState(kd.state);								
			}				
		}
				
	}
	
	
	//========== state machine	
	public enum State {
		SceneStart0,
		GotoBed0,
		MirrorView0,
		UnderBed0,
		UnderBed1,
		HiddenBox0,
		HiddenBox1,
		TakeKey0,
		ReadNote0,
		Door0,
		Door1,
		SceneExit0
	}
	
	public State state;
	public Text textUI;
	
	IEnumerator SceneStart0 () {
		JSONNode state = FindState("SceneStart0",this.states);		
		description = state["description"];		
		MakeKeyListJson(state["keys"].AsArray);

		while (state == State.SceneStart0) {
			yield return 0;
		}
		
	}
	
	IEnumerator GotoBed0 () {
		
		JSONNode state = FindState("GotoBed0",this.states);		
		description = state["description"];
		MakeKeyListJson(state["keys"].AsArray);
							
		while (state == State.GotoBed0) {
			yield return 0;
		}
	}
	
	IEnumerator MirrorView0 () {
		JSONNode state = FindState("MirrorView0",this.states);		
		description = state["description"];
		MakeKeyListJson(state["keys"].AsArray);		
		
		while (state == State.MirrorView0) {
			yield return 0;
		}	
	}	
	
	IEnumerator UnderBed0 () {
		JSONNode state = FindState("UnderBed0",this.states);		
		description = state["description"];
		MakeKeyListJson(state["keys"].AsArray);		
		
		while (state == State.UnderBed0) {
			yield return 0;
		}		
	}
	
	IEnumerator UnderBed1 () {
		JSONNode state = FindState("UnderBed1",this.states);		
		description = state["description"];
		MakeKeyListJson(state["keys"].AsArray);	
		
		while (state == State.UnderBed1) {
			yield return 0;
		}		
	}
	
	IEnumerator HiddenBox0 () {
		JSONNode state = FindState("HiddenBox0",this.states);		
		description = state["description"];
		MakeKeyListJson(state["keys"].AsArray);			
		
		while (state == State.HiddenBox0) {
			yield return 0;
		}		
	}
	
	IEnumerator HiddenBox1 () {
		JSONNode state = FindState("HiddenBox1",this.states);		
		description = state["description"];
		MakeKeyListJson(state["keys"].AsArray);				
		
		while (state == State.HiddenBox1) {
			yield return 0;
		}
	}
	
	
	IEnumerator TakeKey0 () {
		
		playerManager.AddInventory("key0");
		
		JSONNode state = FindState("TakeKey0",this.states);		
		description = state["description"];		
		MakeKeyListJson(state["keys"].AsArray);
								
		while (state == State.TakeKey0) {
			yield return 0;
		}			
		
	}
	
	IEnumerator Door0 () {

		JSONNode state = FindState("Door0",this.states);		
		description = state["description"];		
		MakeKeyListJson(state["keys"].AsArray);	
				
		while (state == State.Door0) {
			yield return 0;
		}	
	}
	
	IEnumerator Door1 () {

		JSONNode state = FindState("Door0",this.states);		
		description = state["description"];	
		MakeKeyListJson(state["keys"].AsArray);
			
		while (state == State.Door1) {
			yield return 0;
		}			
	}
	
	
	IEnumerator SceneExit0 () {
		this.state = State.SceneExit0;
		JSONNode state = FindState("SceneExit0",this.states);		
		description = state["description"];						
		while (state == State.SceneExit0) {
			yield return 0;
		}
	}					
	
	void MakeKeyListJson(JSONArray keyArray){
		int i =0;
		keys = new KeyDescription[keyArray.Count];
		foreach(JSONNode key in keyArray){
			Debug.Log(key);			
			string keycode = key["keycode"];
			string description = key["description"];
			string state = key["state"];
			keys[i] = new KeyDescription(keycode, state, description);
			i+=1;
		}

	}
	
	string MakeKeyText(KeyDescription[] keys){
		string result = "";
		for(int i = 0; i < keys.Length; i++){
			result+=string.Format("\n\rPress {0} to {1}", keys[i].key, keys[i].action);
		}
		return result;
	}
	
	JSONNode FindState(string name, JSONArray states){
		Debug.Log("FindState:"+name);
		
		
		foreach(JSONNode state in states){
			string nameval = state["name"];
			if(nameval.Equals(name)){				
				return state;
			} 
		}

		return null;
	}
	
	void SetState (string stateBaseName) {
		string methodName = stateBaseName+"0";
		
		var inventoryStateActual = new InventoryState(playerManager.inventoryList,methodName);
		playerStateRuleEngine.ActualValue = inventoryStateActual;
		
		// Get the result
		var resultStates = playerStateRuleEngine.Matches();
		
		if(resultStates.ToArray().Length>0){
			foreach(PlayerInventoryStateRule rule in resultStates){	
				methodName = rule.GetState(stateBaseName);		
			}
		} 
		
		Debug.Log("Going to state: " + methodName);
		
		System.Reflection.MethodInfo info =
			GetType().GetMethod(methodName,
			                    System.Reflection.BindingFlags.NonPublic |
			                    System.Reflection.BindingFlags.Instance);
		StartCoroutine((IEnumerator)info.Invoke(this, null));
		
				
		//{0} player name
		textUI.text = string.Format(description, playerManager.playerName)+MakeKeyText(keys);	
		
		if(state == State.SceneExit0){
			Application.LoadLevel(2);
		}	
	}
	
	
}