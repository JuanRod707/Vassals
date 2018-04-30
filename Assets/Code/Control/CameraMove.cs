using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public float BaseSpeed;
    public Vector3 ZoomVector;
    public float ZoomSpeed;
    public float ZoomAngle;
    public GameObject MCam;

    private Quaternion originalRot;

    // Use this for initialization
    void Start ()
    {
        originalRot = MCam.transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    var Speed = BaseSpeed*(this.transform.position.y/4);

	    if (Input.GetKey(KeyCode.D))
	    {
	        this.transform.Translate(Speed,0,0);
	    }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(-Speed, 0, 0);
        }
	    if (Input.GetKey(KeyCode.W))
	    {
            this.transform.Translate(0, 0, Speed);
	    }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0, 0, -Speed);
        }

	    if (Input.mouseScrollDelta.y > 0)
	    {
	        if (this.transform.position.y > 5)
	        {
	            this.transform.Translate(ZoomVector*ZoomSpeed*(this.transform.position.y/2));
                MCam.transform.Rotate(new Vector3(-ZoomAngle, 0f, 0f));
	        }

	        if (this.transform.position.y < 9 & this.transform.position.y > 7)
	        {
	            MCam.transform.rotation = originalRot;
	        }

        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            if (this.transform.position.y < 70)
            {
                this.transform.Translate(ZoomVector*-ZoomSpeed*(this.transform.position.y/2));
                MCam.transform.Rotate(new Vector3(ZoomAngle, 0f, 0f));
            }

            if (this.transform.position.y < 9 & this.transform.position.y > 7)
            {
                MCam.transform.rotation = originalRot;
            }
        }
    }
}
