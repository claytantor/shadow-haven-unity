using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrumbState {
	public string[] crumbs;
	public string baseState;
	
	public CrumbState(string[] crumbs, string state){
		this.crumbs = crumbs;
		this.baseState = state;	
	}
	
	
	public bool ContainsAll(CrumbState _i){
		
	
		string[] items = this.crumbs;
		string[] _items = _i.crumbs;
//		Debug.Log("items:"+string.Join(",",items));
//		Debug.Log("_items:"+string.Join(",",_items));
		
		
		if(items.Length != _items.Length){
			return false;
		}
					
		var lst = new List<string>();		
		lst.AddRange(_items);
		int matchcount = 0;
		for(int i = 0; i < items.Length; i++){	
			//Debug.Log(string.Format("compare state: {0},{1} and {2}",_i.baseState, this.baseState, items[i] ));  
										
			if( _i.baseState.Equals(this.baseState) && lst.Contains(items[i]) ){
				matchcount+=1;
				//Debug.Log(string.Format("MATCH state: {0} value {1}",_i.baseState, items[i])); 
			}				
		}
		
			
		if(matchcount == _items.Length && items.Length > 0){
			//string.Format("RULE MATCH state: {0} value {1}",_i.baseState, a);
			return true;
		} else {
			return false;
		}
				
	}
	
	public bool ContainsAny(CrumbState _i){
	
		string[] items = this.crumbs;
		string[] _items = _i.crumbs;
		
//		if(items.Length != _items.Length){
//			return false;
//		}
				
		var lst = new List<string>();		
		lst.AddRange(_items);
		int matchcount = 0;
		for(int i = 0; i < items.Length; i++){	
			//Debug.Log(string.Format("compare state: {0},{1} and {2} is in list {3}",_i.baseState, this.baseState, items[i], b ));  
			
			if( _i.baseState.Equals(this.baseState) && lst.Contains(items[i]) ){
				matchcount+=1;
				//Debug.Log(string.Format("MATCH state: {0} value {1}",_i.baseState, items[i])); 
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
