using System;
using UnityEngine;

public class PlayerTraining : MonoBehaviour, IPlayerStatsDepnedency
{
    enum State
    {
        Idle,
        Powerful,
        Tired,

    }

    private PlayerStats playerStats;
    private PlayerMovement playerMovement;

    [SerializeField] private float currentSpeed = 0;
    [SerializeField] private float currentStamina = 0;
    [SerializeField] private float maxSpeed = 0;
    [SerializeField] private float maxStamina = 0;

    [SerializeField] private bool isSpeedUp;
    [SerializeField] private float speedUpTimer;

    private float tiredSpeed = .2f;
    private State state = State.Powerful;


    private float timer = 1;
    private float timeTap = .1f;
    private bool canTap = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        timer += Time.deltaTime;
        if (timer >= timeTap)
        {
            canTap = true;
        }

        HandleState();

        if (isSpeedUp)
        {
            speedUpTimer -= Time.deltaTime;

            if (speedUpTimer <= 0)
            {
                isSpeedUp = false;
            }

            float val = state == State.Tired ? tiredSpeed : 1;

            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * val, Time.deltaTime);
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime);
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }

        playerMovement.Move(currentSpeed);

        
    }

    private void HandleState()
    {
        if (currentStamina <= 0)
        {
            state = State.Tired;
        }
    }

    [NaughtyAttributes.Button]
    public void Tap()
    {
        if (!canTap) return;

        timer = 0;
        canTap = false;

        isSpeedUp = true;
        speedUpTimer = 1;


        float income = playerStats.GetValueStat(Stat.INCOME);
        CurrencyManager.Instance.AddCurrency(income);

        if(state == State.Powerful)
        {
            float speed = maxSpeed;
            currentStamina -= speed;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
    }





    private void SpeedUp()
    {
        speedUpTimer -= Time.deltaTime;

        if (speedUpTimer <= 0)
        {
            state = State.Idle;
        }

        if (currentStamina <= 0)
        {
            state = State.Tired;
        }

        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime);
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    private void Tired()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, tiredSpeed, Time.deltaTime);
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        RecoveryStamina();
    }

    private void RecoveryStamina()
    {
        if (currentStamina >= maxStamina) return;

        currentStamina += maxStamina * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina >= maxStamina)
        {
            if (isSpeedUp) state = State.Powerful;
            else state = State.Idle;
        }
    }

    [NaughtyAttributes.Button]
    public void SpeedUps()
    {
        if (currentStamina <= 0) return;

        state = State.Powerful;
        isSpeedUp = true;

        speedUpTimer = 1;

        float income = playerStats.GetValueStat(Stat.INCOME);
        CurrencyManager.Instance.AddCurrency(income);

        float speed = maxSpeed;

        currentStamina -= speed;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public void UpdateStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;

        maxSpeed = playerStats.GetValueStat(Stat.SPEED);
        maxStamina = playerStats.GetValueStat(Stat.STAMINA);
        currentStamina = maxStamina;
    }

    public string DisplayStamina()
    {
        return $"{currentStamina}/{maxStamina}";
    }

    public float StanimaThreshold()
    {
        return currentStamina / Mathf.Max(maxStamina, 0.01f);
    }
}
