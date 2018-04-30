using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LabelWrite : MonoBehaviour
{
    public string BaseString;

    public void WriteLabel(params object[] args)
    {
        this.GetComponent<Text>().text = string.Format(BaseString, args);
    }
}
