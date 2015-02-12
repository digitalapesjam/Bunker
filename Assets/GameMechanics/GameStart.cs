using UnityEngine;
using System.Collections;

[AddComponentMenu("Game Mechanics/Game Start")]
public class GameStart : MonoBehaviour {
	System.Random rand = new System.Random();
	
	int X = 800;
//	public int Z = 800;
	int Width = 1100;
//	public int Depth = 950;
	
	public Vector3 RandomPointInPlane(float y) {
		return new Vector3( r(X,Width),y, z(150,800));//r(Z,Depth) );
	}
	
	int r(int min,int max) {
		int ret = (int)(min + (rand.Next() * (max-min)));
		
		if(ret > max) { ret = max; }
		if(ret < min) { ret = min; }
		return ret;
		//return Random.Range(min,max);
	}
	int z(int delta, int offset) {
		return offset + (Random.Range(0,1) * delta);
	}
	
	void Start ()
	{
		//Physics.gravity = new Vector3 (0, -981, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
