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
    }

    private void Update()
    {
        if (_tickTimer > _tickInterval.Value)
        {
            _tickTimer = 0;
            _totalTicks.ApplyChange(1);

            PerformTick();
        }

        _tickTimer += Time.deltaTime;
    }

    private void PerformTick()
    {
        OnGameTick?.Invoke();
    }

    private void OnApplicationQuit()
    {
    }
}
