using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Utils;

public class PlayerManger : MonoBehaviour {

	public GameObject playerGameObject;

	public int despair = 0;
	public int anger = 0;
	public int fear = 0;
	
	public int sceneNumber;	
	public string lastState;
	
	public Text textFactorsUI;
	
	public HashSet<string> state_crumbs = new HashSet<string>();
	public HashSet<string> inventory_items = new HashSet<string>();
	public HashSet<string> notes = new HashSet<string>();
	
	public List<Player> savedPlayers = new List<Player>();
	
	public string currentPlayerId;
		
	void Awake() {
		DontDestroyOnLoad(playerGameObject);
	}

	// Use this for initialization
	void Start () {
		//this.UpdateState();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AddStateCrumb(string item){
		state_crumbs.Add(item);
	}
	
	public List<string> GetCrumbList(){
		List<string> il = new List<string>();
		foreach(string item in this.state_crumbs){
			il.Add(item);
		}
		return il;
	}
	
	public void AddInventoryItem(string item){
		inventory_items.Add(item);
	}
	
	public List<string> GetInventoryList(){
		List<string> il = new List<string>();
		foreach(string item in this.inventory_items){
			il.Add(item);
		}
		return il;
	}
	
	public void AddNote(string item){
		notes.Add(item);
	}
	
	public List<string> GetNoteList(){
		List<string> il = new List<string>();
		foreach(string item in this.notes){
			il.Add(item);
		}
		return il;
	}
	
	public void SetState(int sceneNumber, string stateBaseName){
		this.sceneNumber = sceneNumber;
		this.lastState = stateBaseName;
	}
	
	public void Save() {
	
		//get the current player from the list
		Player p = FindPlayerById(this.currentPlayerId);
		
		//set values for that player prior to serialization.
		string[] pinv = new string[this.GetInventoryList().Count];
		GetInventoryList().CopyTo(pinv);
		p.Inventory = pinv;
		
		string[] pnotes = new string[GetNoteList().Count];
		GetNoteList().CopyTo(pnotes);
		p.Notes = pnotes;
		
		string[] pcrumbs = new string[GetCrumbList().Count];
		GetCrumbList().CopyTo(pcrumbs);
		p.Crumbs = pcrumbs;
		
		p.SceneNumber = this.sceneNumber;
		p.LastState = this.lastState;		
			
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedPlayers.gd");
		bf.Serialize(file, savedPlayers);
		file.Close();
	}	
	
	public void Load() {
		Debug.Log(Application.persistentDataPath);
		if(File.Exists(Application.persistentDataPath + "/savedPlayers.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedPlayers.gd", FileMode.Open);
			savedPlayers = (List<Player>)bf.Deserialize(file);
			file.Close();
						
		}
	}
	
	// TODO throw exception if player is not found
	public void SetCurrentPlayer(string id){
	
		Player p = FindPlayerById(id);
		
		if(p != null){
			this.currentPlayerId = p.Id;			
			this.state_crumbs = CollectionUtils.AsSet(p.Crumbs);
			this.inventory_items = CollectionUtils.AsSet(p.Inventory);
			this.notes = CollectionUtils.AsSet(p.Notes);
			
			Debug.Log("p.SceneNumber:"+p.SceneNumber);
			if(p.SceneNumber == 0)
				p.SceneNumber = 1;
			this.sceneNumber = p.SceneNumber;
			
			Debug.Log("p.LastState:"+p.LastState);
			if(p.LastState == null)
				p.LastState = "SceneStart";
			this.lastState = p.LastState;
			
			this.anger = p.Anger;
			this.despair = p.Dispair;
			this.fear = p.Fear;
		} else {
			Debug.Log("cant find player with id:"+id);
		}		
	}
		
	
	public Player FindPlayerById(string id){
		foreach(Player p in savedPlayers){
			if(p.Id.Equals(id)){
				return p;
			}
		}
		return null;
	}
	
	public Player CreatePlayer(){
		
		Player p = new Player();
		DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
		double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
		p.Id = string.Format("{0}",timestamp.ToString());
		savedPlayers.Add(p);

		return p;
		
	} 
	

	
}
