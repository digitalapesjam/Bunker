using UnityEngine;
using System;
using System.Collections;

public class Idle : CharacterState
{
	public Idle (CharacterFSM fsm) : base(fsm,(3f + (2.0f*UnityEngine.Random.value)))
	{
		available = true;
	}
	
	protected override CharacterState Progress ()
	{
		return this;
	}
	
	protected override CharacterState Next ()
	{
		if( FSM.IsWillingToPickTarget ) {
			GameObject target = FSM.RandomCharacter();
			return new MoveToTarget(FSM,target,FSM.SelectIntention(target),false);

		} else if ( FSM.IsWillingToGoRandom ) {
			return new GoRandom(FSM, GameObject.Find("Game").GetComponent<GameStart>().RandomPointInPlane(FSM.gameObject.transform.position.y));
		} else {
			return Reset();
		}
	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		if(action == CharacterFSM.UserEventAction.Select) {
			return new WaitPlayerTarget(FSM);
		}else if(action == CharacterFSM.UserEventAction.Scatter) {
			Debug.Log("going random, user scatter");
			return new GoRandom(FSM, GameObject.Find("Game").GetComponent<GameStart>().RandomPointInPlane(FSM.gameObject.transform.position.y));
		}
		return this;
	}
}

