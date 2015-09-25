using System;
using UnityEngine;
using RuleEngine.Base;
using SimpleJSON;

namespace Rules
{
	static class JsonGameRuleFactory {
		public static BaseRule<PlayerState> make(JSONNode ruleNode){
		
			string ruletype = ruleNode["type"];
			Debug.Log("ruletype:"+ruletype);
			BaseRule<PlayerState> rule = null;
			
			if(ruletype.Equals("PlayerCrumbAllStateRule")){
				JSONArray crumbJson = ruleNode["state_crumb"].AsArray;
				string[] iA = new string[crumbJson.Count];
				
				int index = 0;
				foreach(JSONNode item in crumbJson){
					iA[index] = (string)item;
					index+=1;
				}
				
				string baseState = ruleNode["baseState"];
				string resultState = ruleNode["resultState"];
				rule = new PlayerCrumbAllStateRule(new PlayerState(iA, baseState), resultState);	
							
			} else if(ruletype.Equals("PlayerCrumbAnyStateRule")){
				JSONArray inventoryJson = ruleNode["state_crumb"].AsArray;
				string[] iA = new string[inventoryJson.Count];
				
				int index = 0;
				foreach(JSONNode item in inventoryJson){
					iA[index] = (string)item;
					index+=1;
				}
				
				string baseState = ruleNode["baseState"];
				string resultState = ruleNode["resultState"];
				rule = new PlayerCrumbAnyStateRule(new PlayerState(iA, baseState), resultState);				
			} 
//			else if(ruletype.Equals("PlayerStateFactorsRule")){
//				//JSONNode playerNode = ruleNode["player"];
//				Player p = new Player(ruleNode["player"]);
//
//				string baseState = ruleNode["baseState"];
//				string resultState = ruleNode["resultState"];
//				rule = new PlayerCrumbAnyStateRule(new PlayerState(p);				
//			}
			// Guard against a NULL rule
			if (rule == null)
			{
				throw new ArgumentNullException("rule is empty");
			}			
			return rule;			 
		}	
	}
}

