using UnityEngine;
using System.Collections;

public class Label : MonoBehaviour
{
	//private GUIStyle guiStyle; 
    private string text = "";
	public string Text{
		get {return text;}
	}
    private Vector3 screenPosition;

	private Label(){
	}
	
    // Use this for initialization
    void Start()
    {
//		Texture2D background = new Texture2D(200, 200);
//		Color[] cs = new Color[200*200];
//		for (int i=0;i<cs.Length;i++)
//			cs[i] = Color.white;
//		background.SetPixels(0,0,200,200,cs);
//        guiStyle = new GUIStyle();
//        guiStyle.alignment = TextAnchor.MiddleCenter;
//        guiStyle.normal.background = background;
//		guiStyle.hover.background = background;
//		guiStyle.active.background = background;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (text.Length > 0)
        {
            int length = 7 * (text.Length + name.Length);
            if (gameObject.GetComponent<Camera>() != null)
            {
                GUI.Box(new Rect(0, Screen.height-20, Screen.width, 20), text);
            }
            else
            {
                screenPosition = Camera.mainCamera.WorldToScreenPoint(gameObject.transform.position);
                GUI.Box(new Rect(screenPosition.x - length / 2, Camera.current.GetScreenHeight() - screenPosition.y - 10, length*1.2f, 20), text);
            }
        }
    }
	
	public static Label OnScreen {
		get {
			Camera cam = Camera.main;
			if (cam == null)
				cam = Camera.current;
			if (cam == null)
				cam = Camera.mainCamera;
			
			if (cam.gameObject.GetComponent<Label>() == null)
				cam.gameObject.AddComponent<Label>();
			return cam.gameObject.GetComponent<Label>();
		}
	}
	
	public static Label OnRenderer(Renderer r){
		if (r.gameObject.GetComponent<Label>() == null)
			r.gameObject.AddComponent<Label>();
		return r.gameObject.GetComponent<Label>();
	}
	
    public void Log(string s)
    {
        text += " "+ s;
    }

    public void Write(string s)
    {
        text = s;
    }
}