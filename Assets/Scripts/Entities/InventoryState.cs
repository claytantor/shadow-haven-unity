using UnityEngine;
using System.Collections;

public class InventoryState {
	public string inventory;
	public string baseState;
	
	public InventoryState(string inventory, string state){
		this.inventory = inventory;
		this.baseState = state;	
	}
	
	public bool Equals(InventoryState _i){
		return (_i.inventory == this.inventory) && (_i.baseState == this.baseState);
	}
	
	public bool Contains(InventoryState _i){
		return this.inventory.Contains(_i.inventory);
	}
	
	
}
