using UnityEngine;
using System.Collections;

public class SidebarController : MonoBehaviour
{
    public GameObject cityUpdater;
    public GameObject armyUpdater;
    public GameObject townUpdater;

    // Use this for initialization
    void Start () {
	    Deselect();
    }

    public void UpdateCityInfo(CityInfo cityInfo)
    {
        Deselect();
        cityUpdater.SetActive(true);
        cityUpdater.GetComponent<CityInfoUpdater>().PopulateFields(cityInfo);
    }

    public void UpdateTownInfo(TownInfo townInfo)
    {
        Deselect();
        townUpdater.SetActive(true);
        townUpdater.GetComponent<TownInfoUpdater>().PopulateFields(townInfo);
    }

    public void UpdateArmyInfo(ArmyInfo armyInfo)
    {
        Deselect();
        armyUpdater.SetActive(true);
        armyUpdater.GetComponent<ArmyInfoUpdater>().PopulateFields(armyInfo);
    }

    public void Deselect()
    {
        cityUpdater.SetActive(false);
        townUpdater.SetActive(false);
        armyUpdater.SetActive(false);
    }
}
