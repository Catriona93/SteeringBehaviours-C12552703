using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GUIStyle customGuiStyle;//This is creating a custom Gui.
	public GUIStyle Style;//This is creating a custom Gui.
	public GUIStyle Style2;//This is creating a custom Gui.
	private float timer = 180f;//This is adding 180seconds to the timer.

	public AudioClip oceanSound;//This is storing an audio clip called menuMusic.
	void Start () {
		GetComponent<AudioSource>().PlayOneShot(oceanSound, 4f); //On start play audio called "oceanSound" with volume 4f.
	}

	void Update () {
		timer -= Time.deltaTime;
		StartCoroutine(LoadAfterDelay(180f));//Reset level after 3 minutes.
		
		if(Input.GetKey(KeyCode.R)) //When pressing the R key, it will reset the level.
			//Load the level that is currently loaded.


			Application.LoadLevel(Application.loadedLevel);
	}

	IEnumerator LoadAfterDelay(float waitTime){
		yield return new WaitForSeconds(waitTime); // wait 3 minutes
		Application.LoadLevel(Application.loadedLevel);
	}

	void OnGUI() {
		
		int minutes = Mathf.FloorToInt(timer / 60F);
		int seconds = Mathf.FloorToInt(timer - minutes * 60);
		
		string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);//This is setting up the format of how the time will be shown.
		
	
		GUI.Label(new Rect(905,30,250,100), niceTime, customGuiStyle);////This is creating a new gui label at the postion of 905 on the x pos, 30 on the y pos,250 width and 100 height. The text will show the time.
		GUI.Label(new Rect(775,30,250,100), "Resets in : ", Style);//This is creating a new gui label at the postion of 775 on the x pos, 30 on the y pos,250 width and 100 height. The text says Resets in : .
		GUI.Label(new Rect(90,30,250,100), "Press (R) to Reset", Style2);
		//This is creating a new gui label at the postion of 90 on the x pos, 30 on the y pos,250 width and 100 height. The text says Press r to reset.
		}
	}

