using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RuleEngine.Engine;
using Rules;

public class DesolationKeepStoryController : MonoBehaviour {

	public string description;
	public KeyCode[] keys;
	public string[] states;
	public string loadScene = null;
	public RuleEngine<InventoryState> playerStateRuleEngine = new RuleEngine<InventoryState>();		
	private AudioSource audioSource;
	public GameObject playerGameObject;
	public PlayerManger playerManager;
	
	//========== mono game methods
	
	void Awake(){
		
		// @TODO this should have a fake player on game mode
		playerGameObject = GameObject.Find("/PlayerGameObject");
		if(playerGameObject != null){
			playerManager = playerGameObject.GetComponent<PlayerManger>();
		} else {
			playerGameObject = GameObject.Find("/SceneGameObject");
			playerManager = playerGameObject.AddComponent<PlayerManger>();
		}					
		audioSource = GetComponent<AudioSource>();
		
	}
	
	void Start () {	
		CellState("CellStart");		
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
		
		for(int i = 0; i < keys.Length; i++)
		{
			KeyCode item = keys[i];
			if(Input.GetKeyDown(item)){
				CellState(states[i]);								
			}
		}
	}
	
	
	//========== state machine

	public enum State {
		CellStart0,
		GotoBed0,
		MirrorView0,
		HiddenBox0,
		HiddenBox1,
		TakeKey0,
		Door0,
		Door1,
		ExitCell0
	}
	
	public State state;
	public Text textUI;
	
	IEnumerator CellStart0 () {
			
		description = "{0}, you are in a cell of unknown composition approx 2m x 3m long. "+
			"You remember nothing including your name. It feels confined "+
			"and strange writings can be seen on the wall. You feel dull and sad, "+
			"going to bed would be easier than facing your delimma. A mirror on the wall "+
			"is scratched and uneven, its frame seems angular and unnatural."+
			" There is a metallic box on the floor. The door has a heavy bolt lock. "+
			"\n\rPress (S) to go back to bed. "+
			"\n\rPress (M) to look at the mirror. "+
			"\n\rPress (B) to open the box on the floor. "+
			"\n\rPress (L) to look at the lock.";
		
		keys = new KeyCode[] {KeyCode.S, KeyCode.M, KeyCode.B, KeyCode.L };
		states = new string[] { "GotoBed", "MirrorView", "HiddenBox", "Door" };
		while (state == State.CellStart0) {
			yield return 0;
		}

	}
	
	IEnumerator GotoBed0 () {
	
		description = "The bed feels soft, but you can't shake a cold feeling. You feel terrible and sad. Something has "+
			"come over you and there are waves of guilt and self hatred you find it "+
				"hard to shake. Sleeping doesnt help, the more you stay in bed the more tired you feel. "+
				"\n\rPress (C) to get out of bed.";
		
		keys = new KeyCode[] {KeyCode.C };
		states = new string[] { "CellStart" };
		while (state == State.GotoBed0) {
			yield return 0;
		}
	}
	
	IEnumerator MirrorView0 () {
				
		description = "You look at yourself in the mirror. You dont look normal, the person there seems "+
			"like a different version of yourself. An empty stare looks back at you and "+
			"you feel contempt and disgust for the doppleganger that lives on the other side of this silver glass. "+
			"\n\rPress (C) to stop staring at your own hollow grey reflection.";
	
		keys = new KeyCode[] {KeyCode.C };
		states = new string[] { "CellStart" };
		while (state == State.MirrorView0) {
			yield return 0;
		}	
	}	
	
	IEnumerator HiddenBox0 () {
		description = "Picking up the blue metallic container about the size of a shoebox. "+
			"It almost seems alive as you pass your fingers over. Radiating in blue a small glyphic character "+
			"on the top of a foriegn language which confuses. As you open it inside the box is a shimmering key "+
			"made of material you cant quite identify. It feels warm to the touch and vibrates "+
			"slighly, as if it has its own power source.  "+
			"\n\rPress (K) to take the key. "+
			"\n\rPress (C) to avoid the box.";
		
		keys = new KeyCode[] {KeyCode.C, KeyCode.K };
		states = new string[] { "CellStart", "TakeKey" };
		while (state == State.HiddenBox0) {
			yield return 0;
		}		
	}
	
	IEnumerator HiddenBox1 () {
		description = "The box's color has changed to a graphite grey and it feels cold, no writing is now visible "+
			"at all. It seems to have become quiet and deactivated."+
				"\n\rPress (C) to put the empty box down.";
		
		keys = new KeyCode[] {KeyCode.C };
		states = new string[] { "CellStart" };
		while (state == State.HiddenBox1) {
			yield return 0;
		}
	}
	
	
	IEnumerator TakeKey0 () {
		
		playerManager.AddInventory("key1");
		
		description = "As your hand reaches into the box the key starts to glow blue from within and "+
			"you hear an audible whine as if the key has resonate vibarations. The closer your hand moves the higher the pitch of the sickening sound. "+
			"As you near the touch its bizarre whine hurts your ears. Suddenly it goes quiet as your fingers wrap around it and its "+
			"kinetic warmth cools, and wieght is now all you feel in your hand."+
			"\n\rPress (C) to drop the box. "+
			"\n\rPress (L) to try to use the key on the lock.";		
		
		keys = new KeyCode[] {KeyCode.C, KeyCode.L };
		states = new string[] { "CellStart", "Door" };
		while (state == State.TakeKey0) {
			yield return 0;
		}			
	
	}
	
	IEnumerator Door0 () {
		description = "You insert the key into the lock. It opens."+
			"\n\rPress (E) to leave this fetid place.";;
		keys = new KeyCode[] {KeyCode.C, KeyCode.E };
		states = new string[] { "CellStart", "ExitCell" };	
		while (state == State.Door0) {
			yield return 0;
		}			
	}
	
	IEnumerator Door1 () {
		description = "The lock is strong, you cant open it without the key. "+
			"\n\rPress (C) to stop wasting time on the lock.";
		
		keys = new KeyCode[] {KeyCode.C };
		states = new string[] { "CellStart" };	
		while (state == State.Door1) {
			yield return 0;
		}	
	}
	
	IEnumerator ExitCell0 () {
		state = State.ExitCell0;
		description = "You have escaped.";					
		while (state == State.ExitCell0) {
			yield return 0;
		}
	}					
						
	void CellState (string stateBaseName) {
		string methodName = stateBaseName+"0";
			
		var inventoryStateActual = new InventoryState(playerManager.inventoryList,methodName);
		playerStateRuleEngine.ActualValue = inventoryStateActual;
		
		// Get the result
		var resultStates = playerStateRuleEngine.Matches();
		
		if(resultStates.ToArray().Length>0){
			foreach(PlayerInventoryStateRule rule in resultStates){	
				methodName = rule.GetState(stateBaseName);		
			}
		} 
				
		Debug.Log("Going to state: " + methodName);
		
		System.Reflection.MethodInfo info =
			GetType().GetMethod(methodName,
			                    System.Reflection.BindingFlags.NonPublic |
			                    System.Reflection.BindingFlags.Instance);
		StartCoroutine((IEnumerator)info.Invoke(this, null));
		
		//{0} player name
		textUI.text = string.Format(description, playerManager.playerName);	
		
		if(state == State.ExitCell0){
			Application.LoadLevel(2);
		}	
	}
	


}
