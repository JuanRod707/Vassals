using UnityEngine;
using System.Collections;

public class SelectorEffect : MonoBehaviour
{
    public float RotateSpeed;

	// Update is called once per frame
	void Update () {
	    this.transform.Rotate(new Vector3(0f, 0f, RotateSpeed));
	}
}
