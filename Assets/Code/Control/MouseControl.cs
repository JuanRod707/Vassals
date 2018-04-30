using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{
    public GameObject Selector;
    public GameObject Sidebar;
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetMouseButtonDown(0))
	    {
            SelectEntity();
	    }

        if (Input.GetMouseButtonDown(1))
        {
            MoveArmy();
        }
    }

    private void SelectEntity()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();

        //Calculate where the mouse is aiming at
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag.Equals("City"))
            {
               SelectCity(hit.collider.gameObject);
            }

            if (hit.collider.tag.Equals("Army"))
            {
                SelectArmy(hit.collider.gameObject);
            }

            if (hit.collider.tag.Equals("Town"))
            {
                SelectTown(hit.collider.gameObject);
            }
        }
    }

    private void MoveArmy()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = new RaycastHit();

        //Calculate where the mouse is aiming at
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag.Equals("Tile"))
            {
                var tile = hit.collider.GetComponent<TileInfo>();
                var army = Selector.GetComponent<SelectorHudCommand>().armyRef;
                if (army != null)
                {
                    army.GetComponent<ArmyMovement>().SetTarget(tile.Coord);
                }
            }
        }
    }

    private void SelectCity(GameObject entity)
    {
        Selector.transform.SetParent(null);
        var city = entity.GetComponent<CitadelTileMaster>();

        if (city == null)
        {
            city = entity.GetComponent<DistrictTileMaster>().CitadelRef.GetComponent<CitadelTileMaster>();
        }

        Selector.transform.position = city.transform.position;
        Sidebar.GetComponent<SidebarController>().UpdateCityInfo(city.CityInfo);
        Selector.GetComponent<SelectorHudCommand>().cityRef = city.gameObject;
    }

    private void SelectTown(GameObject entity)
    {
        Selector.transform.SetParent(null);
        var town = entity.GetComponent<TownTileMaster>();

        Selector.transform.position = town.transform.position;
        Sidebar.GetComponent<SidebarController>().UpdateTownInfo(town.TownInfo);
        Selector.GetComponent<SelectorHudCommand>().townRef = town.gameObject;
    }

    private void SelectArmy(GameObject entity)
    {
        Selector.transform.position = entity.transform.position;
        Sidebar.GetComponent<SidebarController>().UpdateArmyInfo(entity.GetComponent<ArmyInfo>());
        Selector.GetComponent<SelectorHudCommand>().armyRef = entity;
        Selector.transform.SetParent(entity.transform);
    }

    private void Deselect()
    {
        Selector.transform.position = new Vector3(0f, -10f, 0f);
        Sidebar.GetComponent<SidebarController>().Deselect();
    }
}
