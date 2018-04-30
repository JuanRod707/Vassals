using UnityEngine;
using System.Collections;

public class SelectorHudCommand : MonoBehaviour
{
    public GameObject armyRef;
    public GameObject cityRef;
    public GameObject townRef;
    public GameObject Sidebar;

    public void ExpandCity(int districtId)
    {
        cityRef.GetComponent<CitadelTileMaster>().ExpandRandomDir((DistrictType)districtId);
    }
}
