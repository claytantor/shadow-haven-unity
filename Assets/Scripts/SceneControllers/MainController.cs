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
			MakePlayerButton(player.Name, player.Id, index);
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
		p.Name = nameText.text;
		playerManger.currentPlayer.Id = p.Id;
		playerManger.Save();
					
		MakePlayerButton(p.Name, p.Id, playerManger.savedPlayers.Count-1);
		
	}
	
	void MakePlayerButton(string buttonName, string pId, int index){
		
		int ox = -376;
		int oy = 210;						
		GameObject btnPlayer = UIExtensions.MakeTextIdCallbackOnlyButton(
			buttonName, 
			pId,
			new Vector2(730,30), 
			new Vector2((float)ox,-(float)(index*30)+oy), 
			buttonName, 
			font, 18, Color.white, TextAnchor.MiddleLeft,
			PlayerStartEvent);
		
		btnPlayer.transform.SetParent(playerListCanvas.transform, false);		
	}
	
	void PlayerStartEvent(string id){
		Debug.Log("starting game for player with id:"+id);
		playerManger.SetCurrentPlayer(id);
	
		Application.LoadLevel(1);
	}
	
}
