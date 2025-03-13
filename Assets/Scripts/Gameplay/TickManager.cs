using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public class TickManager : Singleton<TickManager>
{
    [SerializeField]
    private DoubleVariable _tickInterval;
    [SerializeField]
    private DoubleVariable _totalTicks;

    private double _tickTimer;


    public UnityEvent OnGameTick;

    private void Update()
    {
        if (_tickTimer > _tickInterval.Value)
        {
            _tickTimer = 0;
            _totalTicks.SetValue(_totalTicks.Value + 1);

            PerformTick();
        }

        _tickTimer += Time.deltaTime;
    }

    private void PerformTick()
    {
        OnGameTick?.Invoke();
    }
}
