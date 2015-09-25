using System;
using SimpleJSON;

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
	
	private string[] crumbs;
	private string[] inventory;
	private string[] notes;
	
	public Player(){
		this.LastState = "SceneStart";
		this.SceneNumber = 1;
	}
	
	/*"player":{
		"sceneNumber":0,
		"lastState":"SceneStart",
		"fear":10,
		"dispair":10,
		"anger":10,
		"crumbs":[],
		"inventory":[],
		"notes":[]
	},*/
//	public Player(JSONNode playerNode){
//		this.LastState = playerNode["lastState"];
//		this.SceneNumber = playerNode["sceneNumber"].AsInt;
//		this.Fear= playerNode["fear"].AsInt; 
//		this.Anger= playerNode["anger"].AsInt; 
//		this.Dispair= playerNode["dispair"].AsInt; 
//		this.Inventory= playerNode["inventory"].AsArray;
//		this.Crumbs = playerNode["crumbs"].AsArray;
//		this.Notes = playerNode["notes"].AsArray;		
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

	public string[] Crumbs {
		get {
			return this.crumbs;
		}
		set {
			crumbs = value;
		}
	}

	public string[] Inventory {
		get {
			return this.inventory;
		}
		set {
			inventory = value;
		}
	}

	public string[] Notes {
		get {
			return this.notes;
		}
		set {
			notes = value;
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
			Id, Name, Fear, Dispair, Anger, Crumbs, Inventory, Notes, SceneNumber, LastState);
	}	
	
//	public JSONNode ToJSON(){
//		JSONNode playerNode = new JSONNode();
//		playerNode["fear"] = this.Fear;
//		playerNode["dispair"] = this.Dispair;
//		playerNode["anger"] = this.Anger;		
//		return playerNode;
//	}
//	
	
}

