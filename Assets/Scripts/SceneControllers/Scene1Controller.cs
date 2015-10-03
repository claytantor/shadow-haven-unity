using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RuleEngine.Engine;
using Rules;
using SimpleJSON;
using Utils;

public class Scene1Controller : BaseSceneController {
	
	private Scene1StateManager.State state;
	private Scene1StateManager stateManager;
	
	new void Awake(){
		base.Awake();
		stateManager = new Scene1StateManager(this.sceneJSON){ 
			Player = this.playerManager.FindPlayerById(this.playerManager.CurrentPlayer.Id)
		};
		this.keyProvider = stateManager;
	}
	
	void Start () {
		playerManager.CurrentPlayer.SceneNumber = 1;	
		SetState(playerManager.CurrentPlayer.LastState); 
		
//		stateManager.ModifyPlayerState(stateManager.StateNode["factors"], true);
		
		MakePlayerFactors(playerManager.CurrentPlayer, this.textFactors);	
		
		//playerManager.Save();		
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}						
	}				
	
	public string SetState (string stateBaseName) {
		
		System.Reflection.MethodInfo info = stateManager.SetState(stateBaseName);
		StartCoroutine((IEnumerator)info.Invoke(stateManager, null));
		
		textState.text = stateManager.StateDescription;
		
		Debug.Log("Going to state: " + info.Name);
		
		//make the state buttons
		MakeStateKeyButtons(keyProvider.GetKeys(), stateButtonsCanvas.transform);
		
		//must set inventory after state
		MakeInventory(playerManager.CurrentPlayer.GetInventoryList().ToArray(), inventoryCanvas);
		
		MakePlayerFactors(playerManager.CurrentPlayer, this.textFactors);
		playerManager.Save();
		
		if(info.Name.Equals("SceneExit0")){
			Application.LoadLevel(2);
		}		
		
		return info.Name;
		
	}
	
	public void MakeStateKeyButtons(List<KeyDescription> keys, Transform parentTransform){ 
		
		//remove child buttons
		foreach (Transform child in parentTransform) {
			GameObject.Destroy(child.gameObject);
		}
		
		//make a button for each key and add as child	
		int i = 0;
		foreach(KeyDescription kd in keys){

			int dx = (i*170)-330;
			GameObject btnBack = Utils.UIExtensions.MakeTextColorButton(
				kd.buttonId, 
				new Vector2(160,30), 
				new Vector2(dx, 27), 
				kd.action,
				font, 18, Color.white,
				Color.gray,
				TextAnchor.MiddleCenter,
				ButtonEvent);
			
			btnBack.transform.SetParent(parentTransform, false);
			
			i+=1;
		}		
	}
	
	public void ButtonEvent(string btn_event){
		
		foreach(KeyDescription kd in this.keyProvider.GetKeys()){
			if(btn_event.Equals(kd.buttonId)){
				SetState(kd.state);								
			}
		}
	}		
						
}