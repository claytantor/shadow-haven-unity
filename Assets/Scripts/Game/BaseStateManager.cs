using System;
using SimpleJSON;
using UnityEngine;
using RuleEngine.Engine;
using System.Collections;
using Rules;
using System.Collections.Generic;
using Utils;

public class BaseStateManager 
{
	protected string stateDescription;
	protected string factorsStatus;
	protected RuleEngine<PlayerState> playerStateRuleEngine = new RuleEngine<PlayerState>();
	protected KeyDescription[] keys;
	protected Player player;
	protected JSONArray states;
	
	public virtual string SetState (string stateBaseName) {
		
		player.LastState = stateBaseName;
		
		//needed for rues		
		var crumbStateActual = 
			new PlayerState(CollectionUtils.AsArray(player.GetCrumbList()), stateBaseName);
		
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
		
		factorsStatus = string.Format("ANGER:{0}  DESPAIR:{1}  FEAR:{2}",
		                                 player.Anger, 
		                                 player.Dispair, 
		                                 player.Fear);
		
		return methodName;
		
	}
		

	protected virtual JSONNode InitState(string stateName){
		JSONNode stateNode = FindState(stateName, this.states);
		
		this.stateDescription = (string)stateNode["description"];
		
		keys = MakeKeyListJson(stateNode["keys"].AsArray);
		AddCrumbForState(stateNode["state_crumb"]);
		AddInventoryForState(stateNode["inventory"]);
		ModifyPlayerState(stateNode["factors"]);
		return stateNode;			
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
	
	public void AddCrumbForState(JSONNode stateCrumb){
		//Debug.Log(stateCrumb.ToString());
		if(stateCrumb != null){
			JSONArray itemsL = stateCrumb.AsArray;
			foreach(JSONNode itemNode in itemsL){
				player.AddStateCrumb((string)itemNode);
			}		
		}
	}
	
	public void AddInventoryForState(JSONNode inventoryItems){
		if(inventoryItems != null){
			JSONArray itemsL = inventoryItems.AsArray;
			foreach(JSONNode itemNode in itemsL){
				player.AddInventoryItem((string)itemNode);
			}		
		}
	}
	
	public void ModifyPlayerState(JSONNode factors){
		player.Anger+=factors["anger"].AsInt;
		player.Dispair+=factors["despair"].AsInt;
		player.Fear+=factors["fear"].AsInt;				
	}
	
	public Player Player {
		get {
			return this.player;
		}
		set {
			player = value;
		}
	}		
	
	
}

