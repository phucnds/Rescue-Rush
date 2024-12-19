using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IGameStateListener
{
    [SerializeField] private MobileJoystick joystick;

    private CharacterController characterController;
    private PlayerAnimator anim;
    private PlayerStats playerStats;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<PlayerAnimator>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
        // HandleState();
    }

    public float GetCurrentSpeed()
    {
        return characterController.velocity.magnitude;
    }

    public float GetMaxSpeed()
    {
        return playerStats.GetValueStat(Stat.SPEED);
    }

    public void Follow(Vector3 des, Action onCompleted = null)
    {
        destination = des;
        this.onComplete = onCompleted;
        isFollow = true;
    }

    public void Movement(Vector3 dirVector)
    {
        // anim.ManageAnimations(dirVector);

        Vector3 moveVector = dirVector * GetMaxSpeed() * Time.fixedDeltaTime;
        characterController.Move(moveVector);

    }

    public void ToggleTrainingState(bool flag)
    {
        isTraning = flag;
    }

    private Vector3 GetCorrectDirVector(Vector3 dir)
    {
        Vector3 correctDirVector = dir;
        correctDirVector.z = correctDirVector.y;
        correctDirVector.y = 0;
        return correctDirVector;
    }

    private void HandleState()
    {
        if (pointsList != null)
        {
            FollowTarget();
        }
        else if (isFollow)
        {
            Movement(destination - transform.position);
            if (Vector3.Distance(transform.position, destination) < GetMaxSpeed() * Time.deltaTime && onComplete != null)
            {
                isFollow = false;
                onComplete?.Invoke();
            }
        }

        else if (isTraning)
        {
            if (transform.position.z > 50000)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }
        }
        else
        {
            Vector3 correctDirVector = GetCorrectDirVector(joystick.GetMoveVector());
            Movement(correctDirVector);

            Debug.Log("das");
        }
    }



    //=======================FOLLOW TARGETS========================================


    private Vector3[] pointsList;
    private int nextPointIndex;
    private bool isFollow;
    private bool isTraning;
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
            if (distanceToNextPoint < GetMaxSpeed() * Time.deltaTime)
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
        Movement(moveVector);

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

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.TRAINING:
                isTraning = true;
                break;
        }
    }

    //==================================================================================



}
