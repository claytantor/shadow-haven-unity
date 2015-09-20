using System;
using UnityEngine;
using RuleEngine.Base;
using SimpleJSON;

namespace Rules
{
	static class JsonGameRuleFactory {
		public static BaseRule<CrumbState> make(JSONNode ruleNode){
		
			string ruletype = ruleNode["type"];
			Debug.Log("ruletype:"+ruletype);
			BaseRule<CrumbState> rule = null;
			
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
				rule = new PlayerCrumbAllStateRule(new CrumbState(iA, baseState), resultState);	
							
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
				rule = new PlayerCrumbAnyStateRule(new CrumbState(iA, baseState), resultState);				
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

