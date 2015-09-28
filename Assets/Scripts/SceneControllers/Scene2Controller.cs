using System.Collections.Generic;
using RuleEngine.Engine;
using Rules;
using SimpleJSON;
using Utils;
using UnityEngine;
using System.Collections;

public class Scene2Controller : BaseSceneController {
	
	//private State state;
	
	void Start () {	  }
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}						
	}
		
//	IEnumerator InitState(string stateName, State stateValue, JSONArray statesModel){
//		this.state = stateValue;
//		base.InitState( stateName, statesModel);
//		
//		while (this.state == stateValue) {
//			yield return 0;
//		}		
//	}
//	
//	//the states
//	public enum State {
//		SceneStart0,
//		SceneExit0
//	}
//	
//	IEnumerator SceneStart0 () { return InitState("SceneStart0", State.SceneStart0, this.states); }
//	IEnumerator SceneExit0 () { return InitState("SceneExit0", State.SceneExit0, this.states); }					
//	
//	public  string SetState (string stateBaseName) {
//		/*string methodName = base.Get(stateBaseName);
//		Debug.Log("Going to state: " + methodName);
//		
//		System.Reflection.MethodInfo info =
//			GetType().GetMethod(methodName,
//			                    System.Reflection.BindingFlags.NonPublic |
//			                    System.Reflection.BindingFlags.Instance);
//		StartCoroutine((IEnumerator)info.Invoke(this, null));
//		
//		
//		//must set inventory after statey
//		MakeInventory(playerManager.GetInventoryList().ToArray(), inventoryCanvas);
//		
//		if(state == State.SceneExit0){
//			Application.LoadLevel(3);
//		}	*/
//		
//		return stateBaseName;
//		
//	}
	
}