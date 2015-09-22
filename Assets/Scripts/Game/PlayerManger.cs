using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerManger : MonoBehaviour {

	public GameObject playerGameObject;
	public string id;
	public string playerName = "Foobar";
	
	public int despair = 0;
	public int anger = 0;
	public int fear = 0;
	
	public Text textFactorsUI;
	
	public HashSet<string> state_crumbs = new HashSet<string>();
	public HashSet<string> inventory_items = new HashSet<string>();
	public HashSet<string> notes = new HashSet<string>();
	
	public List<Player> savedPlayers = new List<Player>();
	
	public Player currentPlayer;
	

	
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
	
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedPlayers.gd");
		bf.Serialize(file, savedPlayers);
		file.Close();
	}	
	
	public void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedPlayers.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedPlayers.gd", FileMode.Open);
			savedPlayers = (List<Player>)bf.Deserialize(file);
			file.Close();
		}
	}
	
	public Player CreatePlayer(){
		
		Player p = new Player();
		DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
		double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
		p.id = string.Format("{0}",timestamp.ToString());
		savedPlayers.Add(p);

		return p;
		
	} 
	

	
}
