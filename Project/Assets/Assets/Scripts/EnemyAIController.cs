using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

//dynamic event that pass data
[System.Serializable] public class PositionDataSender : UnityEvent<Transform,float,string> { }

public class EnemyAIController : MonoBehaviour {

	NavMeshAgent _navMeshAgent;
	Transform targetPoint;

	List <Transform> _targets = new List<Transform> ();
	float distanceTrigger  = 10f;
	public PositionDataSender onPositionDataSender;


	public void Init(List <Transform> targets , string name)
	{
		this.gameObject.name = name;
		_targets = targets;
		_navMeshAgent = this.GetComponent<NavMeshAgent> ();
		targetPoint = _targets[Random.Range(0,_targets.Count)];

		SetNewDestination (targetPoint);
		StartCoroutine(SendData());
	
	}


	public void SetNewDestination(Transform newPos)
	{
		if (_navMeshAgent != null)
			_navMeshAgent.SetDestination ( newPos.position);
	}

	void Update () 
	{
        //Check if the enemy has reached the nearest target ,if true i give him a new destination
		if (Vector3.Distance (transform.position, targetPoint.position) < distanceTrigger) 
		{
			targetPoint = _targets[Random.Range(0,_targets.Count)];
			SetNewDestination (targetPoint);
		}
	}

    // Send data once in a second
	IEnumerator SendData() {
		
		for(;;) 
		{
			if (onPositionDataSender != null) 
				onPositionDataSender.Invoke (transform, _navMeshAgent.speed,this.gameObject.name);
			yield return new WaitForSeconds(1f);
		}
	}
}
