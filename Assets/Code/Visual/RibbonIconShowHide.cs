using UnityEngine;
using System.Collections;

public class RibbonIconShowHide : MonoBehaviour
{
    public float ThresholdHeight;
    private Camera cam;

    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
	void Update ()
    {
        if (this.transform.position.y > ThresholdHeight)
	    {
	        cam.cullingMask |= 1 << LayerMask.NameToLayer("RibbonIcons");
	    }
	    else
	    {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("RibbonIcons"));
        }
	}
}
