using UnityEngine;
using System;
public class MoveToTarget : CharacterState
{
	
	bool forced;
	GameObject target;
	CharacterFSM.Intention intention;
	
	public CharacterFSM.Intention Intent {
		get { return intention; }
	}
	
	public MoveToTarget (CharacterFSM fsm, GameObject target, CharacterFSM.Intention intent, bool forced) : base(fsm, 5f+(3*UnityEngine.Random.value))
	{
		FSM.Target = target;
		this.target = target;
		this.forced = forced;
		this.intention = intent;
		
		this.available = !forced;
	}
	
	protected override CharacterState Progress ()
	{
		if(forced && GetState(target) is WaitPlayerTarget) {
			GameObject newTarget = FSM.NearestCharacter();
			return new MoveToTarget(FSM,newTarget,CharacterFSM.Intention.Hate,false);
		}
		
		// if near, check if target is still valid
		if(FSM.NearPoint(target.transform.position) && GetState(target).Available) {
			target.GetComponent<CharacterFSM>().OnAgentIntent(intention,FSM.gameObject);

			CharacterState ret = null;
			switch(intention){
			case CharacterFSM.Intention.Hate:
				ret = new Hate(FSM,false); break;
			case CharacterFSM.Intention.Love:
				ret = new Love(FSM,false); break;
			case CharacterFSM.Intention.Talk:
				ret = new Talk(FSM); break;
			}
			
			//put on same plane, min 20f on x distance
			Vector3 myPos = FSM.gameObject.transform.position;
			Vector3 trgPos = target.transform.position;
			
			float z =  (myPos.z < trgPos.z) ? myPos.z : trgPos.z;  //(myPos.z + trgPos.z) / 2f;
			float dstX = trgPos.x - myPos.x;
			
			float myX = myPos.x;
			float trgX = trgPos.x;
			if(Math.Abs(dstX) < 20f) {
				if(dstX<0f) {
					myX += (20-dstX); trgX -= (20+dstX);
				} else {
					myX -= (20+dstX); trgX += (20-dstX);
				}
			}
			
			FSM.gameObject.transform.position = new Vector3(myX, myPos.y, z);
			target.transform.position = new Vector3(trgX, trgPos.y, z);
			
			return ret;
		} else if(FSM.NearPoint(target.transform.position)) {
			return new Idle(FSM);
		} else {
			// calculate direction and moove
			FSM.MoveTowards(target.transform.position);
			return this;
		}
	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		return this;
	}
	
	protected override CharacterState Next ()
	{
		return new Idle(FSM);
	}
}
