using UnityEngine;
using System.Collections;

public class RestartScreen : MonoBehaviour {
	
	private string Title = "You Lost!";
	private string Text = "The last hope of the hearth just died...\nBecause fo YOU!\nDo you want to retry?";
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<GUIText>().text = Title;
		GetComponent<GUIText>().fontSize = Screen.height/10;
		GetComponent<GUIText>().pixelOffset = new Vector2 (Screen.width / 2, -Screen.height / 20);
		if (Input.GetMouseButtonUp(0))
			Application.Quit();
	}
	
	
	void OnGUI ()
	{
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.font = GetComponent<GUIText> ().font;
		GUI.skin.label.fontSize = Screen.height / 20;
		GUI.Label (new Rect (0, Screen.height/5, Screen.width, Screen.height), Text);
		
		GUI.skin.button.font = GetComponent<GUIText> ().font;
		if (GUI.Button (new Rect ((Screen.width/2)-120, 3 * Screen.height / 20 + Screen.height / 2, 100, 50), "Retry"))
			Application.LoadLevel("Bunker");
		if (GUI.Button (new Rect ((Screen.width/2) + 20, 3 * Screen.height / 20 + Screen.height / 2, 100, 50), "Loose"))
			Application.Quit();
	}
}
