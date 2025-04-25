using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualManager : MonoBehaviour, IPrestigable
{
    [SerializeField]
    private Transform _characterRoot;
    private Dictionary<Generator, SpriteRenderer> _activeBodyParts = new Dictionary<Generator, SpriteRenderer>();

    public void AddBodyPart(Generator generator, BodyPartDataSO bodyPartData)
    {
        if (generator.GeneratorLevel <= 0 || _activeBodyParts.ContainsKey(generator))
        {
            return;
        }

        GameObject bodyPartObj = new GameObject(bodyPartData.BodyPartName);
        bodyPartObj.transform.SetParent(_characterRoot);


        SpriteRenderer spriteRenderer = bodyPartObj.AddComponent<SpriteRenderer>();
        //SpriteMovement spriteMovement = bodyPartObj.AddComponent<SpriteMovement>();
        //spriteMovement.DisableAllEffects();
        spriteRenderer.sortingOrder = bodyPartData.SortingOrder;
        spriteRenderer.sprite = bodyPartData.GetSpriteForLevel(generator.GeneratorLevel);


        //Animate
        Vector3 randomRotation = new Vector3(
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f),
            Random.Range(-180f, 180f)
        );

        int rando = Random.Range(2, 4);
        bodyPartObj.transform.localRotation = Quaternion.Euler(randomRotation);
        bodyPartObj.transform.localPosition = GetOffScreenPosition();
        bodyPartObj.transform.DOLocalRotate(Vector3.zero, rando).SetEase(GetRandomEase());
        bodyPartObj.transform.DOLocalMove(bodyPartData.AttachmentPoint, rando)
            .SetEase(GetRandomEase())
            .OnComplete(() =>
            {
                AudioManager.Instance.AudioSystemSO.PlayUISound(SoundName.BodyPartAttached);
                // Particles
            });



        _activeBodyParts[generator] = spriteRenderer;
    }

    public void UpdateBodyPartSprite(Generator generator, BodyPartDataSO bodyPartData)
    {
        if (!_activeBodyParts.TryGetValue(generator, out SpriteRenderer spriteRenderer))
        {
            return;
        }

        Sprite newSprite = bodyPartData.GetSpriteForLevel(generator.GeneratorLevel);
        Debug.Log(newSprite);
        if (spriteRenderer.sprite != newSprite)
        {
            spriteRenderer.sprite = newSprite;
            Debug.Log(spriteRenderer.sprite);
            //Add visual effects for sprite change
            //StartCoroutine(PlayUpgradeEffect(spriteRenderer));
        }
    }

    public void PrestigeResetCharacterVisualManager() 
    {
        foreach (var bodyPart in _activeBodyParts.Values)
        {
            Destroy(bodyPart.gameObject);
        }
        _activeBodyParts.Clear();
    }


    private Vector2 GetOffScreenPosition()
    {
        // Get screen boundaries in world space
        Camera mainCamera = Camera.main;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert screen boundaries to world points
        Vector3 screenBottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, mainCamera.nearClipPlane));

        // Choose a random direction (left, right, top, bottom) to place the position outside the screen
        int direction = Random.Range(0, 4);
        Vector2 offScreenPosition = Vector2.zero;

        switch (direction)
        {
            case 0: // Left of the screen
                offScreenPosition = new Vector2(screenBottomLeft.x - 1, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 1: // Right of the screen
                offScreenPosition = new Vector2(screenTopRight.x + 1, Random.Range(screenBottomLeft.y, screenTopRight.y));
                break;
            case 2: // Below the screen
                offScreenPosition = new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), screenBottomLeft.y - 1);
                break;
            case 3: // Above the screen
                offScreenPosition = new Vector2(Random.Range(screenBottomLeft.x, screenTopRight.x), screenTopRight.y + 1);
                break;
        }

        return offScreenPosition;
    }

    private Ease GetRandomEase()
    {
        Ease[] easeTypes = new Ease[]
        {
        Ease.Linear,
        Ease.InOutQuad,
        Ease.OutBounce,
        Ease.InOutElastic,
        Ease.OutBack
        };

        int randomIndex = UnityEngine.Random.Range(0, easeTypes.Length);
        return easeTypes[randomIndex];
    }

    public void PrestigeReset()
    {
        foreach (var bodyPart in _activeBodyParts.Values)
        {
            Destroy(bodyPart.gameObject);
        }
        _activeBodyParts.Clear();
    }
}
