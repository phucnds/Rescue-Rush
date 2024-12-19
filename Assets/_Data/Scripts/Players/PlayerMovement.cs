using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerStatsDepnedency, IGameStateListener
{
    enum State
    {
        None,
        Joytick,
        Follow,
        Training,
        AutoFindCats
    }

    [SerializeField] private MobileJoystick joystick;
    private CharacterController characterController;
    private PlayerAnimator anim;

    private State state = State.None;

    [SerializeField] private float currentSpeed;
    [SerializeField] private float maxSpeed;

    private Vector3[] pointsList;
    private int nextPointIndex;
    private Vector3 destination;

    private Action onComplete;
    private bool debug;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        HandleState();
    }

    private void HandleState()
    {
        switch (state)
        {
            case State.Joytick:
                Joytick();
                break;

            case State.Follow:
                FollowTarget();
                break;

            case State.Training:
                CheckPosZ();
                break;

            case State.AutoFindCats:
                AutoFindCat();
                break;
        }

    }

    public void Move(float speed)
    {
        if(state == State.Training)
        {
            currentSpeed = speed;
            characterController.Move(Vector3.forward * currentSpeed * Time.fixedDeltaTime);
            anim.UpdateAnimator(Vector3.forward, currentSpeed / maxSpeed);
        }
    }

    private void MoveTo(Vector3 correctDirVector)
    {
        currentSpeed = correctDirVector.magnitude * maxSpeed;
        // Debug.Log($"{currentSpeed} = {correctDirVector.magnitude} * {maxSpeed}");

        characterController.Move(correctDirVector * currentSpeed * Time.fixedDeltaTime);
        anim.UpdateAnimator(correctDirVector, currentSpeed / maxSpeed);
    }

    public void Follow(Vector3 des, Action onCompleted = null)
    {
        destination = des;
        this.onComplete = onCompleted;
        state = State.Follow;
    }

    public void UpdateStats(PlayerStats playerStats)
    {
        maxSpeed = playerStats.GetValueStat(Stat.SPEED);
    }

    public float GetCurrentSpeed()
    {

        // Debug.Log("GetCurrentSpeed: " + currentSpeed);
        return currentSpeed;
    }

    private void Joytick()
    {
        Vector3 correctDirVector = new Vector3(joystick.GetMoveVector().x, 0, joystick.GetMoveVector().y);
        MoveTo(correctDirVector);
    }

    private void FollowTarget()
    {
        MoveTo((destination - transform.position).normalized);
        if (Vector3.Distance(transform.position, destination) < maxSpeed * Time.fixedDeltaTime && onComplete != null)
        {
            state = State.None;
            onComplete?.Invoke();
        }
    }

    private void CheckPosZ()
    {
        if (transform.position.z > 50000)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }

    public void StarMovement(Vector3[] pointsList)
    {
        nextPointIndex = 0;
        this.pointsList = pointsList;
        state = State.AutoFindCats;
    }

    private void AutoFindCat()
    {
        if (pointsList == null)
        {
            state = State.None;
            return;
        }

        if (nextPointIndex < pointsList.Length)
        {
            float distanceToNextPoint = Vector3.Distance(transform.position, pointsList[nextPointIndex]);
            if (distanceToNextPoint < maxSpeed * Time.fixedDeltaTime)
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
        MoveTo(moveVector.normalized);

    }

    private void OnMovementCompleted()
    {
        pointsList = null;
    }

    public float GetPosZ()
    {
        return transform.position.z;
    }

    public void SetDebug(bool flag)
    {
        debug = flag;
    }

    public void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.INTRO:
                ResetPos();
                break;

            case GameState.TRAINING:
                state = State.Training;
                break;

            case GameState.GAME:
                if (!debug) state = State.Joytick;
                break;
            case GameState.STAGECOMPLETE:
                state = State.None;
                break;

            case GameState.GAMEOVER:
                state = State.None;
                break;
        }
    }

    private void ResetPos()
    {
        transform.position = new Vector3(50, 0, 0); //startPos;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public void StartTraining()
    {
        state = State.Training;
    }

}
