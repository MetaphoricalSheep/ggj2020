using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterMonster : MonoBehaviour
{
    private GameObject player;
    private CustomCharacterController characterController;
    private Animator animator;
    private float roamRadius = 5f;
    private float fleeRadius = 8f;
    private float deathRadius = 5f;

    private float speed = 10f;

    EnemyStates state = EnemyStates.Idle;
    Vector3 roamBase;

    Vector3 targetPosition;

    private void Start()
    {
        float randomScale = Random.Range(0.5f, 1.5f);
        GetComponentInChildren<Transform>().localScale = new Vector3(randomScale, randomScale, randomScale);
        player = GameObject.FindWithTag("Player");
        characterController = player.GetComponent<CustomCharacterController>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(SetIdle());
        roamBase = transform.position;
        roamBase.y = 1.0f;
    }

    private void Update()
    {
        // Check death
        CheckDeath();

        // Check new state
        switch (state)
        {
            case EnemyStates.Idle:
                CheckFleeing();
                break;
            case EnemyStates.Moving:
                CheckFleeing();
                if (state == EnemyStates.Moving)
                {
                    MoveToTargetAndCheckArrival();
                }
                break;
            case EnemyStates.Fleeing:
                MoveToTargetAndCheckArrival();
                break;
        }
    }

    private void CheckDeath()
    {
        if (LightManager.GetClosestLightDistanceToPosition(transform.position) < deathRadius)
        {
            Destroy(gameObject);
        }
    }

    private void CheckFleeing()
    {
        if (characterController.HoldingFire())
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= fleeRadius)
            {
                state = EnemyStates.Fleeing;
                animator.SetBool("Moving", true);
                targetPosition = GetNewFleePosition();
                roamBase = targetPosition;
                roamBase.y = 1.0f;
            }
        }
    }

    private void MoveToTargetAndCheckArrival()
    {
        transform.position += (targetPosition - transform.position).normalized * speed * Time.deltaTime;
        if (ArrivedAtPosition(targetPosition))
        {
            StartCoroutine(SetIdle());
        }
    }

    IEnumerator SetIdle()
    {
        state = EnemyStates.Idle;
        animator.SetBool("Moving", false);

        yield return new WaitForSeconds(Random.Range(3f, 10f));
        if (state == EnemyStates.Idle)
        {
            targetPosition = GetNewRoamPosition();
            state = EnemyStates.Moving;
            animator.SetBool("Moving", true);
        }
    }
    
    enum EnemyStates
    {
        Idle,
        Moving,
        Fleeing
    }

    private bool ArrivedAtPosition(Vector3 position)
    {
        return Vector3.Distance(transform.position, position) < 0.2f;
    }

    private Vector3 GetNewRoamPosition()
    {
        Vector3 targetVector;

        for (int i = 0; i < 5; i++)
        {
            // Random location
            Vector2 ran = Random.insideUnitCircle;
            targetVector = new Vector3(ran.x, 0.0f, ran.y);

            // Random distance
            targetVector *= Random.Range(roamRadius/2f, roamRadius);

            // Move to roam base
            targetVector += roamBase;

            if (IsMovePositionValid(targetVector))
            {
                targetVector.y = 1.0f;
                return targetVector;
            }
        }

        return roamBase;
    }

    private Vector3 GetNewFleePosition()
    {
        Vector3 targetDirection = (transform.position - player.transform.position).normalized;
        Vector3 targetVector;

        for (int i = 0; i < 10; i++)
        {
            // Rotate
            float maxRotation = 10f + (i * 5f);
            Vector3 newTargetDirection = Quaternion.Euler(0, Random.Range(-maxRotation, maxRotation), 0) * targetDirection;

            // Scale
            newTargetDirection = newTargetDirection.normalized * Random.Range(3f, 6f);

            // Make vector
            targetVector = transform.position + newTargetDirection;
            if (IsMovePositionValid(targetVector))
            {
                targetVector.y = 1.0f;
                return targetVector;
            }
        }

        return transform.position + (targetDirection.normalized * Random.Range(3f, 6f));
    }

    private bool IsMovePositionValid(Vector3 targetPosition)
    {
        // Check the target vector for light
        if (LightManager.GetClosestLightDistanceToPosition(targetPosition) < fleeRadius)
        {
            return false;
        }

        // Check for something in between
        Vector3 raySource = new Vector3(transform.position.x, 1.0f, transform.position.z);
        Vector3 rayTarget = new Vector3(targetPosition.x, 1.0f, targetPosition.z);
        Vector3 rayVector = rayTarget - raySource;
        if (Physics.Raycast(raySource, rayVector.normalized, rayVector.magnitude))
        {
            return false;
        }

        // Check all the steps in between for light
        targetPosition.y = 0.0f;
        Vector3 moveVector = targetPosition - transform.position;
        Vector3 moveDirection = moveVector.normalized;
        float moveDistance = moveVector.magnitude;

        for (float checkDistance = 2.0f; checkDistance < moveDistance; checkDistance = checkDistance + 2.0f)
        {
            Vector3 newPosition = moveDirection * checkDistance;
            if (LightManager.GetClosestLightDistanceToPosition(newPosition) < deathRadius)
            {
                return false;
            }
        }
        return true;
    }
}
