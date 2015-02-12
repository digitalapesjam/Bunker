using System;
public class Love : CharacterState
{
	
	public bool IsTarget;
	
	public Love (CharacterFSM fsm, bool isTarget) : base(fsm,5f)
	{
		IsTarget = isTarget;
	}
	
	protected override CharacterState Progress ()
	{
		//TODO: do something!
		FSM.Health += 0.003f;
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

