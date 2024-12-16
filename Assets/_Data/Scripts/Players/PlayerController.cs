using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private float moveSpeed = 10f;

    private CharacterController characterController;
    private PlayerAnimator anim;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        Debug.Log(GetCurrentSpeed());

        if (pointsList != null)
        {
            FollowTarget();
        }
        else if (isFollow)
        {
            Movements(destination - transform.position);

            if (Vector3.Distance(transform.position, destination) < moveSpeed * Time.deltaTime && onComplete != null)
            {
                isFollow = false;
                onComplete?.Invoke();
            }
        }
        else
        {
            Vector3 correctDirVector = GetCorrectDirVector(joystick.GetMoveVector());
            Movements(correctDirVector);
        }


    }

    public float GetCurrentSpeed()
    {
        return joystick.GetMoveVector().magnitude * moveSpeed;
    }

    public float GetMaxSpeed()
    {
        return moveSpeed;
    }

    public void MoveTo(Vector3 des, Action onCompleted = null)
    {
        destination = des;
        this.onComplete = onCompleted;
        isFollow = true;
    }

    private void Movements(Vector3 dirVector)
    {
        anim.ManageAnimations(dirVector);

        Vector3 moveVector = dirVector.normalized * moveSpeed * Time.fixedDeltaTime;
        characterController.Move(moveVector);
    }

    private Vector3 GetCorrectDirVector(Vector3 dir)
    {
        Vector3 correctDirVector = dir;
        correctDirVector.z = correctDirVector.y;
        correctDirVector.y = 0;
        return correctDirVector;
    }



    //=======================FOLLOW TARGETS========================================


    private Vector3[] pointsList;
    private int nextPointIndex;
    private bool isFollow;
    private Vector3 destination;

    private Action onComplete;

    public void StarMovement(Vector3[] pointsList)
    {
        nextPointIndex = 0;
        this.pointsList = pointsList;
    }

    private void FollowTarget()
    {
        if (nextPointIndex < pointsList.Length)
        {
            float distanceToNextPoint = Vector3.Distance(transform.position, pointsList[nextPointIndex]);
            if (distanceToNextPoint < moveSpeed * Time.deltaTime)
            {
                nextPointIndex++;
                if (nextPointIndex >= pointsList.Length)
                {
                    OnMovementCompleted();
                    return;
                }
            }
        }

        Vector3 moveVector = pointsList[nextPointIndex] - transform.position;
        Movements(moveVector);

    }

    private void OnMovementCompleted()
    {
        pointsList = null;
    }

    public void MoveToGoal()
    {
        Vector3[] goals = new Vector3[] { new Vector3(50, 0, 1400) };
        StarMovement(goals);
    }

    public float GetPosZ()
    {
        return transform.position.z;
    }

    //==================================================================================



}
