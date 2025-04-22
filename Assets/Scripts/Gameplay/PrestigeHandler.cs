using UnityEngine;
using UnityEngine.Events;

public class PrestigeHandler : MonoBehaviour
{
    public UnityEvent OnPrestigeEvent;
    public UnityEvent OnPoststigeEvent;

    public DoubleVariable TotalPrestiges;
    public void Prestige() 
    {
        TotalPrestiges.ApplyChange(1);
        OnPrestigeEvent?.Invoke();
        OnPoststigeEvent?.Invoke();
    }
}
