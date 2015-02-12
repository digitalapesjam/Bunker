using System;
//wow..do nothing
public class Talk : CharacterState
{
	public Talk (CharacterFSM fsm) : base(fsm,3f)
	{
	}
	
	protected override CharacterState Progress ()
	{
/*		if(!(FSM.FSMFor(FSM.Target).CurrentState is Talk)) {
			Debug.Log("Talkin' to noone??");
			return new Idle(FSM);
		}*/
		return this;
	}
	
	protected override CharacterState Next ()
	{
		return new Idle(FSM);
	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		return this;
	}
}
