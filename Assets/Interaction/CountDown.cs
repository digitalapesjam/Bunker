using UnityEngine;
using System.Collections;

[AddComponentMenu("Miscellaneous/Countdown")]
public class CountDown : MonoBehaviour {
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<AudioSource>().volume = GetComponent<AudioSource>().time/GetComponent<AudioSource>().audio.clip.length;
		if (GetComponent<AudioSource>().audio.clip.length <= GetComponent<AudioSource>().time)
			GetComponent<EndGame>().TimeOver();
	}
	
	void OnGUI ()
	{
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		GUI.skin.box.normal.textColor = Color.yellow;
		GUI.skin.box.fontSize = Screen.width/100;
		GUI.Box(new Rect (Screen.width/2.15f, Screen.height/3.15f, Screen.width/25, Screen.width / 25),"-"+(GetComponent<AudioSource>().audio.clip.length-GetComponent<AudioSource>().time).ToString("0"));
	}
}
