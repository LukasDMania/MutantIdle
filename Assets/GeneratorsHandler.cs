using Unity.VisualScripting;
using UnityEngine;

public class GeneratorsHandler : MonoBehaviour
{
    public ThingRuntimeSet GeneratorSet;

    public void gormParentChanged()
    {
        for (int i = 0; i < GeneratorSet.Items.Count; i++)
        {
            if (GeneratorSet.Items[i] is Generator generator)
            {
                
            }
        }
    }
}
