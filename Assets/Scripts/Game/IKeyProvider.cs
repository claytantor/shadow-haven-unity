using UnityEngine;
using System.Collections;

public interface IKeyProvider<T> {
	 T GetKeys(); 
	 int GetCount();	
}

