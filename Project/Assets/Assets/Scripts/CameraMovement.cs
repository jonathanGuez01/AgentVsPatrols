using UnityEngine;
using System.Collections;
using DG.Tweening;


public class CameraMovement : MonoBehaviour {

	public float speed = 3f;
	public float rotationSpeed = 25f;
	public Transform pathParent;
	Transform targetPoint;
	int index;
	[SerializeField] LineOfSightChecker lineOfSightChecker;

    // For debug - drawing the path on scene
	void OnDrawGizmos()
	{
		Vector3 from;
		Vector3 to;
		for (int a=0; a<pathParent.childCount; a++)
		{
			from = pathParent.GetChild(a).position;
			to = pathParent.GetChild((a+1) % pathParent.childCount).position;
			Gizmos.color = new Color (1, 0, 0);
			Gizmos.DrawLine (from, to);
		}
	}

	public void Init()
	{
		index = 0;
		targetPoint = pathParent.GetChild (index);
		transform.LookAt (targetPoint);
	}
  
	public void SmoothLookAt(Transform _target)
	{
		transform.DOLookAt (targetPoint.position, 1f, AxisConstraint.Y);
	}

    public Transform GetAgentPosition()
    {
        return transform;
    }

    public float GetAgentSpeed()
    {
        return speed;
    }

	void Update ()
	{
		transform.position = Vector3.MoveTowards (transform.position, targetPoint.position, speed * Time.deltaTime);

		if (Vector3.Distance (transform.position, targetPoint.position) < 0.1f) 
		{
			index++;
			index %= pathParent.childCount;
			targetPoint = pathParent.GetChild (index);

			SmoothLookAt (targetPoint);
		}

	}

    // Check if there are enemies in line sight
	public bool isInSight(Transform enemyPos)
	{
		Debug.Log("===== line of sight : " + lineOfSightChecker.HaveLineOfSight (enemyPos) );
		return lineOfSightChecker.HaveLineOfSight (enemyPos);

	}

}