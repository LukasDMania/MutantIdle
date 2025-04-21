using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpManager : MonoBehaviour
{
    public PowerUpDataHolder powerUpDataHolder;
    public UnityEvent OnPowerUpClicked;

    public GameObject TextAfterPowerUpPrefab;

    [SerializeField] private float _timer = 0;
    [SerializeField] private float _powerupInterval = 1;

    public GameObject PowerUpToClickPrefab;
    public GameObject SpawnedPowerUp;

    private Tween moveTween;
    private Tween rotateTween;

    private List<IPowerUpFactory> _powerupFactories;

    public void Awake()
    {
        _powerupFactories = new List<IPowerUpFactory>
        {
            new DoubleCurrencyPowerUpFactory(),
            new ProductionBoostPowerUpFactory()
            // Extend Here
        };
    }
    public void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _powerupInterval)
        {
            SpawnPowerUpImage();
            _timer = 0;
            RandomizePowerUpInterval();
        }
    }

    private void RandomizePowerUpInterval()
    {
        _powerupInterval = UnityEngine.Random.Range(60f,300f);
    }

    private void SpawnPowerUpImage()
    {
        Vector3 spawnPosition = GetRandomOffscreenPosition();
        SpawnedPowerUp = Instantiate(PowerUpToClickPrefab, spawnPosition, Quaternion.identity);
        SpawnedPowerUp.GetComponent<PowerUpClick>().Manager = this;
        Vector3 targetPosition = GetOppositePosition(spawnPosition);

        moveTween = SpawnedPowerUp.transform.DOMove(targetPosition, 10)
        .SetEase(Ease.OutQuad);

        rotateTween = null;

        moveTween.OnStart(() => {
            rotateTween = SpawnedPowerUp.transform.DORotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360);
        })
        .OnComplete(() => {
            moveTween.Kill();
            if (rotateTween != null)
                rotateTween.Kill();

            Destroy(SpawnedPowerUp);
        });

    }

    public void ActivatePowerUp()
    {
        Debug.Log("Power-up clicked!");

        if (_powerupFactories != null) 
        {
            int index = UnityEngine.Random.Range(0, _powerupFactories.Count);
            IPowerUpFactory powerUpFactory = _powerupFactories[index];

            IPowerUp powerUpToActivate = powerUpFactory.CreatePowerUp();
            powerUpToActivate.ApplyPowerUp(powerUpDataHolder);

            OnPowerUpClicked?.Invoke();
            moveTween.Kill();
            if (rotateTween != null)
                rotateTween.Kill();

            Vector3 positionOnClick = SpawnedPowerUp.transform.position;
            Destroy(SpawnedPowerUp);

            DisplayInfoAboutActivatedPowerUp(powerUpToActivate, positionOnClick);
        }
    }

    private void DisplayInfoAboutActivatedPowerUp(IPowerUp powerUp, Vector3 position)
    {
        GameObject text = Instantiate(TextAfterPowerUpPrefab, position, Quaternion.identity);
        TextMeshPro textMesh = text.GetComponent<TextMeshPro>();
        textMesh.text = powerUp.TextToDisplayAfterActivation();

        textMesh.alpha = 0f;

        textMesh.DOFade(1f, 1f)
            .OnComplete(() => {
                textMesh.DOFade(0f, 1f)
                    .SetDelay(3f)
                    .OnComplete(() => {
                        Destroy(text);
                    });
            });
    }

    private Vector3 GetRandomOffscreenPosition()
    {
        float x = UnityEngine.Random.value > 0.5f ? -12f : 12f;
        float y = UnityEngine.Random.Range(-4f, 4f);

        Vector3 randomPos = new Vector3(x, y, -5f);

        return randomPos;
    }

    Vector3 GetOppositePosition(Vector3 startPos)
    {
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float targetX = -startPos.x;
        float targetY = -startPos.y;

        return new Vector3(targetX, targetY, -5);
    }
}
