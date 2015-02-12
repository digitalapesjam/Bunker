using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	public string Title = "You Won!";
	public string Text = "Credits...";
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<GUIText>().text = Title;
		GetComponent<GUIText>().pixelOffset = new Vector2 (Screen.width / 2, -20);
		if (Input.GetMouseButtonUp(0))
			Application.Quit();
	}
	
	
	void OnGUI ()
	{
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		GUI.skin.label.font = GetComponent<GUIText> ().font;
		GUI.skin.label.fontSize = 20;
		GUI.Label (new Rect (0, 150, Screen.width, Screen.height), Text);
	}
}
