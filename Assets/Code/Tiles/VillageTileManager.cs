using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VillageTileManager : MonoBehaviour
{
    public TileInfo TileRef;

    public GameObject[] Huts;

    public int MaxHouses;

	// Use this for initialization
	void Start ()
	{
        this.transform.SetParent(GameObject.Find("PopTiles").transform);
	    ErectHouses();
	}

    private void ErectHouses()
    {
        var buildings = new List<GameObject>();

        for (int i = 0; i < MaxHouses; i++)
        {
            var posInCircle = Random.insideUnitCircle;

            while (posInCircle.magnitude > 0.8f)
            {
                posInCircle = Random.insideUnitCircle;
            }

            var height = 0f;
            var pos = new Vector3(transform.position.x + posInCircle.x, transform.position.y + height,
                transform.position.z + posInCircle.y);

            var houseObj = Huts[Random.Range(0, Huts.Length)];
            var house = Instantiate(houseObj, pos, Quaternion.identity) as GameObject;
            
            var colliders =
                buildings.Where(
                    x => house.GetComponent<BoxCollider>().bounds.Intersects(x.GetComponent<BoxCollider>().bounds));

            int tries = 20;
            while (colliders.Any() && tries > 0)
            {
                while (posInCircle.magnitude > 0.8f)
                {
                    posInCircle = Random.insideUnitCircle;
                }

                pos = new Vector3(transform.position.x + posInCircle.x, transform.position.y + height,
                transform.position.z + posInCircle.y);
                house.transform.position = pos;
                colliders =
                buildings.Where(
                    x => house.GetComponent<BoxCollider>().bounds.Intersects(x.GetComponent<BoxCollider>().bounds));
                tries--;
            }

            if (tries > 0)
            {
                buildings.Add(house);
                house.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), 0f));
                house.transform.SetParent(this.transform);
            }
            else
            {
                GameObject.Destroy(house);
            }
        }
    }
}
