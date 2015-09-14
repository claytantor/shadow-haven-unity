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
	
	
	public bool ContainsAll(InventoryState _i){
		string[] items = this.inventory;
		string[] _items = _i.inventory;
//		string a = string.Join(",", items);
//		string b = string.Join(",", _items);
		
		if(items.Length != _items.Length){
			return false;
		}
					
		var lst = new List<string>();		
		lst.AddRange(_items);
		int matchcount = 0;
		for(int i = 0; i < items.Length; i++){	
			//Debug.Log(string.Format("compare state: {0},{1} and {2} is in list {3}",_i.baseState, this.baseState, items[i], b ));  
										
			if( _i.baseState.Equals(this.baseState) && lst.Contains(items[i]) ){
				matchcount+=1;
				Debug.Log(string.Format("MATCH state: {0} value {1}",_i.baseState, items[i])); 
			}				
		}
		
		//must match
//		Debug.Log(string.Format("matchcount:{0} == _items.Length:{1} and items.Length:{2} > 0", 
//			matchcount, _items.Length, items.Length));
			
		if(matchcount == _items.Length && items.Length > 0){
			//string.Format("RULE MATCH state: {0} value {1}",_i.baseState, a);
			return true;
		} else {
			return false;
		}
				
	}
	
	public bool ContainsAny(InventoryState _i){
	
		string[] items = this.inventory;
		string[] _items = _i.inventory;
//		string a = string.Join(",", items);
//		string b = string.Join(",", _items);
		
		if(items.Length != _items.Length){
			return false;
		}
				
		var lst = new List<string>();		
		lst.AddRange(_items);
		int matchcount = 0;
		for(int i = 0; i < items.Length; i++){	
			//Debug.Log(string.Format("compare state: {0},{1} and {2} is in list {3}",_i.baseState, this.baseState, items[i], b ));  
			
			if( _i.baseState.Equals(this.baseState) && lst.Contains(items[i]) ){
				matchcount+=1;
				Debug.Log(string.Format("MATCH state: {0} value {1}",_i.baseState, items[i])); 
			}				
		}
		
		if(matchcount > 0  && items.Length > 0){
			//string.Format("RULE MATCH state: {0} value {1}",_i.baseState, a);
			return true;
		} else {
			return false;
		}
	}
	
	
	
}
