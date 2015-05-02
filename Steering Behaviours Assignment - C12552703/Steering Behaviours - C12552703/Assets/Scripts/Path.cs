using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour {

	List <Vector3> waypoints = new List<Vector3>();//This is setting up a list for Vector3 waypoints.
	public int next = 0;//This is setting up the next int to 0.
	public bool looped = true;//This is setting up a bool called looped and setting it to true.
	
	public void CreatePath()//This is creating the CreatePath function.
	{
		for(int i = 0; i < 10; i++)//This is creating a for loop. int i = 0; i is less than 10; i++.
		{
			waypoints.Add (Random.insideUnitSphere * 2f);//Add waypoints in a random position inside a sphere multiplied by 2f.

		}
	}
	
	public Vector3 NextWaypoint()//Vector3 called NextWaypoints
	{
		return waypoints[next];//Return to the next waypoint.
	}
	
	public bool IsLastCheckpoint()//bool isLastCheckpoint
	{
		return(next == waypoints.Count-1);//go to the next waypoint.
	}
	
	public void AdvanceWaypoint()//function called AdvancedWaypoint
	{
		if(looped)//If looped is true
		{
			next = (next + 1) % waypoints.Count;//add a waypoint and go to the next one.
		}
		else//else
		{
			if(!IsLastCheckpoint())//If not isLastCheckpoint
			{
				next = next + 1;//go to next one.
			}
		}
	}
}

