using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
used for rules
**/
public class PlayerState {
	
	private string[] crumbs;
	private string[] inventory;
	private string[] notes;
	private string baseState;
	
	private int fear;
	private int dispair;
	private int anger;
	
	
	public PlayerState(string[] crumbs, string state){
		this.crumbs = crumbs;
		this.baseState = state;	
	}
	
	public PlayerState(Player p){
		this.crumbs = p.Crumbs;
		this.baseState = p.LastState;
		
	}
	
	public bool FactorsTotalThreshold(PlayerState _i){
		int totalv = this.Fear+this.Anger+this.Dispair;
		int _totalv = _i.Fear+_i.Anger+_i.Dispair;
		
		if(totalv>_totalv)
			return true;
		else
			return false;
	}
	
	public bool CrumbsContainsAll(PlayerState _i){
			
		string[] items = this.crumbs;
		string[] _items = _i.Crumbs;
		
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
	
	public bool CrumbsContainsAny(PlayerState _i){
	
		string[] items = this.crumbs;
		string[] _items = _i.Crumbs;
						
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
	
	
    string[] Crumbs {
		get {
			return this.crumbs;
		}
	}

	string[] Inventory {
		get {
			return this.inventory;
		}
	}

	string[] Notes {
		get {
			return this.notes;
		}
	}

	string BaseState {
		get {
			return this.baseState;
		}
	}

	int Fear {
		get {
			return this.fear;
		}
	}

	int Dispair {
		get {
			return this.dispair;
		}
	}

	int Anger {
		get {
			return this.anger;
		}
	}  	
	
	
	
}
