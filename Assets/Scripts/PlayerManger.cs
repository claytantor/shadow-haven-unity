using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerManger : MonoBehaviour {

	public GameObject playerGameObject;
	
	public string playerName = "Foobar";
	
	public int despair = 0;
	public int anger = 0;
	public int fear = 0;
	
	public HashSet<string> inventory = new HashSet<string>();

	
	void Awake() {
		DontDestroyOnLoad(playerGameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AddInventory(string item){
		inventory.Add(item);
	}
	
	public List<string> GetInventoryList(){
		List<string> il = new List<string>();
		foreach(string item in this.inventory){
			il.Add(item);
		}
		return il;
	}
	
}
