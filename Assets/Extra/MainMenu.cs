using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Miscellaneous/Main Menu")]
public class MainMenu : MonoBehaviour {
	
	private string Title = "Bunker";
	private string Text = "A cataclysm fell upon earth, but there is a hope hidden underground.\nThe last humans are hidden in a bunker waiting for end of the destruction,\nwill they survive until the countdown finishes? are they going to get mad?";

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{

		//GetComponent<GUIText> ().text = Title;
		//GetComponent<GUIText> ().fontSize = Screen.height / 10;
		//GetComponent<GUIText> ().pixelOffset = new Vector2 (Screen.width / 2, -Screen.height / 20);
	}


	void OnGUI ()
	{
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.font = GetComponent<GUIText> ().font;
		GUI.skin.label.fontSize = Screen.height / 10;
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height / 8), Title);
		
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.font = GetComponent<GUIText> ().font;
		GUI.skin.label.fontSize = Screen.height / 25;
		GUI.Label (new Rect (0, Screen.height / 7, Screen.width, Screen.height), Text);
		
		GUI.skin.button.font = GetComponent<GUIText> ().font;
		if (GUI.Button (new Rect ((Screen.width / 2) - 100, (Screen.height - Screen.height / 20) - 40, 200, 40), "Play"))
			Application.LoadLevel ("Bunker");
	}
}
