using System;
using UnityEngine;
using RuleEngine.Base;
using SimpleJSON;

namespace Rules
{
	static class JsonGameRuleFactory {
		public static BaseRule<InventoryState> make(JSONNode ruleNode){
		
			string ruletype = ruleNode["type"];
			Debug.Log("ruletype:"+ruletype);
			BaseRule<InventoryState> rule = null;
			
			if(ruletype.Equals("PlayerInventoryAllStateRule")){
				JSONArray inventoryJson = ruleNode["inventory"].AsArray;
				string[] iA = new string[inventoryJson.Count];
				
				int index = 0;
				foreach(JSONNode item in inventoryJson){
					iA[index] = (string)item;
					index+=1;
				}
				
				string baseState = ruleNode["baseState"];
				string resultState = ruleNode["resultState"];
				rule = new PlayerInventoryAllStateRule(new InventoryState(iA, baseState), resultState);	
							
			} else if(ruletype.Equals("PlayerInventoryAnyStateRule")){
				JSONArray inventoryJson = ruleNode["inventory"].AsArray;
				string[] iA = new string[inventoryJson.Count];
				
				int index = 0;
				foreach(JSONNode item in inventoryJson){
					iA[index] = (string)item;
					index+=1;
				}
				
				string baseState = ruleNode["baseState"];
				string resultState = ruleNode["resultState"];
				rule = new PlayerInventoryAnyStateRule(new InventoryState(iA, baseState), resultState);				
			}
			// Guard against a NULL rule
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}			
			return rule;			 
		}	
	}
}

