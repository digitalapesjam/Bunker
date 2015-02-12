using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateIcons : MonoBehaviour {
	
	/*class Pair {
		public GameObject a;
		public GameObject b;
		public Pair(GameObject a, GameObject b) {
			this.a = a;
			this.b = b;
		}
	}*/
	
	public GameObject IconTemplate;
	public GameObject IconAnimTemplate;
	
	MakeCharacter mMakeCharacter;
	Dictionary<GameObject,GameObject> icons = new Dictionary<GameObject, GameObject>();
	
	// Use this for initialization
	void Start () {
		mMakeCharacter = gameObject.GetComponent<MakeCharacter>();
	}
	
	public void OnFSMStateChanged( GameObject character, CharacterState newState ) {
		RemoveIconFor(character);
		Vector3 pos = character.transform.position;
		pos.y += 50f;
		pos.z += 40f;
		if(newState is Hate) {
			putIcon(character,GetIcon("anger_anim",pos));
		} else if(newState is Love) {
			putIcon(character,GetIcon("love_anim",pos));
		} else if(newState is Talk) {
			putIcon(character,GetIcon("talk_anim",pos));
		} else if(newState is Dead) {
			pos.y -= 10; pos.z -= 5;
			putIcon(character,GetStaticIcon("dead",pos));
		} else if(newState is WaitPlayerTarget) {
			Debug.Log("put selected icon");
			putIcon(character,GetStaticIcon("selected",pos));
		} else if(newState is MoveToTarget) {
			switch(((MoveToTarget)newState).Intent) {
			case CharacterFSM.Intention.Hate:
				putIcon(character,GetIcon("anger_anim",pos));
				break;
			case CharacterFSM.Intention.Love:
				putIcon(character,GetIcon("love_anim",pos));
				break;
			}
		}
	}
	
	void RemoveIconFor(GameObject character) {
		if(icons.ContainsKey(character)){
			GameObject icon = icons[character];
			icons.Remove(character);
			GameObject.DestroyObject(icon);
		}
	}
	void putIcon(GameObject character,GameObject icon) {
		icons[character] = icon;
	}
	
	GameObject GetIcon( string name, Vector3 iconPos ) {
		GameObject ret = (GameObject)GameObject.Instantiate(IconAnimTemplate,iconPos,Quaternion.identity);
		ret.transform.Find("IconPlane")
					.renderer.material.mainTexture = 
						Resources.Load(name) as Texture2D;
		return ret;
	}
	GameObject GetStaticIcon( string name, Vector3 iconPos ) {
		GameObject ret = (GameObject)GameObject.Instantiate(IconTemplate,iconPos,Quaternion.identity);
		ret.transform.Find("IconPlane")
					.renderer.material.mainTexture = 
						Resources.Load(name) as Texture2D;
		return ret;
	}
	
	// Update is called once per frame
	void Update () {
		foreach( GameObject key in icons.Keys ) {
			Vector3 kp = key.transform.position;
			Vector3 vp = icons[key].transform.position;
			vp.x = kp.x + 20f; vp.z = kp.z - 10f;
			icons[key].transform.position = vp;
		} 

	}
}
