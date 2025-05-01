using System;
using UnityEngine;
using UnityEngine.Events;

public class TickManager : Singleton<TickManager>
{
    [SerializeField]
    private DoubleVariable _tickInterval;
    [SerializeField]
    private DoubleVariable _totalTicks;

    private double _tickTimer;
    public double TotalAfkSeconds;

    public UnityEvent OnGameTick;

    private void Start()
    {
        _totalTicks.SetValue(0);
    }

    private void Update()
    {
        if (_tickTimer > _tickInterval.Value)
        {
            _tickTimer = 0;
            _totalTicks.ApplyChange(1);

            if (CheckForTickAchievement())
            {
                AchievementManager.Instance.TryUnlockingAchievements();
            }
            PerformTick();
        }

        _tickTimer += Time.deltaTime;
    }

    private void PerformTick()
    {
        //Debug.Log("Tick Number: " + _totalTicks.Value);
        OnGameTick?.Invoke();
    }

    private bool CheckForTickAchievement() 
    {
        if (_totalTicks.Value == 1000)
        {
            return true;
        }
        if (_totalTicks.Value == 3000)
        {
            return true;
        }
        if (_totalTicks.Value == 5000)
        {
            return true;
        }
        if (_totalTicks.Value == 12000)
        {
            return true;
        }
        if (_totalTicks.Value == 30000)
        {
            return true;
        }
        if (_totalTicks.Value == 50000)
        {
            return true;
        }
        if (_totalTicks.Value == 100000)
        {
            return true;
        }
        if (_totalTicks.Value == 250000)
        {
            return true;
        }
        return false;
    }

    private void OnApplicationQuit()
    {
    }
}
