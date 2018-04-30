using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoraleCounter : MonoBehaviour
{
    public Color CPlatinum;
    public Color CHigh;
    public Color CMedium;
    public Color CLow;
    public float GraphWidthFactor;

    private Image moraleGraph;

    // Use this for initialization
    void Start ()
    {
        moraleGraph = this.GetComponent<Image>();
    }
	
	// Update is called once per frame
	public void UpdateMorale (int morale)
	{
	    moraleGraph.rectTransform.sizeDelta = new Vector2(morale*GraphWidthFactor, 30);

	    if (morale > 90)
	    {
	        moraleGraph.color = CPlatinum;
	    }
        else if (morale > 60)
        {
            moraleGraph.color = CHigh;
        }
        else if (morale > 40)
        {
            moraleGraph.color = CMedium;
        }
        else
        {
            moraleGraph.color = CLow;
        }
    }
}
