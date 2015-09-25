using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
	public static class UIExtensions
	{
		public static GameObject MakeTextOnlyButton(
			string name, Vector2 size, Vector2 position, 
			string buttontext, Font font, int fontsize, Color color, TextAnchor alignment,
			Action<string> callback)
		{
			
			//make the button game object
			GameObject btnGO = new GameObject(name);
			btnGO.name = name;
			
			//xform
			RectTransform brect = btnGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(brect,size);
			RectTransformExtensions.SetLeftTopPosition(brect,position);
			
			//make the button
			Button button = btnGO.AddComponent<Button>();
			button.onClick.AddListener(() => { callback(name); });
			
			
			//make the button text
			GameObject txtGO = new GameObject("text"+name);
			txtGO.name = "text"+name;
			txtGO.transform.SetParent(btnGO.transform, false); 
			
			RectTransform tbrect = txtGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(tbrect,size);
//			RectTransformExtensions.SetLeftTopPosition(brect,position);
			
			txtGO.AddComponent<Text>();
			Text tval = txtGO.GetComponent<Text>();
			tval.alignment = alignment;
			tval.text = buttontext;
			tval.font = font;
			tval.color = color;
			tval.fontSize = fontsize;
			
			return btnGO;	
		}
		
		public static GameObject MakeTextIdCallbackOnlyButton(
			string name, string id, Vector2 size, Vector2 position, 
			string buttontext, Font font, int fontsize, Color color, TextAnchor alignment,
			Action<string> callback)
		{
			
			//make the button game object
			GameObject btnGO = new GameObject(name);
			btnGO.name = name;
			
			//xform
			RectTransform brect = btnGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(brect,size);
			RectTransformExtensions.SetLeftTopPosition(brect,position);
			
			//make the button
			Button button = btnGO.AddComponent<Button>();
			button.onClick.AddListener(() => { callback(id); });
			
			
			//make the button text
			GameObject txtGO = new GameObject("text"+name);
			txtGO.name = "text"+name;
			txtGO.transform.SetParent(btnGO.transform, false); 
			
			RectTransform tbrect = txtGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(tbrect,size);
			

			Text tval = txtGO.AddComponent<Text>();
			tval.alignment = alignment;
			tval.text = buttontext;
			tval.font = font;
			tval.color = color;
			tval.fontSize = fontsize;
						
			return btnGO;	
		}

		public static GameObject MakeTextColorButton(
			string name, Vector2 size, Vector2 position, 
			string buttontext, Font font, int fontsize, Color color, Color buttonColor, TextAnchor alignment,
			Action<string> callback)
		{
			
			//make the button game object
			GameObject btnGO = new GameObject(name);
			btnGO.name = name;
			
			//xform
			RectTransform brect = btnGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(brect,size);
			RectTransformExtensions.SetLeftTopPosition(brect,position);

			//button.image.color = Color.red;
			//add image
			Image img = btnGO.AddComponent<Image> (); 
			img.color = buttonColor;
									
			//make the button
			Button button = btnGO.AddComponent<Button>();
			button.onClick.AddListener(() => { callback(name); });
			
			//button color
			ColorBlock cb = button.colors;
			cb.normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
			cb.disabledColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
			cb.highlightedColor = new Color (0.9f, 0.9f, 0.9f, 0.5f); 
			

			
			//make the button text
			GameObject txtGO = new GameObject("text"+name);
			txtGO.name = "text"+name;
			txtGO.transform.SetParent(btnGO.transform, false); 
			
			RectTransform tbrect = txtGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(tbrect,size);
			
			txtGO.AddComponent<Text>();
			Text tval = txtGO.GetComponent<Text>();
			tval.alignment = alignment;
			tval.text = buttontext;
			tval.font = font;
			tval.color = color;
			tval.fontSize = fontsize;
			
			return btnGO;	
		}
		
		public static GameObject MakeTextColorButton2(
			string name, Vector2 size, Vector2 position, 
			string buttontext, Font font, int fontsize, Color color, Color buttonColor, TextAnchor alignment,
			Action<string> callback)
		{
			
			//make the button game object
			GameObject btnGO = new GameObject(name);
			btnGO.name = name;
			
			//xform
			RectTransform brect = btnGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(brect,size);
			RectTransformExtensions.SetLeftTopPosition(brect,position);

			//button.image.color = Color.red;
			//add image
			Image img = btnGO.AddComponent<Image> (); 
			img.color = buttonColor;
									
			//make the button
			Button button = btnGO.AddComponent<Button>();
			button.onClick.AddListener(() => { callback(name); });
			
			//button color
			ColorBlock cb = button.colors;
			cb.normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
			cb.disabledColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
			cb.highlightedColor = new Color (0.9f, 0.9f, 0.9f, 0.5f); 
			

			
			//make the button text
			GameObject txtGO = new GameObject("text"+name);
			txtGO.name = "text"+name;
			txtGO.transform.SetParent(btnGO.transform, false); 
			
			RectTransform tbrect = txtGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(tbrect,size);
			
			txtGO.AddComponent<Text>();
			Text tval = txtGO.GetComponent<Text>();
			tval.alignment = alignment;
			tval.text = buttontext;
			tval.font = font;
			tval.color = color;
			tval.fontSize = fontsize;
			
			return btnGO;	
		}		
						
		public static GameObject MakeImageButton(
			string name, Vector2 size, Vector2 position, 
			string imagename,
			Action<string> callback)
		{
			
			//make the button game object
			GameObject btnGO = new GameObject(name);
			btnGO.name = name;
			
			RectTransform brect = btnGO.AddComponent<RectTransform>();
			RectTransformExtensions.SetSize(brect,size);
			RectTransformExtensions.SetLeftTopPosition(brect,position);
			
			//make the button
			Button button = btnGO.AddComponent<Button>();
			button.onClick.AddListener(() => { callback(name); });
			
			//add image
			Image img = btnGO.AddComponent<Image> (); 
			img.sprite = Resources.Load <Sprite>(imagename);	
			
						
			return btnGO;	
		}				

	}
}

