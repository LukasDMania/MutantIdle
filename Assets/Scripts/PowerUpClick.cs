using Unity.VisualScripting;
using UnityEngine;

public class PowerUpClick : MonoBehaviour
{
    public PowerUpManager Manager;
    public Sprite[] powerUpSprites;
    private Sprite _activeSprite;

    private void Awake()
    {
        if (powerUpSprites.Length != 0)
        {
            _activeSprite = powerUpSprites[UnityEngine.Random.Range(0, powerUpSprites.Length)];
        }
        GetComponent<SpriteRenderer>().sprite = _activeSprite;

        if (GetComponent<Collider2D>() == null)
        {
            gameObject.AddComponent<PolygonCollider2D>();
        }
        AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.PowerUpGlow);
        Destroy(gameObject, 10f);
    }

    private void OnMouseDown()
    {
        Manager.ActivatePowerUp();
    }
    private void OnDestroy()
    {
        AudioManager.Instance.AudioSystemSO.StopSound(SoundName.PowerUpGlow);
    }
}

