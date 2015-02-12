using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBehaviour : MonoBehaviour{
	
	public enum State
	{
		Talking,
		Quarreling,
		Moving,
		None,
		Dead,
		Wander
	}
	public enum UserAction
	{
		None,Poke,Stop
	}
	
	public State CurrentState = State.None;
	public UserAction lastAction = UserAction.None;
	
	public float Velocity = 0.4f + (Random.value / 4f * (Random.value>.5f?1f:-1f));
	
	public float Social = 0.4f;
	public float Empathy = 0.5f;
	public float Hunger = 0.5f;
	
	public float Happyness = 1.0f;
	
	public float TalkTime = 3f;
	public float QuarrelTime = 3f;
	public float WanderTime = 2.5f;
	
	public GameObject Target {
		get { return target; }
	}
	
	bool HappyTalk = false;
	bool Dead = false;
	float Timeout;
	GameObject target;
	Vector3 fakeWander;
	
	public float ChanceOfMakingPeopleHappy {
		get {
			return Empathy * Happyness;
		}
	}
	
	public float Consumption {
		get {
			return Hunger * (1 - Happyness) + 1;
		}
	}
	
	public float StoppingCost (int prevStopActions)
	{
		float intesity = 0.05f;
		if (CurrentState == State.Quarreling)
			intesity = 0.1f;
		return intesity + prevStopActions * 0.01f;
	}
	
	void Start ()
	{

	}
	
	void Update ()
	{
		if(Dead){ Debug.Log("dead!"); return; }
		if(lastAction != UserAction.None) {
			//perform last action action
			switch(lastAction) {
			case UserAction.Stop:
				if(CurrentState==State.Talking || CurrentState==State.Quarreling){
					Debug.Log("user stopped " + CurrentState.ToString());
					CurrentState=State.None;
					target.GetComponent<CharacterBehaviour>().HaveBeenStopped(this.gameObject);
				}
				break;
			case UserAction.Poke:
				Debug.Log("User makes me happy!");
				//TODO: increase happyness!
				break;
			}
			lastAction = UserAction.None;
		}
		else if(CurrentStateTimedOut()) {
			Debug.Log("State " + CurrentState.ToString() + " timed out");
			//change the action!
			switch(CurrentState){
			case State.Talking:
				if(HappyTalk){
					//CurrentState = CharacterBehaviour.State.None;
					StartWander();
					TargetBehaviour().DoneTalkin(this.gameObject);
				} else {
					Debug.Log("Quarreling!");
					CurrentState = CharacterBehaviour.State.Quarreling;
					Timeout = Time.realtimeSinceStartup + QuarrelTime;
					TargetBehaviour().StartQuarreling(this.gameObject);
					//TODO: decrease happyness!
				}
				break;
			case State.Quarreling:
				Debug.Log("Done quarreling");
				//CurrentState = CharacterBehaviour.State.None;
				StartWander();
				TargetBehaviour().DoneTalkin(this.gameObject);
				break;
			case State.Wander:
				CurrentState = CharacterBehaviour.State.None;
				break;
			}
		}
		else if(CurrentState == CharacterBehaviour.State.Moving && NearTarget()) {
			Timeout = Time.realtimeSinceStartup + TalkTime;
			//engage conversation
			if(TargetBehaviour().EngageConversation(this.gameObject)){
				CurrentState = CharacterBehaviour.State.Talking;
				//TODO: what now? check if we can make happyness?
				if( Random.Range(0,100) < (100*ChanceOfMakingPeopleHappy) ) {
					//happy happy
					HappyTalk = true;
				} else {
					HappyTalk = false;
				}
			} else {
				//just go around..
				StartWander();
			}
		} else if(CurrentState == State.Moving) {
			MoveTowards(target.transform.position);
//			Vector3 dir = target.transform.position - gameObject.transform.position;
//			gameObject.transform.position = (gameObject.transform.position + (dir.normalized*Velocity));
		} else if(CurrentState == State.Wander && NearPoint(fakeWander)) {
			CurrentState = CharacterBehaviour.State.None;
		} else if(CurrentState == State.Wander) {
			MoveTowards(fakeWander);
		} else if(CurrentState == State.None) {
			PickCharacter();
			if(target != null) {
				CurrentState = CharacterBehaviour.State.Moving;
			} else {
				StartWander();
			}
		}
		
	}
	
	
	
	void MoveTowards(Vector3 dest){
		Vector3 dir = dest - transform.position;
		transform.position = (transform.position + (dir.normalized*Velocity*(.5f+Random.value)));
	}
	
	void StartWander() {
		fakeWander = GameObject.Find("Game").GetComponent<GameStart>().RandomPointInPlane(gameObject.transform.position.y);
		CurrentState = CharacterBehaviour.State.Wander;
		Timeout = Time.realtimeSinceStartup + WanderTime;
	}
	CharacterBehaviour TargetBehaviour(){
		return target.GetComponent<CharacterBehaviour>();
	}
	bool CurrentStateTimedOut() {
		if(CurrentState == CharacterBehaviour.State.Moving ||
			CurrentState == CharacterBehaviour.State.None) {
			return false;
		}
		return Timeout < Time.realtimeSinceStartup;
	}
	
	//woo-oohh..somebody wants to talk with us
	public bool EngageConversation(GameObject target) {
		if(CurrentState==State.Dead || CurrentState == CharacterBehaviour.State.Quarreling
			|| CurrentState == State.Talking) {
			//gosh, I'm dead or engaged in another conversation..
			return false;
		}
		//immediately change the state and set the target..discard every previous info
		CurrentState = CharacterBehaviour.State.Talking;
		this.target = target;
		return true;
	}
	public void StartQuarreling(GameObject target) {
		CurrentState = CharacterBehaviour.State.Quarreling;
	}
	public void DoneTalkin(GameObject target) {
		//CurrentState = CharacterBehaviour.State.None;
		StartWander();
	}
	public void HaveBeenStopped(GameObject target) {
		//TODO: decrease happyness
		CurrentState = State.None;
		this.target = null;
	}
	
	GameObject PickCharacter() {
		List<GameObject> nears = GameObject.Find("Game").GetComponent<MakeCharacter>().NearCharachters(this.gameObject);
		if(nears.Count==0) { 
			target = null; 
			Debug.Log("no one near..");
		} else {
			if(Random.Range(0,100) < (this.Social * 100)){
				target = nears[ (int)Random.Range(0,nears.Count) ];
				Debug.Log(gameObject.GetInstanceID() + " choose: "+target.GetInstanceID());
			} else {
				target = null;
				Debug.Log("none");
			}
		}
		return target;
	}
	bool NearPoint(Vector3 pt){
		return (pt - transform.position).magnitude < 10;
	}
	bool NearTarget() {
		return NearPoint(target.transform.position);
//		Vector3 distVect = target.transform.position - transform.position;
//		return distVect.magnitude < 10;
	}
	
	public void Poked ()
	{
		lastAction = CharacterBehaviour.UserAction.Poke;
	}
	
	public void Stopped (int prevStopActions)
	{
		if(CurrentState==State.Talking || CurrentState==State.Quarreling){
			Happyness -= StoppingCost (prevStopActions);
		}

		lastAction = UserAction.Stop;
	}
}
