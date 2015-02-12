using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Side Scroll")]
public class SideScrollCamera : MonoBehaviour {
	
	public float SideLimits = 20;
	public float Speed = 1;
	public float ScreenTriggers = 0.02f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.mousePosition.x < Screen.width * ScreenTriggers && 
			camera.transform.position.x > GameObject.Find ("Wall").transform.position.x - GameObject.Find ("Wall").transform.localScale.x/2 + SideLimits) {
			camera.transform.position -= new Vector3 (Speed, 0, 0);
		}
		
		if (Input.mousePosition.x > Screen.width - (Screen.width * ScreenTriggers) && 
			camera.transform.position.x < GameObject.Find ("Wall").transform.position.x + GameObject.Find("Wall").transform.localScale.x/2 - SideLimits) {
			camera.transform.position += new Vector3 (Speed, 0, 0);
		}
	}
}
