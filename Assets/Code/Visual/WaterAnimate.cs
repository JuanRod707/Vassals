using UnityEngine;
using System.Collections;

public class WaterAnimate : MonoBehaviour
{
    public Material MovingMaterial;
    public float MoveSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        MovingMaterial.mainTextureOffset += new Vector2(MoveSpeed, MoveSpeed);

	    if (MovingMaterial.mainTextureOffset.magnitude > 1)
	    {
	        MovingMaterial.mainTextureOffset = Vector2.zero;
	    }
    }
}
