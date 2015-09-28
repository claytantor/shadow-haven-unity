using UnityEngine;
using System.Collections;
using SimpleJSON;
using Rules;

public class Scene1StateManager : BaseStateManager
{
	private State state;
	
	public Scene1StateManager(JSONNode sceneJson){
		
		this.states = sceneJson["states"].AsArray;
		
		//load rules
		var rulesArray = sceneJson["rules"].AsArray;
		foreach(JSONNode ruleNode in rulesArray){
			playerStateRuleEngine.Add(JsonGameRuleFactory.make(ruleNode));
		}	
				
	}
	
	//the states
	public enum State {
		SceneStart0,
		SceneStart1,
		GotoBed0,
		MirrorView0,
		UnderBed0,
		UnderBed1,
		UnderBed2,
		HiddenBox0,
		HiddenBox1,
		TakeKey0,
		ReadNote0,
		Door0,
		Door1,
		SceneExit0,
		Break0
	}
	
	IEnumerator SceneStart0()	{ 	return InitState("SceneStart0", State.SceneStart0);		}
	IEnumerator SceneStart1()	{ 	return InitState("SceneStart1", State.SceneStart1);		}
	IEnumerator GotoBed0()		{ 	return InitState("GotoBed0", 	State.GotoBed0);		}
	IEnumerator MirrorView0()	{ 	return InitState("MirrorView0", State.MirrorView0);		}	
	IEnumerator UnderBed0()		{ 	return InitState("UnderBed0", 	State.UnderBed0);		}
	IEnumerator UnderBed1() 	{ 	return InitState("UnderBed1", 	State.UnderBed1);		}
	IEnumerator UnderBed2() 	{ 	return InitState("UnderBed2", 	State.UnderBed2);		}		
	IEnumerator HiddenBox0() 	{ 	return InitState("HiddenBox0", 	State.HiddenBox0);		}
	IEnumerator HiddenBox1() 	{ 	return InitState("HiddenBox1", 	State.HiddenBox1);		}
	IEnumerator ReadNote0() 	{ 	return InitState("ReadNote0", 	State.ReadNote0);		}	
	IEnumerator TakeKey0() 		{ 	return InitState("TakeKey0", 	State.TakeKey0);		}
	IEnumerator Door0()			{ 	return InitState("Door0", 		State.Door0);			}
	IEnumerator Door1()			{ 	return InitState("Door1", 		State.Door1);			}
	IEnumerator SceneExit0() 	{ 	return InitState("SceneExit0", 	State.SceneExit0);		}
	IEnumerator Break0() 		{ 	return InitState("Break0", 		State.Break0);			}	
	
	IEnumerator InitState(string stateName, State stateValue){
		this.state = stateValue;
		base.InitState( stateName);
		
		while (this.state == stateValue) {
			yield return 0;
		}		
	}
		
	public System.Reflection.MethodInfo SetState (string stateBaseName) {
		string methodName = base.GetStateForPlayer(stateBaseName);
		System.Reflection.MethodInfo info =
			GetType().GetMethod(methodName,
			                    System.Reflection.BindingFlags.NonPublic |
			                    System.Reflection.BindingFlags.Instance);
		return info;
		
	}	
	
}

