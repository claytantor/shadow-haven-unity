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
	
	public void Save() {		
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
	
	public void AddPlayer(Player p){
		if(!this.savedPlayers.Contains(p)){
			this.savedPlayers.Add(p);
		}
	}
	

	public void SetCurrentPlayer(string id, bool start){
	
		Player p = FindPlayerById(id);
		p.IsStartup = start;
		
		if(p != null){
			this.currentPlayer = p;
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
		//savedPlayers.Add(p);
		this.AddPlayer(p);

		return p;
		
	} 
	
	public Player CurrentPlayer {
		get {
			return this.currentPlayer;
		}
	}
	
}
