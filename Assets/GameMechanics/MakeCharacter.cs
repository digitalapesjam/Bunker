using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MakeCharacter : MonoBehaviour {
	
//	public GameObject bar;
	public GameObject character;
	public List<GameObject> characters = new List<GameObject>();
	
	public List<GameObject> Characters {
		get { return characters; }
	}
	
	public GameObject SelectedCharacter {
		get {
			foreach(GameObject go in Characters) {
				if(go.GetComponent<CharacterFSM>().CurrentState is WaitPlayerTarget) {
					return go;
				}
			}
			return null;
		}
	}
	
	public int Number = 3;
	public int NearDistance = 200;
	int minX = 0;
	int maxX = 500;
	int minZ = 800;
	int maxZ = 950;

	int offsetX = 700;
	int offsetZ = 0;

	//int numPics = 3;
	
	bool TooNear(Vector3 v) {
		return NearCharachters(v,30f).Count > 2;
	}
	
	// Use this for initialization
	void Start ()
	{
		System.Random random = new System.Random ();
		
		for (int i = 0; i < Number; i++) {
			int x,y,z;
			y = 20;
			
			do {
				x = Random.Range (minX, maxX) + offsetX;
				z = Random.Range (minZ, maxZ) + offsetZ;
			} while( TooNear(new Vector3(x,y,z) ) );
			
			var q = new Quaternion ();
			q.SetLookRotation (new Vector3 (0, 0, 1));
			GameObject character = (GameObject)Instantiate (this.character, new Vector3 (x, y, z), q);
			characters.Add (character);
			//CharacterOnClick ocb;
			//ocb = character.AddComponent ("CharacterOnClick") as CharacterOnClick;
			//ocb.idx = i;
			//character.AddComponent ("CharacterBehaviour");
			character.AddComponent ("CharacterOnClick");
			character.AddComponent ("CharacterAudioAnimation");
			character.AddComponent ("CharacterFSM");
			character.AddComponent ("HealthIndicator");
			
			int l = System.Enum.GetValues (typeof(CharacterFSM.CharacterType)).Length;
			character.GetComponent<CharacterFSM> ().Type = (CharacterFSM.CharacterType)random.Next (l);
			
			//how to change the textures??
			string txtName = "Animations/" + character.GetComponent<CharacterFSM>().Type.ToString() + "Idle";
			//Debug.Log("Loading char. texture: " + txtName);
			var body = character.transform.Find ("Body");
			body.renderer.material.mainTexture = Resources.Load (txtName) as Texture2D;//(character.GetComponent<CharacterFSM> ().Type.ToString() + "IdleUp") as Texture2D;
			//based on h,t,l select the character features
		}
	}
	
	Texture2D loadTexture(string part, int p) {
		return Resources.Load(part + p) as Texture2D;
	}
	int pick(int max) {
		return (int)Random.Range(0,max);
	}
	
	// Update is called once per frame
	
	//int ctr = 0;
	//int iter = 200;
	void Update () {
	/*	if(ctr==iter) {
			ctr = 0;
			
			var chars = SelectCharacters( x => x.transform.position.z > minZ );
			foreach(var ch in chars) {
				var pos = ch.transform.position;
				pos.z = maxZ;
				ch.transform.position = pos;
			}
			chars = SelectCharacters( x => x.transform.position.z < maxZ );
			foreach(var ch in chars) {
				var pos = ch.transform.position;
				pos.z = maxZ;
				ch.transform.position = pos;
			}
		} else {
			ctr++;
		}*/
		
	}
	
	public List<GameObject> SelectCharacters(System.Predicate<GameObject> pred) {
		return characters.FindAll(pred);
	}
	public List<GameObject> NearCharachters(Vector3 refPt, float dst) {
		System.Predicate<GameObject> isNear = 
			x => 
				(x.transform.position - refPt).magnitude < dst;
		return characters.FindAll(isNear);
	}
	
	public List<GameObject> NearCharachters(GameObject refObj) {
		System.Predicate<GameObject> isNear = 
			x => 
				(x.transform.position - refObj.transform.position).magnitude < NearDistance &&
				x.GetInstanceID() != refObj.GetInstanceID();
		return characters.FindAll(isNear);
	}
	
}
