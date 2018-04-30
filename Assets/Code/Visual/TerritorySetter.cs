using UnityEngine;
using System.Collections;

public class TerritorySetter : MonoBehaviour
{
    public SpriteRenderer TerritorySprite;

    private void Start()
    {
        if(this.GetComponent<TileInfo>().Height <= -5)
        {
            TerritorySprite.enabled = false;
        }
    }

    public void SetTerritoryColor(Color color)
    {
        color.a = 0.5f;
        TerritorySprite.color = color;
    }
}
