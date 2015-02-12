using UnityEngine;
using System.Collections;

public class HealthIndicator : MonoBehaviour {
	
//	public GameObject Bar;
	// Use this for initialization
	void Start () {
//		Debug.Log("Health start on " + gameObject.GetInstanceID());
		//Bar = (GameObject)gameObject.transform.FindChild("Health").gameObject;
		//gameObject.transform.FindChild("Health").gameObject.SetActiveRecursively(false);
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("Cur health: " + gameObject.GetComponent<CharacterFSM>().Health);
		//Bar.transform.localScale = new Vector3(1f,gameObject.GetComponent<CharacterFSM>().Health,1f);
		Label.OnRenderer(transform.FindChild("Body").renderer).Write("Stress: "+(100-(gameObject.GetComponent<CharacterFSM>().Health*100)).ToString ("0")+"%");
	}
}
