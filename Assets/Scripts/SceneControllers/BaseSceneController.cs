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
	public TextAsset sceneJSONAsset;
	
	public Canvas stateCanvas;	
	public Canvas stateDescCanvas;
	public Canvas stateButtonsCanvas;
	public Canvas inventoryCanvas; 
	public Canvas notepadCanvas; 	
	
	public Text textFactors;
	public Text textTitle;		
	
	protected KeyDescription[] keys;
	protected RuleEngine<PlayerState> playerStateRuleEngine = new RuleEngine<PlayerState>();
	
	protected GameObject playerGameObject;
	protected string stateDescription;
	
	protected PlayerManger playerManager;
	protected JSONNode sceneJSON;	
	protected Font font;
	
	protected GameObject textGO;	
	protected Font arialFont;
	protected Text textState;
	
	public IKeyProvider<List<KeyDescription>> keyProvider;	
	
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
		sceneJSON = JSON.Parse(sceneJSONAsset.text);
		textTitle.text = (string)sceneJSON["sceneInfo"]["name"];
				
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
		Debug.Log("current player:"+playerManager.CurrentPlayer.ToString());
		MakeInventory(playerManager.CurrentPlayer.GetInventoryList().ToArray(), inventoryCanvas);
		
		
	}
	
	protected void MakePlayerFactors(Player p, Text textFactorsArea){
		textFactorsArea.text = string.Format("Fear={0} Dispair={1} Anger={2}",p.Fear, p.Dispair, p.Anger);
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
	
	//========= properties
	public JSONArray States {
		get {
			return this.sceneJSON["states"].AsArray;
		}
	}
	
}

