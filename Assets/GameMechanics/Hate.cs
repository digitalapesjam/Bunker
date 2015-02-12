using System;
public class Hate : CharacterState
{
	
	public bool IsTarget;
	
	public Hate (CharacterFSM fsm, bool isTarget) : base(fsm,3f)
	{
		IsTarget = isTarget;
	}
	
	protected override CharacterState Progress ()
	{
		//TODO decrease life points, bla bla
		FSM.Health -= 0.002f;
		
		if(FSM.Health <= 0.01f) {
			return new Dead(FSM);
		}
		
		return this;
	}
	
	protected override CharacterState Next ()
	{
		if(!IsTarget)
			return new Idle(FSM);
		else
			return new GoRandom(FSM,UnityEngine.GameObject.Find("Game").GetComponent<GameStart>().RandomPointInPlane(FSM.gameObject.transform.position.y));

	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		return this;
	}
}

