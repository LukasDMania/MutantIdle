using UnityEngine;
using UnityEngine.Events;

public class PrestigeHandler : MonoBehaviour
{
    public UnityEvent OnPrestigeEvent;
    public UnityEvent OnPoststigeEvent;
    public void Prestige() 
    {
        OnPrestigeEvent?.Invoke();
        OnPoststigeEvent?.Invoke();
    }
}
