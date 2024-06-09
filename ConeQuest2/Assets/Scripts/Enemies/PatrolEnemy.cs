using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    // --- VARIABLES ---
    public GameObject patrolPointsParentObj;
    private List<Transform> patrolPoints = new List<Transform>();
    private int currPatrolPoint = 0;
    [SerializeField] private float pointCollisionDist = 0.5f;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private bool ignoreYAxis = true;

    // --- METHODS ---
    private void Start()
    {
        int childCount = patrolPointsParentObj.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            patrolPoints.Add(patrolPointsParentObj.transform.GetChild(i).transform);
        }
    }

    private void Update()
    {
        if(patrolPoints.Count <= 1)
        {
            Debug.LogWarning("Not enough patrol points! Pleased have a minimum of 2 patrol points.");
            return;
        }

        // Check if enemy has reached next patrol point
        float distToNextPoint = Vector3.Distance(transform.position, patrolPoints[currPatrolPoint].position);

        if(distToNextPoint <= pointCollisionDist)
        {
            // We have reached next point, so move on to the next point.
            currPatrolPoint += 1;
            
            if(currPatrolPoint >= patrolPoints.Count)
            {
                currPatrolPoint = 0;
            }
        }

        // Move towards currPatrolPoint
        Vector3 pointDirection = patrolPoints[currPatrolPoint].position - transform.position;

        // Ignore moving enemy on the Y-Axis
        if(ignoreYAxis)
        {
            pointDirection.y = 0.0f;
        }

        pointDirection = pointDirection.normalized;

        transform.Translate(pointDirection * moveSpeed * Time.deltaTime);
    }

}
