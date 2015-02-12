using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFSM : MonoBehaviour {
	
	public enum CharacterType {
		Emo,
		Fat,
		Old,
		Whale
	}
	
	public enum UserEventAction {
		None,
		Select,
		Redirect,
		Discard,
		Scatter
	}
	public enum Intention {
		Love,
		Hate,
		Talk
	}
	
	public CharacterState CurrentState;
	UserEventAction LastAction;
	
	GameObject target;
	
	public GameObject Target {
		get { return target; }
		set { target = value; }
	}
	
	public CharacterType Type {
		get { return type; }
		set { type = value; SetFeatures(); }
	}
	
	bool directionRight = true;
	
	public bool IsGoingRight { get { return directionRight; } }
	
	CharacterType type;
	
	public float Near = 30f;
	public float RandomWill = 60f;
	
	public float Velocity = 2f;
	public float Empathy = .3f;
	
	public float Health {
		get { return mHealth; }
		set { if(value>=0.0f && value<=1.0f) { mHealth = value; } }
	}
	
	float mHealth = 1f;
	
	void SetFeatures() {
		switch(Type) {
		case CharacterType.Emo:
			Velocity = 2f;
			Empathy = .2f;
			break;
		case CharacterType.Fat:
			Velocity = 1.4f;
			Empathy = .4f;
			break;
		case CharacterType.Old:
			Velocity = 1f;
			Empathy = .6f;
			break;
		case CharacterType.Whale:
			Velocity = 1.6f;
			Empathy = .5f;
			break;
		}
		
		Velocity += (MakeDecision(.5f)?-1f:1f) * (Velocity/10f);
		Empathy += (MakeDecision(.5f)?-1f:1f) * (Empathy/10f);
	}
	
	public void UserSelected() {
		LastAction = CharacterFSM.UserEventAction.Select;
	}
	public void UserRedirect(GameObject target) {
		LastAction = CharacterFSM.UserEventAction.Redirect;
		this.target = target;
	}
	public void UserDiscard() {
		LastAction = CharacterFSM.UserEventAction.Discard;
	}
	public void UserScatter() {
		Debug.Log("User scatter");
		LastAction = CharacterFSM.UserEventAction.Scatter;
	}
	
	public bool StateChanged = false;
	
	bool isDead = false;
	public bool Dead { get {return isDead;} }
	
	public bool IsWillingToPickTarget {
		get {
			return MakeDecision(Empathy);
		}
	}
	
	//it will go randomly if more than 3 people are too near
	public bool IsWillingToGoRandom {
		get {
			System.Predicate<GameObject> pred =
				x => RandomWill <= Distance(x.transform.position);
			if( GameObject.Find("Game").GetComponent<MakeCharacter>().SelectCharacters(pred).Count > 2 ){
				return true;
			}
			return MakeDecision(.5f);
		}
	}
	
	public static bool MakeDecision( float probability ) {
		return Random.value < probability;
	}
	
	public void MoveTowards(Vector3 dest){
		Vector3 dir = dest - transform.position;
		transform.position = (transform.position + (dir.normalized*Velocity*(.5f+Random.value)));
		if(dir.x <= 0) {
			GoingLeft();
		} else {
			GoingRight();
		}
		var myz = transform.position.z;
		GameObject tooNear = null;
		foreach(GameObject go in GameObject.Find("Game").GetComponent<MakeCharacter>().Characters) {
			var z = go.transform.position.z;
			if(go.GetInstanceID() != gameObject.GetInstanceID() && Mathf.Abs(myz-z) < 10f && (Distance(go.transform.position)<15f)){
				Debug.Log("too near: " + go.transform.position + ", " +transform.position + " -- " + Distance(go.transform.position));
				tooNear = go; break;
			}
		}
		if(tooNear != null){
			var myPos = transform.position;
			var trgPos = tooNear.transform.position;
			
			GameObject toMove = null;
			float deltaOnZ = 0f;
			if( ((myPos.z+trgPos.z)/2f) < 875f ) {
				toMove = (myPos.z > trgPos.z) ? gameObject : tooNear;
				deltaOnZ = +10f;
			} else {
				toMove = (myPos.z < trgPos.z) ? gameObject : tooNear;
				deltaOnZ = -10f;
			}
			var curPos = toMove.transform.position;
			toMove.transform.position = new Vector3(curPos.x,curPos.y,curPos.z + deltaOnZ);
			
//			transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z-10f);
		}
		
	}
	public bool NearPoint(Vector3 pt){
		return (pt - transform.position).magnitude < Near;
	}
	
	public float Distance(Vector3 pt){
		return (pt - transform.position).magnitude;
	}
	
	
	public Intention SelectIntention(GameObject target) {
		//TODO: implement selection algorithm!
		
		if(MakeDecision(.2f)){
			return Intention.Talk;
		}
		if(MakeDecision( Empathy * (Health + .4f) )) {
			return Intention.Love;
		} 
		return Intention.Hate;
	}
	
	public GameObject RandomCharacter() {
		List<GameObject> chars = GameObject.Find("Game").GetComponent<MakeCharacter>().Characters;
		GameObject go = null;
		do {
			go = chars [ Random.Range(0,chars.Count) ];
		} while( gameObject.GetInstanceID() == go.GetInstanceID() );
		return go;
	}
	public GameObject NearestCharacter() {
		float nearDst = 1000f;
		GameObject nearest = null;
		foreach(GameObject go in GameObject.Find("Game").GetComponent<MakeCharacter>().Characters) {
			if(nearDst > Distance(go) && gameObject.GetInstanceID() != go.GetInstanceID()) { nearest = go; nearDst = Distance(go); }
		}
		Debug.Log("nearest char. is far: " + nearDst);
		return nearest;
	}
	
	float Distance(GameObject go){
		return (gameObject.transform.position - go.transform.position).magnitude;
	}
	
	// Use this for initialization
	void Start () {
		//initialize to Idle state
		CurrentState = new Idle(this);
		gameObject.GetComponent<CharacterAudioAnimation>().OnFSMStateChanged(CurrentState);
	}
	
	// Update is called once per frame
	void Update () {
		if(isDead) {return;}
		//var LastState = CurrentState;
		
		//react to events
		if( LastAction != UserEventAction.None ) {
			CurrentState = CurrentState.ConsumeAction(LastAction);
			LastAction = UserEventAction.None;
		} else {
			//update state. Timeouts are handled internally in states
			CurrentState = CurrentState.Update();
		}
		
		//if(LastState != CurrentState) {
		if(StateChanged) {
			Debug.Log("State changed to: " + CurrentState.GetType());
			StateChanged = false;
			//oh well, state changed..
			if(CurrentState is Dead) { 
				isDead = true; 
				GameObject.Find("Game").GetComponent<EndGame>().CharacterDeath();
			}
			gameObject.GetComponent<CharacterAudioAnimation>().OnFSMStateChanged(CurrentState);
			GameObject.Find("Game").GetComponent<StateIcons>().OnFSMStateChanged(gameObject,CurrentState);
		}
	}
	
	void SendSwitchDirection(){
		gameObject.GetComponent<CharacterAudioAnimation>().OnDirectionChange(directionRight);
	}
	
	void GoingLeft() {
		if(directionRight) {
			directionRight = false;
			SendSwitchDirection();
		}
	}
	void GoingRight() {
		if(!directionRight) {
			directionRight = true;
			SendSwitchDirection();
		}
	}
	
	public void OnAgentIntent( Intention intent, GameObject agent ) {
		Debug.Log("OnAgentIntent: " + intent.ToString());
		target = agent;
		switch(intent) {
		case Intention.Love:
			CurrentState = new Love(this,true);
			StateChanged = true;
			break;
		case Intention.Hate:
			CurrentState = new Hate(this,true);
			StateChanged = true;
			break;
		case Intention.Talk:
			CurrentState = new Talk(this);
			StateChanged = true;
			break;
		}
		
		bool targetOnRight = (target.transform.position.x - gameObject.transform.position.x) > 0f;
//		Debug.Log(target.transform.position + ", " + gameObject.transform.position);
		if(targetOnRight != directionRight) {
//			Debug.Log("target in other direction! turn! " + gameObject.GetInstanceID());
			directionRight = targetOnRight;
			SendSwitchDirection();
		}
	}
	
	public CharacterFSM FSMFor(GameObject obj){
		return obj.GetComponent<CharacterFSM>();
	}
}
