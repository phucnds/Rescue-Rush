using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour, IPlayerStatsDepnedency
{
    enum State
    {
        Powerful,
        Tired,
    }

    [SerializeField] private float tapDelay = .1f;
    [SerializeField] private float autoDelay = .2f;
    [SerializeField] private float tiredSpeed = .5f;

    [SerializeField] private bool isAuto;

    private float currentSpeed = 0;
    private float currentStamina = 0;
    private float maxSpeed = 0;
    private float maxStamina = 0;

    private PlayerStats playerStats;
    private PlayerMovement playerMovement;

    private float tapTimer = 0;

    private bool canTap = false;

    private float autoTimer;


    private bool isSpeedUp;
    private float speedUpTimer;

    private State state = State.Powerful;

    private bool isFirstLoad = true;
    private bool isTraining;

    public static Action<Stat, float> OnShowTextFading;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        autoTimer = autoDelay;
    }

    private void Update()
    {
        if(!isTraining) return;

        tapTimer += Time.deltaTime;
        if (tapTimer >= tapDelay)
        {
            canTap = true;
        }

        HandleState();
        HandleMove();

        if (isAuto) AutoTap();

        playerMovement.Move(currentSpeed);
    }

    private void AutoTap()
    {
        autoTimer += Time.deltaTime;
        if (autoTimer >= autoDelay)
        {
            autoTimer = 0;

            Tap();
        }
    }

    [NaughtyAttributes.Button]
    public void Tap()
    {
        if (!canTap) return;

        tapTimer = 0;
        canTap = false;

        SpeedUp();
    }

    private void SpeedUp()
    {
        isSpeedUp = true;
        speedUpTimer = 1;

        float income = playerStats.GetValueStat(Stat.INCOME);
        CurrencyManager.Instance.AddCurrency(income);

        if (state == State.Powerful)
        {
            float speed = maxSpeed;
            currentStamina -= speed;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }


        OnShowTextFading?.Invoke(Stat.INCOME, income);
        OnShowTextFading?.Invoke(Stat.SPEED, maxSpeed);
        OnShowTextFading?.Invoke(Stat.STAMINA, -maxSpeed);
    }

    private void HandleState()
    {
        switch (state)
        {
            case State.Powerful:
                if (currentStamina <= 0) state = State.Tired;
                if (currentSpeed <= .01f) currentSpeed = 0;
                if (currentSpeed <= 0)
                {
                    RecoveryStamina();
                }

                tapDelay = .2f;
                break;

            case State.Tired:
                tapDelay = 1f;
                RecoveryStamina();
                break;
        }
    }

    private void HandleMove()
    {
        if (isSpeedUp)
        {
            speedUpTimer -= Time.deltaTime;

            if (speedUpTimer <= 0)
            {
                isSpeedUp = false;
            }

            float val = state == State.Tired ? tiredSpeed : 1;

            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * val, 2f * Time.deltaTime);
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime);
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        }
    }


    private void RecoveryStamina()
    {
        currentStamina += maxStamina * .1f * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (currentStamina >= maxStamina)
        {
            state = State.Powerful;
        }
    }

    public void UpdateStats(PlayerStats playerStats)
    {
        this.playerStats = playerStats;

        maxSpeed = playerStats.GetValueStat(Stat.SPEED);
        maxStamina = playerStats.GetValueStat(Stat.STAMINA);

        if (isFirstLoad)
        {
            currentStamina = maxStamina;
            isFirstLoad = false;
        }

    }

    public string DisplayStamina()
    {
        return $"{currentStamina}/{maxStamina}";
    }

    public float StanimaThreshold()
    {
        return currentStamina / Mathf.Max(maxStamina, 0.01f);
    }

    public void ToggleAuto(bool flag)
    {
        isAuto = flag;
    }

    public bool IsAuto()
    {
        return isAuto;
    }

    public void StartTraining()
    {
        isTraining = true;
        playerMovement.StartTraining();
    }
}
