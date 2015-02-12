using System;
public class Dead : CharacterState
{
	public Dead (CharacterFSM fsm) : base(fsm,-1f)
	{
	}
	
	protected override CharacterState Progress ()
	{
		return this;
	}
	
	protected override CharacterState ReactAction (CharacterFSM.UserEventAction action)
	{
		return this;
	}
}

