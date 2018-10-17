using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightChecker : MonoBehaviour {

	[SerializeField] float minAngle = 90f;
	[SerializeField] float maxAngle = 270f;

    //check if the enemy is in front the camera - we can configure the angle parametre in the inspector
	public bool InFront(Transform target)
	{
		Vector3 dirToTarget = transform.position - target.position;
		float angle = Vector3.Angle (transform.forward, dirToTarget);

		if (Mathf.Abs(angle)>minAngle && Mathf.Abs(angle)<maxAngle)
		{
			Debug.DrawLine (transform.position, target.position, Color.black);
			return true;
		}
		else
		{
			Debug.DrawLine (transform.position, target.position, Color.yellow);
			return false;
		}
	}

    //check whether there are objects that prevent me from direct range to the enemy
	public bool HaveLineOfSight(Transform target)
	{
		RaycastHit hit;
		Ray ray = new Ray (transform.position, Vector3.forward);
		Vector3 dir = target.position - transform.position;
		ray.direction = dir;

		Debug.DrawRay(transform.position,dir, Color.yellow);

		if (Physics.Raycast(ray,out hit))
		{
           //we need to check both Infront method too , to Make sure there is a direct line and also within sight
		   if (hit.collider.tag == "Enemy" && InFront (target))
			return true;

		}

		return false;
	
	}

}
