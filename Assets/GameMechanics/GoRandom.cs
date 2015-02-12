using UnityEngine;
using System;

public class GoRandom : CharacterState
{
	
	Vector3 target;
	public GoRandom (CharacterFSM fsm, Vector3 targetPos) : base(fsm,4f+(3*UnityEngine.Random.value))
	{
		target = targetPos;
		available = true;
	}
	
	protected override CharacterState Progress ()
	{
		if(FSM.NearPoint(target)) {
			return new Idle(FSM);
		} else {
			FSM.MoveTowards(target);
			return this;
		}
	}
	
	protected override CharacterState Next ()
	{
		return new Idle(FSM);
	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		switch(action) {
		case CharacterFSM.UserEventAction.Select:
			return new WaitPlayerTarget(FSM);
		}
		return this;
	}
}

