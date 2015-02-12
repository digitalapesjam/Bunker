using UnityEngine;
using System.Collections;

public class CharacterOnClick : MonoBehaviour {
	
	float LastPick;
	float MinTimeBtwPicks = .5f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		LastPick = Time.realtimeSinceStartup;
	}
	
	void OnMouseUp ()
	{
		
/*		if(Input.GetMouseButtonUp(1)) {
			Debug.Log("scatter single");
			gameObject.GetComponent<CharacterFSM>().UserScatter();
			return;
		}*/
		
		float now = Time.realtimeSinceStartup;
		
		MakeCharacter mk = GameObject.Find ("Game").GetComponent<MakeCharacter> ();
		
		GameObject selected = mk.SelectedCharacter;
		
		Debug.Log("Elapsed: " + (now-LastPick));
//		if( (now - LastPick) > MinTimeBtwPicks ) {
			LastPick = now;
			if(selected == null ){
				Debug.Log("selected");
				gameObject.GetComponent<CharacterFSM>().UserSelected();
			} else {
			Debug.Log("Redirected");
				selected.GetComponent<CharacterFSM>().UserRedirect(gameObject);				
			}
//		}
		
		/*if(selected == null && (MinTimeBtwPicks > (now-LastPick))) {
			LastPick = now;
			gameObject.GetComponent<CharacterFSM>().UserSelected();
		} else if(selected != null) {
			selected.GetComponent<CharacterFSM>().UserRedirect(gameObject);
		}*/
		
/*		Action act = GameObject.Find ("Game").GetComponent<Action> ();
		
		if (act.CurrentAction == Action.Actions.Poke) {
			gameObject.GetComponent<CharacterBehaviour> ().Poked ();
			act.NumberOfPokesLeft--;
		}
		
		if (act.CurrentAction == Action.Actions.Stop) {
			gameObject.GetComponent<CharacterBehaviour> ().Stopped (act.NumberOfStopActions);
			act.NumberOfStopActions++;
		}*/
	}
}
