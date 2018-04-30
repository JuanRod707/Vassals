using UnityEngine;
using System.Collections;

public class DebugKeys : MonoBehaviour
{
    public KeyCode KeyTerritory;
    public KeyCode KeyLines;
    public KeyCode KeyNav;
    public KeyCode KeyArmy;
    public GameObject ArmyAvatar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyTerritory))
        {
            Camera.main.cullingMask ^= 1 << LayerMask.NameToLayer("Territory");
        }

        if (Input.GetKeyDown(KeyLines))
        {
            Camera.main.cullingMask ^= 1 << LayerMask.NameToLayer("DependencyLines");
        }

        if (Input.GetKeyDown(KeyNav))
        {
            Camera.main.cullingMask ^= 1 << LayerMask.NameToLayer("NavMap");
        }

        if (Input.GetKeyDown(KeyArmy))
        {
            CreateArmy();
        }
    }

    void CreateArmy()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();

        //Calculate where the mouse is aiming at
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag.Equals("Tile"))
            {
                var tileInfo = hit.collider.GetComponent<TileInfo>();
                Instantiate(ArmyAvatar, tileInfo.transform.position, Quaternion.identity);
            }
        }
    }
}
