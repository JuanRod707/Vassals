using UnityEngine;
using System.Collections;

public class BannerRandomizer : MonoBehaviour
{
    public Material[] Materials;

    public Material GetRandomMaterial()
    {
        var i = Random.Range(0, Materials.Length);
        return Materials[i];
    }
}
