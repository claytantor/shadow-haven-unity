using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using RuleEngine.Engine;
using Rules;
using SimpleJSON;
using Utils;

public class Scene1Controller : MonoBehaviour {
	
	public string description;
	public string loadScene = null;
	public KeyDescription[] keys;
	public RuleEngine<CrumbState> playerStateRuleEngine = new RuleEngine<CrumbState>();		
	public GameObject playerGameObject;
	public PlayerManger playerManager;
	public TextAsset scene1Json;	
	public State state;
	public Text textState;
	public Text textFactors;
	public Canvas inventoryCanvas; 
	
	public Button[] buttons;
	
	private JSONArray states;
	
	private string[] button_names = new string[] {"key0", "notepad0"};
	
	//========== mono game methods
	
	void Awake(){
	
		//textStatusUI.text = "DISPAIR:1   ANGER:1   FEAR:1";
		var dict = JSON.Parse(scene1Json.text);
		this.states = dict["states"].AsArray;
		
		//load the buttons
		buttons = new Button[4];
		for(int i = 0; i < buttons.Length; i++){
			buttons[i] = GameObject.Find("/Canvas/ButtonPanel/Button"+i).GetComponent<Button>();
			buttons[i].image.color = new Color(0.9f, 0.9f, 0.9f);
		}	
				
		//create fake player object if not in game
		playerGameObject = GameObject.Find("/PlayerGameObject");
		if(playerGameObject != null){
			playerManager = playerGameObject.GetComponent<PlayerManger>();
		} else {
			playerGameObject = GameObject.Find("/SceneGameObject");
			playerManager = playerGameObject.AddComponent<PlayerManger>();
		}	
											
		//load the rules
		var rulesArray = dict["rules"].AsArray;
		foreach(JSONNode ruleNode in rulesArray){
			playerStateRuleEngine.Add(JsonGameRuleFactory.make(ruleNode));
		}	
		

		
		//myButton.Getcomponent<Button>().onClick.AddListener(action);
		RectTransform objectRectTransform = inventoryCanvas.GetComponent<RectTransform> ();
		Debug.Log("width: " + objectRectTransform.rect.width + ", height: " + objectRectTransform.rect.height);
		
		for(int i = 0; i < button_names.Length; i++){
			//make a single button in unity
			string item = button_names[i];
			GameObject btnGO = new GameObject("Button"+item);
			btnGO.name = "Button"+item;
			btnGO.transform.SetParent(inventoryCanvas.transform, false);
			
			btnGO.AddComponent<RectTransform> ();
			RectTransform rect = btnGO.GetComponent<RectTransform>();
			int size = 50;
			int lbx = (i*size)-192-(int)(size/2);
			int lby = (int)(size/2);
			//int rtx = (((i+1)*size)+(i*3))-195;
			//int rty = 22;
			Debug.Log(string.Format("lb:{0},{1}", lbx,lby));
			RectTransformExtensions.SetSize(rect,new Vector2(size,size));
			RectTransformExtensions.SetLeftTopPosition(rect,new Vector2(lbx,lby));
			//RectTransformExtensions.SetRightTopPosition(rect,new Vector2(rtx,rty));

			
			btnGO.AddComponent<Button>();
			btnGO.GetComponent<Button>().onClick.AddListener(() => {
				//handle click here
				Clicker(item);
			});				
			
			btnGO.AddComponent<Image>();	
			Image img = btnGO.GetComponent<Image> (); 
			img.sprite = Resources.Load <Sprite>(item);
		}				
		
														
	}
	
	void Clicker(string item){
		Debug.Log(item);
	}
	
	
	void Start () {	
		//keyButton = (Texture)Resources.Load("key0");
		SetState("SceneStart");		
	}
	
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
						
	}
	
	
	//========== state machine	
	public enum State {
		SceneStart0,
		SceneStart1,
		SceneStart2,
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
	
	IEnumerator InitState(string stateName, State stateValue, JSONArray statesModel){
		this.state = stateValue;
		JSONNode stateNode = FindState(stateName, statesModel);		
		description = stateNode["description"];		
		MakeKeyListJson(stateNode["keys"].AsArray);
		AddCrumbForState(stateNode);
		ModifyPlayerState(stateNode["factors"]);
		
		while (this.state == stateValue) {
			yield return 0;
		}
		
	}
	
	IEnumerator SceneStart0 () {
		return InitState("SceneStart0", State.SceneStart0, this.states);		
	}
	
	IEnumerator SceneStart1 () {
		return InitState("SceneStart1", State.SceneStart1, this.states);		
	}

	IEnumerator SceneStart2 () {
		return InitState("SceneStart2", State.SceneStart2, this.states);		
	}
	
	IEnumerator GotoBed0 () {		
		return InitState("GotoBed0", State.GotoBed0, this.states);
	}
	
	IEnumerator MirrorView0 () {
		return InitState("MirrorView0", State.MirrorView0, this.states);		
	}	
	
	IEnumerator UnderBed0 () {
		return InitState("UnderBed0", State.UnderBed0, this.states);	
	}
	
	IEnumerator UnderBed1 () {
		return InitState("UnderBed1", State.UnderBed1, this.states);			
	}

	IEnumerator UnderBed2 () {
		return InitState("UnderBed2", State.UnderBed2, this.states);			
	}	
			
	IEnumerator HiddenBox0 () {
		return InitState("HiddenBox0", State.HiddenBox0, this.states);		
	}
	
	IEnumerator HiddenBox1 () {
		return InitState("HiddenBox1", State.HiddenBox1, this.states);		
	}
	
	IEnumerator ReadNote0 () {
		return InitState("ReadNote0", State.ReadNote0, this.states);		
	}	
	
	IEnumerator TakeKey0 () {
		return InitState("TakeKey0", State.TakeKey0, this.states);		
	}
	
	IEnumerator Door0 () {
		return InitState("Door0", State.Door0, this.states);	
	}
	
	IEnumerator Door1 () {
		return InitState("Door1", State.Door1, this.states);	
	}
		
	IEnumerator SceneExit0 () {
		return InitState("SceneExit0", State.SceneExit0, this.states);
	}					
	
	void MakeKeyListJson(JSONArray keyArray){
		int i =0;
		keys = new KeyDescription[keyArray.Count];
		foreach(JSONNode key in keyArray){
			//Debug.Log(key);			
			string keycode = key["keycode"];
			string description = key["description"];
			string state = key["state"];
			string buttonId = key["buttonId"];
			keys[i] = new KeyDescription(keycode, state, description, buttonId);
			i+=1;
		}

	}
	
	string MakeKeyText(KeyDescription[] keys){
		string result = "";
		for(int i = 0; i < keys.Length; i++){
			result+=string.Format("\n\rPress {0} to {1}", keys[i].key, keys[i].action);
		}
		return result;
	}
	
	void ModifyPlayerState(JSONNode factors){
		playerManager.anger+=factors["anger"].AsInt;
		playerManager.despair+=factors["despair"].AsInt;
		playerManager.fear+=factors["fear"].AsInt;				
	}
	
	string MakeStatus(){
		return string.Format("ANGER:{0}  DESPAIR:{1}  FEAR:{2}",
			playerManager.anger, 
		    playerManager.despair, 
			playerManager.fear);
	}
	
	JSONNode FindState(string name, JSONArray states){
		foreach(JSONNode state in states){
			string nameval = state["name"];
			if(nameval.Equals(name)){				
				return state;
			} 
		}
		return null;
	}
	
	void AddCrumbForState(JSONNode state){
		if(state["state_crumb"] != null){
			JSONArray itemsL = state["state_crumb"].AsArray;
			foreach(JSONNode itemNode in itemsL){
				Debug.Log((string)itemNode);
				playerManager.AddStateCrumb((string)itemNode);
			}		
		}
	}
	
	void SetState (string stateBaseName) {
	
		Debug.Log("base state: " + stateBaseName);
		
				
		var crumbStateActual = 
			new CrumbState(
				playerManager.GetCrumbList().ToArray(), stateBaseName);
		
		playerStateRuleEngine.ActualValue = crumbStateActual;
				
		// Get the result
		var resultStates = playerStateRuleEngine.Matches();
		Debug.Log("matched rule count: " + resultStates.ToArray().Length);
		
		//make the default state
		string methodName = stateBaseName+"0";
		if(resultStates.ToArray().Length>0){
			foreach(PlayerCrumbAllStateRule rule in resultStates){
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
		textState.text = string.Format(description, playerManager.playerName);
		textFactors.text = MakeStatus();	
		

		//disable all
		for(int i = keys.Length; i < buttons.Length; i++)
		{
			buttons[i].GetComponentInChildren<Text>().text = "";
			buttons[i].interactable = false;
		}
		
		//enable by key
		for(int i = 0; i < keys.Length; i++)
		{
			KeyDescription kd = keys[i];
			buttons[i].GetComponentInChildren<Text>().text = kd.action;
			buttons[i].image.color = new Color(0.9f, 0.9f, 0.9f);
			buttons[i].interactable = true;			
		}
		
		//playerManager.UpdateState();
		
		if(state == State.SceneExit0){
			Application.LoadLevel(2);
		}	
	}
	
	
	public void ButtonEvent(string btn_event){
		
		for(int i = 0; i < keys.Length; i++)
		{
			KeyDescription kd = keys[i];
			Debug.Log (string.Format("{0} = {1}",btn_event,kd.buttonId));
			if(btn_event.Equals(kd.buttonId)){
				SetState(kd.state);								
			}				
		}
	}
	
	
}