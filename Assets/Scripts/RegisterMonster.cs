using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterMonster : MonoBehaviour
{
    private static GameObject player;
    private static CustomCharacterController characterController;

    private float roamRadius = 5f;
    private float fleeRadius = 10f;
    private float deathRadius = 4f;

    private float speed = 10f;

    EnemyStates state = EnemyStates.Idle;
    Vector3 roamBase;

    Vector3 targetPosition;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterController = player.GetComponent<CustomCharacterController>();
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
            print("Holding fire");

            print("Distance: " + Vector3.Distance(transform.position, player.transform.position));
            print("fleeRadius: " + fleeRadius);

            if (Vector3.Distance(transform.position, player.transform.position) <= fleeRadius)
            {
                print("Fleeing");
                state = EnemyStates.Fleeing;
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
        Debug.Log("Now idle!");
        state = EnemyStates.Idle;
        yield return new WaitForSeconds(Random.Range(2f, 10f));
        if (state == EnemyStates.Idle)
        {
            targetPosition = GetNewRoamPosition();
            Debug.Log("Now moving!");
            state = EnemyStates.Moving;
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
        Debug.Log("Roaming");
        Vector3 targetVector;

        for (int i = 0; i < 10; i++)
        {
            // Random location
            Vector2 ran = Random.insideUnitCircle;
            targetVector = new Vector3(ran.x, 0.0f, ran.y);

            // Random distance
            targetVector *= Random.Range(0.0f, roamRadius);

            // Move to roam base
            targetVector += roamBase;

            if (IsMovePositionValid(targetVector))
            {
                targetVector.y = 1.0f;
                return targetVector;
            }
        }

        Debug.Log("Could not find valid position");
        return transform.position;
    }

    private Vector3 GetNewFleePosition()
    {
        Debug.Log("Fleing");
        Vector3 targetDirection = (transform.position - player.transform.position).normalized;
        Vector3 targetVector;

        for (int i = 0; i < 20; i++)
        {
            // Rotate
            targetDirection = Quaternion.Euler(0, Random.Range(-20f, 20f), 0) * targetDirection;

            // Scale
            targetDirection = targetDirection.normalized * Random.Range(3f, 8f);

            // Make vector
            targetVector = transform.position + targetDirection;
            if (IsMovePositionValid(targetVector))
            {
                targetVector.y = 1.0f;
                return targetVector;
            }
        }

        Debug.Log("Could not find valid position");
        return transform.position;
    }

    private bool IsMovePositionValid(Vector3 targetPosition)
    {
        Debug.Log("Monster " + gameObject.name + " would like to move to " + targetPosition);

        // Check the target vector for light
        if (LightManager.GetClosestLightDistanceToPosition(targetPosition) < fleeRadius)
        {
            Debug.Log("Arrival vector is in light");
            return false;
        }

        // Check for something in between
        Vector3 raySource = new Vector3(transform.position.x, 1.0f, transform.position.z);
        Vector3 rayTarget = new Vector3(targetPosition.x, 1.0f, targetPosition.z);
        Vector3 rayVector = rayTarget - raySource;
        if (Physics.Raycast(raySource, rayVector.normalized, rayVector.magnitude))
        {
            Debug.Log("Raycast hit something");
            return false;
        }

        // Check all the steps in between for light
        targetPosition.y = 0.0f;
        Vector3 moveVector = targetPosition - transform.position;
        Vector3 moveDirection = moveVector.normalized;
        float moveDistance = moveVector.magnitude;

        for (float checkDistance = 1.0f; checkDistance < moveDistance; checkDistance = checkDistance + 1.0f)
        {
            Vector3 newPosition = moveDirection * checkDistance;
            if (LightManager.GetClosestLightDistanceToPosition(newPosition) < deathRadius)
            {
                Debug.Log("Way would be in light!");
                return false;
            }
        }
        return true;
    }
}
