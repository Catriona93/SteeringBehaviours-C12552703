using UnityEngine;
using System.Collections;

public class eatFood : MonoBehaviour {

	public void OnTriggerEnter(Collider collider){
		if(collider.gameObject.name == "_Crab") //If the food collides with the crab collider destroy food.
			
		{
			
			
			
			
			Destroy(gameObject);//Destroy the enemy game object.
		}
		
		
	}
}