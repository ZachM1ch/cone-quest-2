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
    [SerializeField] private float currentMoveSpeed = 2.0f;
    [SerializeField] private float patrolMoveSpeed = 2.0f;
    [SerializeField] private float aggroMoveSpeed = 4.0f;
    [SerializeField] private bool ignoreYAxis = true;

    [SerializeField] private float aggroLoseTime = 2.0f;
    [SerializeField] public GameObject exclamation;
    [SerializeField] public SpriteBillboard spriteBillboard;

    private Animator spriteAnimator;

    private Vector3 followPoint;
    private bool followPatrolPoints = true;

    // --- METHODS ---
    private void Start()
    {
        spriteAnimator = GetComponentInChildren<Animator>();
        int childCount = patrolPointsParentObj.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            patrolPoints.Add(patrolPointsParentObj.transform.GetChild(i).transform);
        }
    }

    private void Update()
    {
        if (followPatrolPoints)
        {

            if (patrolPoints.Count <= 1)
            {
                return;
            }

            // Check if enemy has reached next patrol point
            float distToNextPoint = Vector3.Distance(transform.position, patrolPoints[currPatrolPoint].position);

            if (distToNextPoint <= pointCollisionDist)
            {
                // We have reached next point, so move on to the next point.
                currPatrolPoint += 1;

                if (currPatrolPoint >= patrolPoints.Count)
                {
                    currPatrolPoint = 0;
                }
            }

            // Move towards currPatrolPoint
            Vector3 pointDirection = patrolPoints[currPatrolPoint].position - transform.position;

            // Ignore moving enemy on the Y-Axis
            if (ignoreYAxis)
            {
                pointDirection.y = 0.0f;
            }

            pointDirection = pointDirection.normalized;

            transform.Translate(pointDirection * currentMoveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 pointDirection = (followPoint - transform.position).normalized;
            
            transform.Translate(pointDirection * currentMoveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Blob")
        {
            spriteBillboard.enabled = false;
            spriteAnimator.Play("Death");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // :)
        if(followPatrolPoints && collision.gameObject.tag == "Player")
            exclamation.GetComponent<Animator>().Play("Bounce");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (followPatrolPoints)
            {
                followPatrolPoints = false;
                currentMoveSpeed = aggroMoveSpeed;
            }
            followPoint = other.gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke(nameof(LoseAggro), aggroLoseTime);
        }
    }

    private void LoseAggro()
    {
        followPatrolPoints = true;
        currentMoveSpeed = patrolMoveSpeed;
    }

}
