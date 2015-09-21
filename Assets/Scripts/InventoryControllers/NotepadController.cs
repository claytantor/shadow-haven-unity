using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utils;
using SimpleJSON;
using System.Collections.Generic;
using System;

public class NotepadController : MonoBehaviour {

	// ========== public 
	public TextAsset notesJsonAsset;

	// ========= private
	private Font arialFont;
	private JSONArray notesJson;
	private List<Note> notes = new List<Note>();
	private GameObject notesListGO;
	private GameObject noteDetailGO;
	private Text noteDetailText;
	
	
	void Awake(){
	
		this.arialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		
		//load the JSON
		var dict = JSON.Parse(notesJsonAsset.text);
		JSONArray notesJson = dict["notes"].AsArray;
		
		// the notepad
		gameObject.SetActive(false);
		
		
		//make the note deatil GO
		this.noteDetailGO = new GameObject("noteDetailGO");
		this.noteDetailGO.transform.SetParent(gameObject.transform, false);
		this.noteDetailGO.SetActive(false);
		
		RectTransform ndrect = noteDetailGO.AddComponent<RectTransform>();
		RectTransformExtensions.SetSize(ndrect,new Vector2(730,330));
				
		GameObject btnBack = UIExtensions.MakeTextColorButton(
			"buttonback", 
			new Vector2(160,30), 
			new Vector2(210, 200), 
			"Go Back",
			arialFont, 18, Color.white,
			Color.gray,
			TextAnchor.MiddleCenter,
			BackCallback);
		
		btnBack.transform.SetParent(this.noteDetailGO.transform, false);				
				
		// note text						
		this.noteDetailText = this.noteDetailGO.AddComponent<Text>();
		this.noteDetailText.font = arialFont;
		this.noteDetailText.color = Color.white;
		this.noteDetailText.fontSize = 18;	
		this.noteDetailText.horizontalOverflow = HorizontalWrapMode.Wrap;
									
		//make the notes list GO
		this.notesListGO = new GameObject("notesListGO");
		this.notesListGO.transform.SetParent(gameObject.transform, false);		
						
		foreach (JSONNode noteNode in notesJson) {
			Debug.Log(noteNode.ToString());
			Note n = new Note(
				(string)noteNode["id"],
				(string)noteNode["title"],
				(string)noteNode["text"]);
			notes.Add(n);	

			int ox = -376;
			int oy = 210;						
			GameObject btnNote = UIExtensions.MakeTextOnlyButton(
				n.GetId(), 
				new Vector2(730,30), 
				new Vector2((float)ox,-(float)(notes.Count*30)+oy), 
				n.GetTitle(),
				arialFont, 18, Color.white, TextAnchor.MiddleLeft,
				ButtonEvent);
			btnNote.transform.SetParent(this.notesListGO.transform, false);
		
		}	
	}
	
	public void BackCallback(string str) {
		Debug.Log("going back with:"+str);
		this.notesListGO.SetActive(true);
		this.noteDetailGO.SetActive(false);	
	}
	
	void ButtonEvent(string noteId){
		foreach(Note n in this.notes){
			if(n.GetId().Equals(noteId)){
				this.notesListGO.SetActive(false);
				this.noteDetailText.text = n.GetText();
				this.noteDetailGO.SetActive(true);				
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SetActive(bool active){
		gameObject.SetActive(active);
	}
	
	
	
}
