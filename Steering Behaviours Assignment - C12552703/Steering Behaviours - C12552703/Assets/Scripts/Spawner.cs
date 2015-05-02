using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float minTime = 5f; //This is creating a float for minTime and setting it to 5f.
	public float maxTime = 10f; //This is creating a float for maxTime and setting it to 10f.
	public float minX = -5.5f; //This is creating a float for minX and setting it to -5.5f.
	public float maxX = 5.5f; //This is creating a float for maxX and setting it to 5.5f.
	public float topY = 5.5f; //This is creating a float for topY and setting it to 5.5f.
	public float z = 0.0f; //This is creating a float for z and setting it to 0.0f.
	public int foodCount = 20; //This is creating an int for foodCount and setting it to 20.
	public GameObject prefab; //This is creating an empty GameObject called prefab. You can add the gameObject in the inspector.
	private bool rotation = false;//This is creating a boolean called rotation and setting it to false. The gameObject will not rotate.
	
	public bool doSpawn = true;//This is creating a boolean called doSpawn and setting it to true. The gameObject will spawn.
	
	void Start() {

		StartCoroutine(Spawner1()); //This is calling the startCoroutine(Spawner1) function.
		//The spawning will begin when the screen starts. 
		
		}
	
	IEnumerator Spawner1() { //The spawner will spawn the food randomly in the range of minX, maxX, topY and z.
		while (doSpawn && foodCount > 0) {
			Vector3 v = new Vector3(Random.Range (minX, maxX), topY, z);
			Instantiate(prefab, v, Random.rotation); //This instantiates the gameobject called prefab and it will move in a random directiion when spawned.
			foodCount--;//This is taking away from the food count everytime one is spawned. Only 20 will spawn.
			
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));
			//This is will spawn the gameObject in a random time between minTime and maxTime.
		}
		
		while (doSpawn && foodCount < 0) {
		
			
		}
	}
}

