using UnityEngine;
using System;
public class WaitPlayerTarget : CharacterState
{
	public WaitPlayerTarget (CharacterFSM fsm) : base(fsm,-1f)
	{
		this.available = true;
	}
	
	protected override CharacterState Progress ()
	{
		//well, do nothing, we are waiting..
		return this;
	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		if(action == CharacterFSM.UserEventAction.Redirect) {
			return new MoveToTarget(FSM,FSM.Target,FSM.SelectIntention(FSM.Target), true);
		} else if(action == CharacterFSM.UserEventAction.Discard) {
			return new Idle(FSM);
		}
		return this;
	}
}

