using UnityEngine;
using System.Collections;

public class LodShowHide : MonoBehaviour
{
    public float Level1ThresholdHeight;
    public float Level2ThresholdHeight;
    public float Level3ThresholdHeight;

    private Camera cam;

    private void Start()
    {
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
	void Update ()
    {
        if (this.transform.position.y < Level1ThresholdHeight)
	    {
	        cam.cullingMask |= 1 << LayerMask.NameToLayer("LODlvl1");
	    }
	    else
	    {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("LODlvl1"));
        }

        if (this.transform.position.y < Level2ThresholdHeight)
        {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("LODlvl2");
        }
        else
        {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("LODlvl2"));
        }

        if (this.transform.position.y < Level3ThresholdHeight)
        {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("LODlvl3");
        }
        else
        {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("LODlvl3"));
        }
    }
}
