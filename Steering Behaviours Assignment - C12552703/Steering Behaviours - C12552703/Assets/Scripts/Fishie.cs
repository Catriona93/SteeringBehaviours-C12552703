using UnityEngine;
using System.Collections;

public class Fishie : MonoBehaviour {

	public Vector3 force; //This is setting up the Vector3s force. This can be applied to any direction.
	public Vector3 velocity; //This is setting up the Vector3s velocity. This is the direction of the vector 3.
	public float mass = 1f; //This is setting the weight of our agent to 1f.
	public float maxSpeed = 5f; //This is setting up the maxSpeed of the fish. This is how fast they can travel.
	public GameObject target; //This is setting up a GameObject called target. This is giving the fish something to steer towards.
	public bool seekEnabled, fleeEnabled, pursueEnabled, arriveEnabled, pathFollowEnabled, offsetPursuitEnabled; //You can toggle which steering behaviour you want on in the inspector.
	public Vector3 offsetPursuitOffset;//This is setting up the Vector 3 offsetPursuitOffset.


	Path path = new Path();//This is creating a new path for out player to follow. It is calling the Path script.
	
	void Start()
	{
		path.CreatePath(); //This is accessing the path instance and call the CreatePath method.
		offsetPursuitOffset = transform.position; //This is settin the offsetPursuitOffset vector to the starting position of each follower.
	}
	
	// Update is called once per frame
	void Update () 

	{//This is making sure that the fish will not go off the screen.
		//It is checking the position of the camera and the viewport if the camera.
		//If the fish get to the side or top of the view of the camera they will be clampled and not go off the camera view.
		Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
		viewPos.x = Mathf.Clamp01(viewPos.x);//If the fish is at the x position of the view they will be clamped.
		viewPos.y = Mathf.Clamp01(viewPos.y);//If the fish is at the y position of the view they will be clamped.
		transform.position = Camera.main.ViewportToWorldPoint(viewPos);
		ForceIntegrator();//This is calling the ForceIntegrator function.
	}
	
	void ForceIntegrator()//This is setting up the ForceIntegrator function.
	{
		Vector3 accel = force / mass; //This is setting up our acceleration.The accell is the accumulated force / our mass. 
		velocity = velocity + accel * Time.deltaTime; //This is adding the accel to our velocity over time.
		transform.position = transform.position + velocity * Time.deltaTime; //This is setting our position to our position snd the the velocity over time.
		force = Vector3.zero; //each frame, we find the difference between self and target, and add the different to the forceAcc. We don't want this adding up to itself, so we reset it each frame and only add force once each frame to the required direction
		if(velocity.magnitude > float.Epsilon) //if we have speed
		{
			transform.forward = Vector3.Normalize(velocity); //set our forward direction to use our velocity (difference between us and the target) so to always point at the target
		}
		velocity *= 0.99f; //each frame is set velocity to 0.99 of itself, so it can slow down.
		if(seekEnabled)
		{
			force += Seek(target.transform.position); //Seek is a functon that returns a Vector3, and it also pass down our targets position
		}
		if(fleeEnabled) //If flee is enabled 
		{
			force += Flee (target.transform.position);//Flee from target.
		}
		if(pursueEnabled)//If pursue is enabled
		{
			force += Pursue(target); //Pursue is a function that returns a Vector3, and we also pass down our target gameobject
		}
		if(arriveEnabled)//if arrive is enabled we habe arrived at the target.
		{
			force += Arrive (target.transform.position);
		}
		if(pathFollowEnabled)//if pathFollow is enabled 
		{
			force += PathFollow (); //Call the PathFollow function.
		}
		if(offsetPursuitEnabled) //if the offsetPursuitEnabled is enabled
		{
			force += OffsetPursuit (offsetPursuitOffset); //return the offsetpursuit force, passing in the offset.
		}
	}
	
	Vector3 Seek(Vector3 target) //This is setting up the Seek behaviour. It returns a Vector3 to the force, which is where we steer this agent towards
	{
		Vector3 desiredVel = target - transform.position; //This is finding the desired Velocity - the difference between the target and ourselves. This is the vector we need to add force to, which will steer us towards our target
		desiredVel.Normalize(); //This is normalize the vector, so that it keeps it's direction but is of max length 1, so we have control over it's speed (To normalize, divide each x, y, z of the vector by it's own magnitude. 
		//Magnitude is calculated by adding the squared values of x,y,z together, then getting the squared root of the result.
		desiredVel *= maxSpeed; //This is multiplying the normalized vector by maxSpeed to make it move faster
		return desiredVel - velocity; //This is returning the difference of the desired velocity and velocity so that we are steered in the direction of the targets direction
	}
	
	Vector3 Flee(Vector3 target) //This is setting up the Flee behaviour. This is creating a Vector3 to steer the agent in the opposite direction.
	{
		Vector3 desiredVel = target - transform.position; //This is finding the desired Velocity - the difference between the target and ourselves. This is the vector we need to add force to, which will steer us towards our target.
		float distance = desiredVel.magnitude; //This is creating a distance float, which tracks the distance between agent and target using the magnitude of the vector (distance)
		if(distance < 5f) //if the distance between the two is less than 5 units, Flee!
		{
			Debug.Log("Fleeee");

			desiredVel.Normalize();//This is normalzing the desired velocity.
			desiredVel *= 800f;//This is multipying the desired velocity by the speed of 800f.
			return velocity - desiredVel; //This creating our opposing force here.The vectors are flipped compared to Seek.
		}
		else //if our agent and target are greater than 5 units apart
		{

			return Vector3.zero; //return a 0,0,0 vector.
		}
	}
	
	Vector3 Pursue(GameObject target) //Pursue calculates a future interception position of a target. It is used in conjunction with Seek!
	{
		Vector3 desiredVel = target.transform.position - transform.position;//This is finding the desired Velocity - the difference between the target and ourselves. This is the vector we need to add force to, which will steer us towards our target.
		float distance = desiredVel.magnitude; //This is finding the distance between agent and target.
		float lookAhead = distance / maxSpeed; //This is adding a bit of distance onto the position we track. It is dividing the distance by maxSpeed ensuring it always scales as they change
		Vector3 desPos = target.transform.position+(lookAhead * target.GetComponent<Fishie>().velocity); //This is telling our agent to Seek the targets position with the added lookAhead value, multiplied by the targets velocity, so that the look ahead can always be calculated in the correct direction
		return Seek (desPos); //This is return our vector to Seek, which then runs the normal Seek code.
	}
	
	Vector3 Arrive(Vector3 targetPos) //This is setting up the Arrive behaviour. This is creating a Vector3 to steer the agent towards targetPos.
	{
		Vector3 toTarget = targetPos - transform.position;;//This is finding the desired Velocity - the difference between the targePos.
		float distance = toTarget.magnitude;//This is finding the distance between agent and target.
		if(distance <= 1f)//If the distance is less than 1f
		{
			return Vector3.zero;//return vector3.zero.//stop
		}
		float slowingDistance = 8.0f; //This is setting up the radius from the target and seeing do we want to start slowing down
		float decelerateTweaker = maxSpeed / 10f; //This is checking how fast or slow we want to decelerate
		float rampedSpeed = maxSpeed * (distance / slowingDistance * decelerateTweaker); //ramped speed scales based on the distance to our target.
		float newSpeed = Mathf.Min (rampedSpeed, maxSpeed); //This returns the smaller of the two speeds.
		Vector3 desiredVel = newSpeed * toTarget.normalized; //use the newSpeed * by the normalized toTarget vector
		return desiredVel - velocity; //return the difference between desiredVel and our velocity and apply a force to it
	}
	
	Vector3 PathFollow()//This is creating a Vector3 PathFollow function.
	{
		float distance = (transform.position - path.NextWaypoint()).magnitude;//This is setting the distance to transform position - path.nextwaypoint.magnitude.
		if(distance < 0.5f)//If the distance is less than 0.5f
		{
			path.AdvanceWaypoint();//Go to next waypoint on the path.
		}
		if(!path.looped && path.IsLastCheckpoint())//If at the last check waypoint
		{
			return Arrive (path.NextWaypoint());//return arrive. go the next wayoint in the path.
		}
		else//else
		{
			return Seek (path.NextWaypoint());//return seek. Go to next waypoint in the path.
		}
	}
	
	Vector3 OffsetPursuit(Vector3 offset)//This is setting up a vector3 OffsetPurset(Vector3 offset function.
	{
		Vector3 desiredVel = Vector3.zero;//Vector3s desired velocity = Vector3.zero.
		desiredVel = target.transform.TransformPoint(offset);//This is setting the desiredVel = target.transform.Transofmr(offset).
		float distance = (desiredVel - transform.position).magnitude;//Distance is desired velocity - transform.position.magnitude.
		float lookAhead = distance / maxSpeed; //the lookAhead is how much we should look in front of our target.
												//divide the distance by maxSpeed to ensure we are looking ahead a relative amount to our distance from our target
		desiredVel = desiredVel +(lookAhead * target.GetComponent<Fishie>().velocity);//This is setting the desired velocity to desired velocity plus lookAhead multyply the targets velocity. This is getting the targets velocity form the Fishie script component.
		return Arrive (desiredVel);//This is returning the arrive desired velocity.
		
	}
}