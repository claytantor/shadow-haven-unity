using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public GameObject mainGameObject;
	
	public string playerName = "Foobar";
	
	public int playerScore = 9001;
	
	void Awake() {
		DontDestroyOnLoad(mainGameObject);
	}

	// Use this for initialization
	void Start () {
		Application.LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		} 
		
	}
}
