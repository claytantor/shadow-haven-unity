using System;
using SimpleJSON;
using UnityEngine;
using RuleEngine.Engine;
using System.Collections;
using Rules;
using System.Collections.Generic;
using UnityEngine.UI;
using Utils;

public class BaseSceneController : MonoBehaviour
{
	public TextAsset sceneJson;
	
	public Canvas stateCanvas;	
	public Canvas stateDescCanvas;
	public Canvas stateButtonsCanvas;
	public Canvas inventoryCanvas; 
	public Canvas notepadCanvas; 	
	
	public Text textFactors;
	public Text textTitle;		
	
	protected KeyDescription[] keys;
	protected RuleEngine<CrumbState> playerStateRuleEngine = new RuleEngine<CrumbState>();
	
	protected GameObject playerGameObject;
	protected string stateDescription;
	
	protected PlayerManger playerManager;
	protected JSONArray states;	
	protected Font font;
	
	protected GameObject textGO;	
	protected Font arialFont;
	protected Text textState;	
	
	
	
	
	public void Awake() {
	
		//load the font
		font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
	
		//create fake player object if not in game
		playerGameObject = GameObject.Find("/MainGameObject");
		if(playerGameObject != null){
			Debug.Log("found player game object");
			playerManager = playerGameObject.GetComponent<PlayerManger>();
		} else {
			Debug.Log("CANNOT find player game object, creating");
			playerGameObject = GameObject.Find("/SceneGameObject");
			playerManager = playerGameObject.AddComponent<PlayerManger>();
		}	
		
		//load the json
		var dict = JSON.Parse(sceneJson.text);
		textTitle.text = (string)dict["sceneInfo"]["name"];
		states = dict["states"].AsArray;
		
		//load rules
		var rulesArray = dict["rules"].AsArray;
		foreach(JSONNode ruleNode in rulesArray){
			playerStateRuleEngine.Add(JsonGameRuleFactory.make(ruleNode));
		}	
		
		RectTransform objectRectTransform = stateDescCanvas.GetComponent<RectTransform> ();
		textGO = new GameObject("textnew");
		textGO.name = "textnew";
		textGO.transform.SetParent(objectRectTransform, false);
		
		textGO.AddComponent<RectTransform> ();
		RectTransform rect = textGO.GetComponent<RectTransform>();
		
		RectTransformExtensions.SetSize(rect,new Vector2(730,1082));
		
		textGO.AddComponent<Text> ();
		textState = textGO.GetComponent<Text>();
		textState.text = "";
		textState.font = font;
		textState.fontSize = 30;
		
		//initialize from last state
		MakeInventory(playerManager.GetInventoryList().ToArray(), inventoryCanvas);
		
		
	}
	
	protected void MakeInventory(string[] inventory_items, Canvas inventoryCanvas){
		RectTransform objectRectTransform = inventoryCanvas.GetComponent<RectTransform> ();
		
		List<string> buttonlst = new List<string>();
		for (int i = 0; i < objectRectTransform.childCount; i++) {
			buttonlst.Add(objectRectTransform.GetChild(i).name);	
		}
		
		for(int i = 0; i < inventory_items.Length; i++){
			//try to get the child
			//make a single button in unity
			string item = inventory_items[i];
			
			//only add if it does not exist
			if(!buttonlst.Contains(item)){
				int size = 50;
				int lbx = (i*size)-350-(int)(size/2);
				int lby = (int)(size/2);				
				GameObject btnGO = 
					Utils.UIExtensions.MakeImageButton(
						item, new Vector2(size,size), new Vector2(lbx,lby), 
						item, InventoryButtonEvent);
				
				btnGO.transform.SetParent(inventoryCanvas.transform, false);
				
			}		
		}					
	}
	
	
	public JSONNode FindState(string name, JSONArray states){
		foreach(JSONNode state in states){
			string nameval = state["name"];
			if(nameval.Equals(name)){				
				return state;
			} 
		}
		return null;
	}
	
	public KeyDescription[] MakeKeyListJson(JSONArray keyArray){
		int i =0;
		KeyDescription[] keys = new KeyDescription[keyArray.Count];
		foreach(JSONNode key in keyArray){
			//Debug.Log(key);			
			string keycode = key["keycode"];
			string description = key["description"];
			string state = key["state"];
			string buttonId = key["buttonId"];
			keys[i] = new KeyDescription(keycode, state, description, buttonId);
			i+=1;
		}
		return keys;		
	}	
	
	public void MakeStateKeyButtons(KeyDescription[] keys, Transform parentTransform){ 
		
		//remove child buttons
		foreach (Transform child in parentTransform) {
			GameObject.Destroy(child.gameObject);
		}
		
		//make a button for each key and add as child		
		for(int i = 0; i < keys.Length; i++){
			int dx = (i*170)-330;
			GameObject btnBack = Utils.UIExtensions.MakeTextColorButton(
				keys[i].buttonId, 
				new Vector2(160,30), 
				new Vector2(dx, 27), 
				keys[i].action,
				font, 18, Color.white,
				Color.gray,
				TextAnchor.MiddleCenter,
				ButtonEvent);
			
			btnBack.transform.SetParent(parentTransform, false);
		}		
	}
	
	public void ButtonEvent(string btn_event){
		
		for(int i = 0; i < keys.Length; i++)
		{
			KeyDescription kd = keys[i];
			if(btn_event.Equals(kd.buttonId)){
				this.SetState(kd.state);								
			}				
		}
	}
	
	void InventoryButtonEvent(string item){
		if(item.Equals("note0") && stateCanvas.gameObject.activeSelf){
			Clicker(item, notepadCanvas.gameObject, new GameObject[] {stateCanvas.gameObject});
		} else {
			Clicker(item, stateCanvas.gameObject, new GameObject[] {notepadCanvas.gameObject});
		}
	}
	
	void Clicker(string item, GameObject goActive, GameObject[] goDisable){			
		
		goActive.SetActive(true);
		
		for (int i = 0; i < goDisable.Length; i++) {
			goDisable[i].SetActive(false);
		}	
	}
	
	protected virtual JSONNode InitState(string stateName, JSONArray statesModel){
		JSONNode stateNode = FindState(stateName, statesModel);
		
		textState.text = (string)stateNode["description"];
		
		keys = MakeKeyListJson(stateNode["keys"].AsArray);
		MakeStateKeyButtons(keys, stateButtonsCanvas.gameObject.transform);
		AddCrumbForState(stateNode["state_crumb"]);
		AddInventoryForState(stateNode["inventory"]);
		ModifyPlayerState(stateNode["factors"]);
		return stateNode;			
	}
	
	public void AddCrumbForState(JSONNode stateCrumb){
		//Debug.Log(stateCrumb.ToString());
		if(stateCrumb != null){
			JSONArray itemsL = stateCrumb.AsArray;
			foreach(JSONNode itemNode in itemsL){
				//Debug.Log("adding crumb:"+(string)itemNode);
				playerManager.AddStateCrumb((string)itemNode);
			}		
		}
	}
	
	public void AddInventoryForState(JSONNode inventoryItems){
		if(inventoryItems != null){
			JSONArray itemsL = inventoryItems.AsArray;
			foreach(JSONNode itemNode in itemsL){
				playerManager.AddInventoryItem((string)itemNode);
			}		
		}
	}
	
	public void ModifyPlayerState(JSONNode factors){
		playerManager.anger+=factors["anger"].AsInt;
		playerManager.despair+=factors["despair"].AsInt;
		playerManager.fear+=factors["fear"].AsInt;				
	}		
	
	public virtual string SetState (string stateBaseName) {
		
		Debug.Log("base state: " + stateBaseName);
		
		//needed for rues		
		var crumbStateActual = 
			new CrumbState(
				playerManager.GetCrumbList().ToArray(), stateBaseName);
		
		Debug.Log("player has crumbs:"+string.Join(",",playerManager.GetCrumbList().ToArray()));
		
		
		playerStateRuleEngine.ActualValue = crumbStateActual;
		
		// Get the result
		var resultStates = playerStateRuleEngine.Matches();
		
		//make the default state
		string methodName = stateBaseName+"0";
		if(resultStates != null && resultStates.ToArray().Length>0){
			foreach(GameRule rule in resultStates){
				methodName = rule.GetState(stateBaseName);		
			}
		} 
		
		textFactors.text = string.Format("ANGER:{0}  DESPAIR:{1}  FEAR:{2}",
		                                 			playerManager.anger, 
		                                 		    playerManager.despair, 
		                                 			playerManager.fear);
					
		return methodName;

	}
	
}

