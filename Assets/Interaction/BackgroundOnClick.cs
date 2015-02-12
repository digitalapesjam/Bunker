using UnityEngine;
using System.Collections;

public class BackgroundOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if(!isScatter) {isScatter = Input.GetKeyDown(KeyCode.S);  Debug.Log("isScatter: " + isScatter);  }
		//else if(Input.GetKeyUp(KeyCode.S)){ Debug.Log("no scatter");isScatter = false; }
	}
	
	bool isScatter = false;
	void OnMouseUp ()
	{
		Debug.Log("mouseup!");
		//if(!isScatter) {
			GameObject selected = GameObject.Find ("Game").GetComponent<MakeCharacter> ().SelectedCharacter;
			if(selected != null) {
				selected.GetComponent<CharacterFSM> ().UserDiscard ();
			}
		/*} else {
			Debug.Log("Scatter!");
			foreach(var ch in GameObject.Find ("Game").GetComponent<MakeCharacter> ().Characters) {
				ch.GetComponent<CharacterFSM>().UserScatter();
			}
		}*/
	}
}
