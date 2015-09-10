using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryState {
	public string[] inventory;
	public string baseState;
	
	public InventoryState(string[] inventory, string state){
		this.inventory = inventory;
		this.baseState = state;	
	}
	
//	public bool Equals(InventoryState _i){
//		return (_i.inventory == this.inventory) && (_i.baseState == this.baseState);
//	}
//	
//	public bool Contains(InventoryState _i){
//		return (_i.baseState == this.baseState) && this.inventory.Contains(_i.inventory);
//	}
	
	public bool ContainsAll(InventoryState _i){
		string[] items = this.inventory;
		string[] _items = _i.inventory;
		string a = string.Join(",", items);
		string b = string.Join(",", _items);
		
		var lst = new List<string>();		
		lst.AddRange(_items);
		int matchcount = 0;
		for(int i = 0; i < items.Length; i++){	
			Debug.Log(string.Format("compare state: {0},{1} and {2} is in list {3}",_i.baseState, this.baseState, items[i], b ));  
										
			if( _i.baseState.Equals(this.baseState) && lst.Contains(items[i]) ){
				matchcount+=1;
				Debug.Log(string.Format("MATCH state: {0} value {1}",_i.baseState, items[i])); 
			}				
		}
		
		//must match
		if(matchcount>0 && items.Length > 0){
			string.Format("RULE MATCH state: {0} value {1}",_i.baseState, a);
			return true;
		} else {
			return false;
		}
		
		
	}
	
	
	
}
