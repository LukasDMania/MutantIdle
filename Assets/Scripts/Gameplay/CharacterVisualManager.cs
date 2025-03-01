using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualManager : MonoBehaviour
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
        SpriteMovement spriteMovement = bodyPartObj.AddComponent<SpriteMovement>();
        spriteMovement.DisableAllEffects();
        spriteRenderer.sortingOrder = bodyPartData.SortingOrder;
        spriteRenderer.sprite = bodyPartData.GetSpriteForLevel(generator.GeneratorLevel);
        bodyPartObj.transform.localPosition = bodyPartData.AttachmentPoint;

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
}
