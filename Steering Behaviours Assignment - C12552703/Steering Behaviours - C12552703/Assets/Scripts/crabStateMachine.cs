using UnityEngine;
using System.Collections;

public class crabStateMachine : MonoBehaviour {

	Transform crab,food;//This is setting up the Transform for crab and food.
	float searchTime, searchTimer = 4f;//This is setting up the float classes for searchTime, searchTimer to 4f;
	float randLookTime, randLookTimer = 0.3f;//This is setting up the float classes for randLookTime, randLookTimer to 0.3f;
	
	Ray ray; //This is setting the raycast called ray.
	RaycastHit target;//This is setting up the raycastHit called target.

	NavMeshAgent myNavMeshAgent; //This is adding the NavMeshagent and calling it myNavMechAgent.
	
	public float speed = 20; //This is setting the speed to 20.


	
	enum States //This is setting up our states called Initialize, Sleep, Attack and Chase.
	{
		Initialize,
		Sleep,
		Attack,
		Chase,
	}
	
	States currentState = States.Initialize; //This is creating an instance of the enum and setting it's default to Initialize
	


	void Start()
	{
		
		myNavMeshAgent = transform.GetComponent<NavMeshAgent>();//This is adding the navMeshAgent component.
	
	}
	
	
	
	void Update () 
	{
		switch(currentState) //This is passing in the current state
		{
		case States.Initialize: 
			Initilize(); //This is calling the Initilize function.
			break;
		case States.Sleep:
			Sleep (); //This is calling the Sleep function.
			break;
		case States.Attack:
			Attack(); //This is calling the Attack function.
			break;
		case States.Chase:
			Chase (); //This is calling the chase function.
			break;
		}
		
		//This is drawing the ray at the crabs position. The size of the ray is 10f and the color is magenta.
		Debug.DrawRay(crab.position, crab.transform.forward * 10f, Color.magenta);
		
	}
	
	void Initilize()
	{
		crab = GameObject.Find ("_Crab").transform;//This is finding the crab gameobjects transform.
		food = GameObject.FindWithTag("food").transform;//This is finding the object with the tag food transform.
		
		currentState = States.Sleep;//The current state is sleep.
	}
	
	void Sleep()
	{
		searchTime = 0; //The search time is set to 0.
		if(DoRayCast())
		{
			if(target.transform.tag == "food")//if the raycast hits the target tagged as food the state will switch to Attack.
			{
				currentState = States.Attack;//The current state is Attack.
			}
		}
	}	
	
	void Attack()
	{
		Vector3 lookAtPlayer = new Vector3( food.position.x, this.transform.position.y, food.position.z ) ; //This is creating a vector to find the food x and z position
		crab.LookAt(lookAtPlayer); //lookat takes a vector3 position and converts it to the angle it needs to look at supplied position

		if(Vector3.Distance(crab.position, food.position) > 10f) //If the distance between the crab and food position is greater than 10f the state will switch to chasing.
		{
			currentState = States.Chase;//The current state is chasing.
		}
	}
	
	
	void Chase()
	{
		myNavMeshAgent.destination = food.position;//chase the food.
		food = GameObject.FindWithTag("food").transform;
		if(myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance && myNavMeshAgent.remainingDistance != 0)
		{

		}
	}
	
	
	
	bool DoRayCast() //bool return type, returns true if ray hits a collider.
	{
		ray = new Ray(crab.position, crab.transform.forward); //a new ray, which takes a start position and direction
		bool rayHit = Physics.Raycast(ray, out target, 10f); //raycast returns true or false
		return rayHit; //returns rayHit if true or false
	}
	
}