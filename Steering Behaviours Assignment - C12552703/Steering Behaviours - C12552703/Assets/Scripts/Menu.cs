using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public GUIStyle myGuistyle;//This is creating a custom gui style called myGuistyle.
	public GUIStyle myGuiStyle3;//This is creating a custom gui style called myGuistyle3.
	public AudioClip menuMusic;//This is storing an audio clip called menuMusic.
	
	
	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().PlayOneShot(menuMusic, 3f); //On start play audio called "menuMusic" with volume 3f.
	}
	
	
	void OnGUI()
	{
		//This is placing the gui to 380 x pos, 120 on the y pos, 200 width and 30 height. The text says "Fishies". you can use the custom gui in the inspector.
		GUI.Box(new Rect(380,120, 200, 30), "fishies", myGuistyle);
		
		
		
		//This is placing the gui to 415 on the x pos, 445 on the y pos, 200 width and 30 height. The text says "Press Space to Stsrt". you can use the custom gui in the inspector.
		GUI.Box(new Rect(415,445, 200, 30), "Press  (Space)  To Start ", myGuiStyle3);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Input.GetKey(KeyCode.Space)) //When pressing the Space key, it will bring you to the play screen.
			//Load the level called "Level 1".
			Application.LoadLevel("Level 1");
	}
	
}
