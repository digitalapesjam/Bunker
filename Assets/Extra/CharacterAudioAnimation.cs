using UnityEngine;
using System.Collections;

public class CharacterAudioAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnFSMStateChanged (CharacterState newState)
	{
		SelectSound (newState);
		SelectAnimation (newState,newState.FSM.IsGoingRight);
	}
	
	public void OnDirectionChange( bool goingRight )
	{
		//Debug.Log("direction change! " + goingRight);
		SelectAnimation(gameObject.GetComponent<CharacterFSM>().CurrentState,goingRight);
	}
	
	private void SelectSound (CharacterState newState)
	{
		AudioSource s = gameObject.GetComponent<AudioSource> ();
		if (newState is Hate)
			s.clip = Resources.Load ("Sounds/Angry Chipmunk") as AudioClip;
		else if (newState is Dead)
			s.clip = Resources.Load ("Sounds/Goodbye") as AudioClip;
		else if (newState is MoveToTarget)
			s.clip = Resources.Load ("Sounds/Throat Clearing") as AudioClip;
		else if (newState is Love)
			s.clip = Resources.Load ("Sounds/Aww Sympathy") as AudioClip;
		else if (newState is Talk)
			s.clip = Resources.Load ("Sounds/Blah") as AudioClip;
		else if (newState is Idle)
			s.clip = Resources.Load ("Sounds/Heavy Sigh") as AudioClip;

		s.Play();
	}
	
	private void SelectAnimation (CharacterState newState, bool right)
	{
		string name = "Animations/" + gameObject.GetComponent<CharacterFSM>().Type.ToString();
		
		if (newState is Hate)
			name += "Fight";
		else if (newState is Dead)
			name += "Idle"; 
		else if (newState is MoveToTarget || newState is GoRandom)
			name += "Walk"; 
		else if (newState is Love)
			name += "Talk"; 
		else if (newState is Talk)
			name += "Talk"; 
		else if (newState is Idle || newState is WaitPlayerTarget)
			name += "Idle";

		if (!right)
			name += "Left";
		
		//Debug.Log(newState) ;
		
		GetComponentInChildren<Renderer> ().material.mainTexture = Resources.Load (name) as Texture2D;
	}
}
