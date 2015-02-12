using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void TimeOver ()
	{
		Application.LoadLevel ("Credits");	
	}
	
	public void CharacterDeath ()
	{
		Application.LoadLevel ("RestartScreen");
	}
}
