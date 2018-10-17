using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public EnemyAIController enemyPrefab;
	[SerializeField] List <Transform> enemyPositions;
	[SerializeField] int numberOfEnemy;
	[SerializeField] CameraMovement agent;

    List <EnemyAIController> enemiesListRef = new List<EnemyAIController>();

	void Start () 
	{
        //start moving the agent in path
		agent.Init ();

        //instantiate the enemies
		for (int i = 0; i < numberOfEnemy; i++) 
		{
			EnemyAIController _enemy = Instantiate (enemyPrefab, transform);
			_enemy.Init (enemyPositions,"Enemy "+ i);
			_enemy.onPositionDataSender.AddListener (GetEnemyData);

            enemiesListRef.Add(_enemy);
		}

        StartCoroutine(MakeDecision());

	}
		
    //Get the enemies data  by dynamic event
	public void GetEnemyData(Transform enemyPosition,float enemySpeed, string enemyName)
	{
		//Debug.Log (" enemy name : "+ name + " pos : " + position + " Speed: " + speed);
        Debug.Log("Enemy name : " + enemyName + 
            "======= is in sight of view: "
            + agent.isInSight(enemyPosition)
            + "======== Distance from agent: " + Vector3.Distance(enemyPosition.position, agent.GetAgentPosition().position));
	
	}


    //this function is observer! the function get all data to make decisions
    // make decision once in a second - the time can be change in inspector
    IEnumerator MakeDecision()
    {
        yield return new WaitForEndOfFrame();

        for (; ; )
        {
            for (int i = 0; i < enemiesListRef.Count; i++)
            {
                Debug.Log("Enemy name : " + enemiesListRef[i].gameObject.name +
            "======= is in sight of view: "
            + agent.isInSight(enemiesListRef[i].transform)
            + "======== Distance from agent: " + Vector3.Distance(enemiesListRef[i].transform.position, agent.GetAgentPosition().position));

                //Example to decision-- if to shot or not
                if (Vector3.Distance(enemiesListRef[i].transform.position, agent.GetAgentPosition().position) < 25
                    && agent.isInSight(enemiesListRef[i].transform))
                {
                    Debug.Log("-----------SHOOOT toward ENEMY - " + enemiesListRef[i].gameObject.name);
                    // Shoot function
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }



}
