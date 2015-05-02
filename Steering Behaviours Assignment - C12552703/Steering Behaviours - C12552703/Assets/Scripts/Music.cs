using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public GameObject jaws;//This is storing the gameObect called jaws.

	// Use this for initialization
	void Start () {
		jaws.GetComponent<AudioSource>().Play();//This is playing the audio called jaws.
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
