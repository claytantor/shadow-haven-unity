using System;

[System.Serializable]
public class Player
{
	public string id;
	public string name;
	
	public int fear;
	public int dispair;
	public int anger;
	
	public string[] crumbs;
	public string[] inventory;
	public string[] notes;
		
	string Id {
		get {
			return this.id;
		}
		set {
			id = value;
		}
	}

	string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}	
	

	int Fear {
		get {
			return this.fear;
		}
		set {
			fear = value;
		}
	}

	int Dispair {
		get {
			return this.dispair;
		}
		set {
			dispair = value;
		}
	}

	int Anger {
		get {
			return this.anger;
		}
		set {
			anger = value;
		}
	}

	string[] Crumbs {
		get {
			return this.crumbs;
		}
		set {
			crumbs = value;
		}
	}

	string[] Inventory {
		get {
			return this.inventory;
		}
		set {
			inventory = value;
		}
	}

	string[] Notes {
		get {
			return this.notes;
		}
		set {
			notes = value;
		}
	}  
		
	
	
	
}

