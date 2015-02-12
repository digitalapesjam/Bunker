using UnityEngine;
using System.Collections;

[AddComponentMenu("Rendering/Flickering Light")]
public class FlickeringLight : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Random.value < 0.1)
			gameObject.GetComponent<Light> ().intensity = Random.value*8;
	}
}
