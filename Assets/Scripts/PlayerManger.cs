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
	
	public List<string> inventory = new List<string>();
	public string inventoryList;

	
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
		foreach(string itemInv in inventory)
		{
			inventoryList = itemInv+",";
		}
	}
	
	public void SetReaction(int[] reactions){
		despair+=reactions[0];
		anger+=reactions[1];
		fear+=reactions[2];		
	}
}
