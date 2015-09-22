using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;

public class MainController : MonoBehaviour {
	
	public Button buttonAddPlayer;
	public Canvas playerListCanvas;
	public InputField playerNameField;
	
	
	private PlayerManger playerManger;
	private Font font;
	
	void Awake() {
		font = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		DontDestroyOnLoad(gameObject);
		playerManger = gameObject.AddComponent<PlayerManger>();
		buttonAddPlayer.onClick.AddListener(() => { ButtonEvent(); }); 
		
		playerManger.Load();
		
		int index = 0;
		foreach(Player player in playerManger.savedPlayers){
			MakePlayerButton(player.name, index);
			index+=1;
			
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		} 		
	}
	
	void ButtonEvent(){
		
		Text nameText = playerNameField.gameObject.transform.FindChild("Text").GetComponent<Text>();		
		Debug.Log(nameText.text);
		
		//create a new player
		Player p = playerManger.CreatePlayer();
		p.name = nameText.text;
		
		playerManger.Save();
					
		MakePlayerButton(nameText.text, playerManger.savedPlayers.ToArray().Length-1);
		
	}
	
	void MakePlayerButton(string buttonId, int index){
		
		int ox = -376;
		int oy = 210;						
		GameObject btnPlayer = UIExtensions.MakeTextOnlyButton(
			buttonId, 
			new Vector2(730,30), 
			new Vector2((float)ox,-(float)(index*30)+oy), 
			buttonId,
			font, 18, Color.white, TextAnchor.MiddleLeft,
			PlayerStartEvent);
		
		btnPlayer.transform.SetParent(playerListCanvas.transform, false);		
	}
	
	void PlayerStartEvent(string id){
		Application.LoadLevel(1);
	}
	
}
