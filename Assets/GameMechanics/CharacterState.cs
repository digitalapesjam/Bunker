using UnityEngine;
using System.Collections;

public abstract class CharacterState
{
	CharacterFSM fsm;
	bool hasTimeout = false;
	float timeout = -1f;
	float duration = -1f;
	protected bool available = false;
	
	public CharacterFSM FSM {
		get { return fsm; }
	}
	
	public CharacterState (CharacterFSM fsm, float duration)
	{
		this.fsm = fsm;
		this.duration = duration;
		if(duration > 0f) {
			hasTimeout = true;
			timeout = Time.realtimeSinceStartup + duration;
		}
	}
	
	public CharacterState Update(){
		CharacterState ret = this;
		if(IsTimeout) { ret = Next(); }
		else  { ret = Progress(); }
		
		if(ret != this){
			FSM.StateChanged = true;
		}
		return ret;
	}
	
	protected abstract CharacterState Progress();
	
	public CharacterState ConsumeAction(CharacterFSM.UserEventAction action) {
		CharacterState ret = ReactAction(action);
		if(ret != null){ FSM.StateChanged = true; }
		return ret;
	}
	
	protected abstract CharacterState ReactAction(CharacterFSM.UserEventAction action);
	
	virtual protected CharacterState Next() { return null; }
	
	public bool Available { get {return available;} }
	
	protected CharacterState Reset() {
		timeout = Time.realtimeSinceStartup + duration;
		return this;
	}
	
	bool IsTimeout {
		get {
			return hasTimeout && (Time.realtimeSinceStartup > timeout);
		}
	}
	
	protected CharacterFSM GetFSM(GameObject obj){
		return obj.GetComponent<CharacterFSM>();
	}
	protected CharacterState GetState(GameObject obj) {
		return GetFSM(obj).CurrentState;
	}
	
}

