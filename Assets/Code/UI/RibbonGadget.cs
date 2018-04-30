using UnityEngine;
using System.Collections;

public class RibbonGadget : MonoBehaviour
{
    public float ScaleFactor;
    public float CameraHeightThreshold;

    // Update is called once per frame
    void Update ()
	{
	    if (Camera.main.transform.position.y > CameraHeightThreshold)
	    {
	        var scale = Camera.main.transform.position.y*ScaleFactor;
	        transform.localScale = new Vector3(scale, scale, scale);
	    }
    }
}
