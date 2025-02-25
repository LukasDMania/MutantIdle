using UnityEngine;

public class SpriteMovement : MonoBehaviour
{
    [System.Serializable]
    public class MovementSettings
    {
        public bool enabled = true;
        public float speed = 1f;
        public float amplitude = 1f;
        public float timeOffset = 0f;
    }

    [Header("Breathing")]
    public MovementSettings breathing = new MovementSettings
    {
        speed = 1f,
        amplitude = 0.05f
    };

    [Header("Floating")]
    public MovementSettings floating = new MovementSettings
    {
        speed = 0.5f,
        amplitude = 0.1f
    };

    [Header("Swaying")]
    public MovementSettings swaying = new MovementSettings
    {
        speed = 0.7f,
        amplitude = 2f
    };

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private float time;

    private void Start()
    {
        originalPosition = transform.localPosition;
        originalScale = transform.localScale;

        breathing.timeOffset = Random.Range(0f, Mathf.PI * 2);
        floating.timeOffset = Random.Range(0f, Mathf.PI * 2);
        swaying.timeOffset = Random.Range(0f, Mathf.PI * 2);
    }

    private void Update()
    {
        time += Time.deltaTime;
        Vector3 newPosition = originalPosition;
        Vector3 newScale = originalScale;

        if (breathing.enabled)
        {
            float breathe = Mathf.Sin((time * breathing.speed) + breathing.timeOffset) * breathing.amplitude;
            newScale += new Vector3(breathe, breathe, 0);
        }

        if (floating.enabled)
        {
            float float_offset = Mathf.Sin((time * floating.speed) + floating.timeOffset) * floating.amplitude;
            newPosition.y += float_offset;
        }

        if (swaying.enabled)
        {
            float sway = Mathf.Sin((time * swaying.speed) + swaying.timeOffset) * swaying.amplitude;
            transform.rotation = Quaternion.Euler(0, 0, sway);
        }

        transform.localPosition = newPosition;
        transform.localScale = newScale;
    }

    public void DisableAllEffects()
    {
        breathing.enabled = false;
        floating.enabled = false;
        swaying.enabled = false;
    }
}