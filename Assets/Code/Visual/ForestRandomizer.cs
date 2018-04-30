using UnityEngine;
using System.Collections;

public class ForestRandomizer : MonoBehaviour
{
    public Transform ForestMesh;

	// Use this for initialization
	void Start ()
	{
	    var rnd = Random.Range(0f, 360f);
	    ForestMesh.Rotate(new Vector3(0f, rnd, 0f));
	}
}
