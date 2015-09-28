using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
used for rules
**/
using Utils;
using System.Linq;


public class PlayerState {
	
	private Player p;
	private string baseState;
	
	
	public PlayerState(Player p, string state){
		this.p = p;
		this.baseState = state;
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
	
		string[] items = CollectionUtils.AsArray(p.GetCrumbList());
		string[] _items = _i.Crumbs;
				
		IEnumerable<string> filtered =
			p.GetCrumbList().Except(_items, new StringComparer());
		
		string[] itemsFiltered = CollectionUtils.AsArray(filtered);		
				
		ReallySimpleLogger.ReallySimpleLogger.WriteLog(
			this.GetType(),string.Format("filtered items:{0}",string.Join(",",itemsFiltered)));
			
		foreach(string toRemove in itemsFiltered){
			items = items.Where(val => val != toRemove).ToArray();
		}
						
		ReallySimpleLogger.ReallySimpleLogger.WriteLog(
			this.GetType(),string.Format(
			"comparing player all this crumbs:{0} for state:{1} and rule crumbs:{2} for state:{3}",
			string.Join(",", items),
			this.BaseState,
			string.Join(",", _i.p.GetCrumbList().ToArray()),
			_i.BaseState			
			));
				
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
	
		ReallySimpleLogger.ReallySimpleLogger.WriteLog(
			this.GetType(),string.Format("comparing player any crumbs:{0}",string.Join(",", this.p.GetCrumbList().ToArray())));
	
		string[] items = CollectionUtils.AsArray(p.GetCrumbList());
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
	
	private string[] FindCommon(string[] iArray, string[] _iArray){
		return null;
	}		
	
    string[] Crumbs {
		get {
			return CollectionUtils.AsArray(this.p.GetCrumbList());
		}
	}

	string[] Inventory {
		get {
			return CollectionUtils.AsArray(this.p.GetInventoryList());
		}
	}

	string[] Notes {
		get {
			return CollectionUtils.AsArray(this.p.GetNoteList());
		}
	}

	string BaseState {
		get {
			return this.baseState;
		}
	}

	int Fear {
		get {
			return this.p.Fear;
		}
	}

	int Dispair {
		get {
			return this.p.Dispair;
		}
	}

	int Anger {
		get {
			return this.p.Anger;
		}
	}  	
	
}
