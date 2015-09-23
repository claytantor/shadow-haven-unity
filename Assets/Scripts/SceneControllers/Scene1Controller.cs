using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RuleEngine.Engine;
using Rules;
using SimpleJSON;
using Utils;

public class Scene1Controller : BaseSceneController {
	
	private State state;
	
	void Start () {
		playerManager.sceneNumber = 1;	 
		SetState(playerManager.lastState); 
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}						
	}
	
	
	IEnumerator InitState(string stateName, State stateValue, JSONArray statesModel){
		this.state = stateValue;
		base.InitState( stateName, statesModel);
		
		while (this.state == stateValue) {
			yield return 0;
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
		SceneExit0
	}
	
	IEnumerator SceneStart0 () { return InitState("SceneStart0", State.SceneStart0, this.states); }
	IEnumerator SceneStart1 () { return InitState("SceneStart1", State.SceneStart1, this.states); }
	IEnumerator GotoBed0 () { return InitState("GotoBed0", State.GotoBed0, this.states); }
	IEnumerator MirrorView0 () { return InitState("MirrorView0", State.MirrorView0, this.states); }	
	IEnumerator UnderBed0 () { return InitState("UnderBed0", State.UnderBed0, this.states);	 }
	IEnumerator UnderBed1 () { return InitState("UnderBed1", State.UnderBed1, this.states);	}
	IEnumerator UnderBed2 () { return InitState("UnderBed2", State.UnderBed2, this.states);	}	
	IEnumerator HiddenBox0 () { return InitState("HiddenBox0", State.HiddenBox0, this.states);}
	IEnumerator HiddenBox1 () { return InitState("HiddenBox1", State.HiddenBox1, this.states); }
	IEnumerator ReadNote0 () { return InitState("ReadNote0", State.ReadNote0, this.states); }	
	IEnumerator TakeKey0 () { return InitState("TakeKey0", State.TakeKey0, this.states); }
	IEnumerator Door0 () { return InitState("Door0", State.Door0, this.states); }
	IEnumerator Door1 () { return InitState("Door1", State.Door1, this.states);	 }
	IEnumerator SceneExit0 () { return InitState("SceneExit0", State.SceneExit0, this.states); }					
	
	public override string SetState (string stateBaseName) {
		string methodName = base.SetState(stateBaseName);
		Debug.Log("Going to state: " + methodName);
		
		System.Reflection.MethodInfo info =
			GetType().GetMethod(methodName,
			                    System.Reflection.BindingFlags.NonPublic |
			                    System.Reflection.BindingFlags.Instance);
		StartCoroutine((IEnumerator)info.Invoke(this, null));
				
		//must set inventory after state
		MakeInventory(playerManager.GetInventoryList().ToArray(), inventoryCanvas);
		
		if(state == State.SceneExit0){
			Application.LoadLevel(2);
		}	
		
		return methodName;
	
	}
		
}