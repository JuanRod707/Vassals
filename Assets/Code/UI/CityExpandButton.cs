using UnityEngine;
using System.Collections;

public class CityExpandButton : MonoBehaviour
{
    public CityExpandMenu Menu;
    public Vector3 Offset;

    private bool isActive = false;

    public void Click()
    {
        Menu.gameObject.SetActive(!isActive);
        isActive = !isActive;
    }
}
