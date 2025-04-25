using UnityEngine;
using UnityEngine.UI;


public class LightFlicker : Singleton<LightFlicker>
{
    [SerializeField] private Image blackOverlay;

    [SerializeField] private float minAlpha = 0.0f;
    [SerializeField] private float maxAlpha = 0.9f;
    [SerializeField] private float flickerDuration = 4.0f;
    [SerializeField] private int flickerCount = 4;
    [SerializeField] private float fadeOutSpeed = 0.5f;

    private Color overlayColor;
    private bool isFlickering = false;
    private float flickerTimer = 0f;
    private float originalAlpha;
    private int currentFlickerCount = 0;
    private bool isCurrentlyDim = false;
    private float nextFlickerTime = 0f;
    private float alphaTarget = 0f;
    private float currentLerpTime = 0f;
    private float lerpDuration = 0.8f;
    public float flickerCooldownTimer = 0f;
    public float nextFlickerCooldown = 0f;

    private int currentSoundPlays;
    private int maxSoundPlays;
    private float audioIntervalTime;
    private float nextAudioTime;


    void Start()
    {
        if (blackOverlay == null)
            blackOverlay = GetComponent<Image>();

        overlayColor = blackOverlay.color;
        originalAlpha = overlayColor.a;

        overlayColor.a = 0;
        blackOverlay.color = overlayColor;

        nextFlickerCooldown = Random.Range(120f, 1500f);
    }

    void Update()
    {
        if (isFlickering)
        {
            flickerTimer -= Time.deltaTime;

            if (Time.time >= nextFlickerTime && currentFlickerCount < flickerCount)
            {
                TriggerNextFlicker();
            }

            currentLerpTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentLerpTime / lerpDuration);
            float smoothT = Mathf.SmoothStep(0, 1, t);

            overlayColor.a = Mathf.Lerp(overlayColor.a, alphaTarget, smoothT);
            blackOverlay.color = overlayColor;

            if (flickerTimer <= 0)
            {
                FadeToBlack();
            }
        }
        else if (overlayColor.a > originalAlpha)
        {
            overlayColor.a = Mathf.Lerp(overlayColor.a, originalAlpha, Time.deltaTime);
            blackOverlay.color = overlayColor;
        }

        flickerCooldownTimer += Time.deltaTime;
        if (flickerCooldownTimer >= nextFlickerCooldown)
        {
            flickerCooldownTimer = 0f;
            nextFlickerCooldown = Random.Range(120f, 1500f);
            StartFlicker();
        }
    }

    public void StartFlicker(float duration = -1)
    {
        //Trigger flicekr sound
        isFlickering = true;
        flickerCount = Random.Range(4, 12);
        flickerDuration = Random.Range(2, 5.5f);
        maxAlpha = Random.Range(0.8f, 1.1f);
        flickerTimer = (duration > 0) ? duration : flickerDuration;
        currentFlickerCount = 0;
        isCurrentlyDim = false;

        overlayColor.a = minAlpha;
        blackOverlay.color = overlayColor;

        maxSoundPlays = Mathf.Clamp(Mathf.CeilToInt(flickerTimer / 2f), 1, 3);
        currentSoundPlays = 0;
        audioIntervalTime = flickerTimer / maxSoundPlays;
        nextAudioTime = Time.time + (audioIntervalTime);
        PlayFlickerSound();

        TriggerNextFlicker();
    }
    private void PlayFlickerSound() 
    {
        switch (Random.Range(0, 2)) 
        {
            case 0:
                AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.LightFlicker1);
                break;
            case 1:
                AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.LightFlicker2);
                break;
            case 2:
                AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.LightFlicker3);
                break;
        }
    }

    private void TriggerNextFlicker()
    {
        currentFlickerCount++;

        if (currentSoundPlays < maxSoundPlays && Time.time >= nextAudioTime)
        {
            PlayFlickerSound();
            currentSoundPlays++;
            nextAudioTime = Time.time + audioIntervalTime; // Schedule next audio
        }


        float baseDelay = flickerDuration / (flickerCount + 2);
        float randomVariation = baseDelay * 0.4f;

        isCurrentlyDim = !isCurrentlyDim;

        if (isCurrentlyDim)
        {
            alphaTarget = Mathf.Lerp(minAlpha, maxAlpha, (float)currentFlickerCount / flickerCount);
            lerpDuration = 0.1f + (0.3f * (float)currentFlickerCount / flickerCount);
        }
        else
        {
            alphaTarget = minAlpha;
            lerpDuration = 0.3f - (0.15f * (float)currentFlickerCount / flickerCount);
        }

        currentLerpTime = 0f;

        float nextDelay = baseDelay + Random.Range(-randomVariation, randomVariation);
        nextDelay *= (currentFlickerCount / (float)flickerCount) + 0.5f;

        nextFlickerTime = Time.time + nextDelay;
    }

    private void FadeToBlack()
    {
        alphaTarget = maxAlpha;
        lerpDuration = 1.5f / fadeOutSpeed;
        currentLerpTime = 0f;

        Invoke("StopFlicker", lerpDuration + 0.5f);
    }

    public void StopFlicker()
    {
        isFlickering = false;
    }

    public void ResetLights()
    {
        isFlickering = false;
        overlayColor.a = originalAlpha;
        blackOverlay.color = overlayColor;
    }
}