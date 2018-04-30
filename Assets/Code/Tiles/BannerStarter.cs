using UnityEngine;
using System.Collections;

public class BannerStarter : MonoBehaviour
{
    private MeshRenderer banner;
    
    void Start ()
    {
        banner = this.transform.Find("banner").GetComponent<MeshRenderer>();
        var mat = this.banner.GetComponent<MeshRenderer>().materials[0];
        banner.materials = new[] { mat, GameObject.Find("Resources").GetComponent<BannerRandomizer>().GetRandomMaterial() };
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
