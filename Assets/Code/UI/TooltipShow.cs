using UnityEngine;
using System.Collections;

public class TooltipShow : MonoBehaviour
{
    public GameObject Tooltip;

    private void Start()
    {
        Tooltip.SetActive(false);
    }

    public void OnMouseEnter()
    {
        Tooltip.SetActive(true);
    }

    public void OnMouseExit()
    {
        Tooltip.SetActive(false);
    }
}
