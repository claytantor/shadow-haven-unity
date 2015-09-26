using System;
using SimpleJSON;
using System.Collections.Generic;
using Utils;

[System.Serializable]
public class Player
{
	private string id;
	private string name;
	
	private int sceneNumber=1;
	private string lastState="SceneStart";
	
	private int fear=0;
	private int dispair=0;
	private int anger=0;

	public HashSet<string> state_crumbs = new HashSet<string>();
	public HashSet<string> inventory_items = new HashSet<string>();
	public HashSet<string> notes = new HashSet<string>();
	
	public Player(){
		this.LastState = "SceneStart";
		this.SceneNumber = 1;
	}
	
//	"player":{
//		"sceneNumber":0,
//		"lastState":"SceneStart",
//		"fear":10,
//		"dispair":10,
//		"anger":10,
//		"crumbs":[],
//		"inventory":[],
//		"notes":[]
//	}
	public Player(JSONNode playerNode){
		this.LastState = playerNode["lastState"];
		this.SceneNumber = playerNode["sceneNumber"].AsInt;
		this.Fear = playerNode["fear"].AsInt;
		this.Dispair = playerNode["dispair"].AsInt;
		this.Anger = playerNode["anger"].AsInt;
		this.state_crumbs = CollectionUtils.AsSet(JSONUtils.AsStringArray(playerNode["crumbs"].AsArray));
		this.inventory_items = CollectionUtils.AsSet(JSONUtils.AsStringArray(playerNode["inventory"].AsArray));
		this.notes = CollectionUtils.AsSet(JSONUtils.AsStringArray(playerNode["notes"].AsArray));		
	}
		
	public string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}	
	

	public int Fear {
		get {
			return this.fear;
		}
		set {
			fear = value;
		}
	}

	public int Dispair {
		get {
			return this.dispair;
		}
		set {
			dispair = value;
		}
	}

	public int Anger {
		get {
			return this.anger;
		}
		set {
			anger = value;
		}
	}

		
	public int SceneNumber {
		get {
			return this.sceneNumber;
		}
		set {
			sceneNumber = value;
		}
	}

	public string LastState {
		get {
			return this.lastState;
		}
		set {
			lastState = value;
		}
	}  
	
	public override string ToString ()
	{
		return string.Format ("[Player: Id={0}, Name={1}, Fear={2}, Dispair={3}, Anger={4}, Crumbs={5}, Inventory={6}, Notes={7}, SceneNumber={8}, LastState={9}]", 
		                      Id, Name, Fear, Dispair, Anger, 
		                      CollectionUtils.AsArray(this.state_crumbs), 
		                      CollectionUtils.AsArray(this.inventory_items), 
		                      CollectionUtils.AsArray(this.notes), 
		                      SceneNumber, LastState);
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
	
}

