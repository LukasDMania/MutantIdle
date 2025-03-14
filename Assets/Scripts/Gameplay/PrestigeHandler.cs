using UnityEngine;
using UnityEngine.Events;

public class PrestigeHandler : MonoBehaviour
{
    public UnityEvent OnPrestigeEvent;
    public void Prestige() 
    {
        OnPrestigeEvent?.Invoke();
    }
}
