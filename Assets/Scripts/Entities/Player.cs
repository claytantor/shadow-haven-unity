using System;
using SimpleJSON;
using System.Collections.Generic;
using Utils;
using System.Runtime.Serialization;

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


	private List<string> state_crumbs = new List<string>();
		
	private List<string> inventory_items = new List<string>();
	
	private List<string> notes = new List<string>();
	
	public Player(){
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
		this.state_crumbs = CollectionUtils.AsList(JSONUtils.AsStringArray(playerNode["crumbs"].AsArray));
		this.inventory_items = CollectionUtils.AsList(JSONUtils.AsStringArray(playerNode["inventory"].AsArray));
		this.notes = CollectionUtils.AsList(JSONUtils.AsStringArray(playerNode["notes"].AsArray));		
	}
	
//	[OnSerializing]
//	private void SetValuesOnSerializing(StreamingContext context)
//	{
//		//convert to arrays for serialization
//		a_state_crumbs = CollectionUtils.AsArray(this.state_crumbs);
//		a_inventory_items = CollectionUtils.AsArray(this.inventory_items);
//		a_notes = CollectionUtils.AsArray(this.notes);
//		
//	}
	
//	[OnDeserializing]
//	private void SetValuesOnDeserializing(StreamingContext context)
//	{
//		//convert to arrays for serialization
//		this.state_crumbs = CollectionUtils.AsSet(this.a_state_crumbs);
//		this.inventory_items = CollectionUtils.AsSet(this.a_inventory_items);
//		this.notes = CollectionUtils.AsSet(this.a_notes);
//		
//	}
		
		
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
		if(!state_crumbs.Contains(item))
			state_crumbs.Add(item);
	}
	
	public List<string> GetCrumbList(){
		return this.state_crumbs;
	}
	
	public void AddInventoryItem(string item){
		if(!inventory_items.Contains(item))
			inventory_items.Add(item);
	}
	
	public List<string> GetInventoryList(){
		return this.inventory_items;
	}
	
	public void AddNote(string item){
		if(!notes.Contains(item))
			notes.Add(item);
	}
	
	public List<string> GetNoteList(){
		return this.notes;
	}
	
	public List<string> State_crumbs {
		get {
			return this.state_crumbs;
		}
		set {
			state_crumbs = value;
		}
	}

	public  List<string> Inventory_items {
		get {
			return this.inventory_items;
		}
		set {
			inventory_items = value;
		}
	}

	public  List<string> Notes {
		get {
			return this.notes;
		}
		set {
			notes = value;
		}
	}		
//	public override string ToString ()
//	{
//		return string.Format ("[Player: id={0}, name={1}, sceneNumber={2}, lastState={3}, "+ 
//			"fear={4}, dispair={5}, anger={6}, a_state_crumbs={7}, a_inventory_items={8}, a_notes={9}]", 
//			id, name, sceneNumber, lastState, fear, dispair, anger, a_state_crumbs, a_inventory_items, a_notes);
//	}
	
	
}

