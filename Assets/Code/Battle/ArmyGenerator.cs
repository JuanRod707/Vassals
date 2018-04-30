using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmyGenerator : MonoBehaviour
{
    public GameObject Archer;
    public GameObject Knight;
    public GameObject Spearman;

    public int SpearmanRatio;
    public int ArcherRatio;
    public int KnightRatio;

    public int SpearmanRanks;
    public int KnightRanks;
    public int ArcherRanks;
    public float unitSeparationVert;
    public float unitSeparationHoriz;

    private Vector3 KnightLine;
    private Vector3 SpearmanLine;
    private Vector3 ArcherLine;

    private void Start()
    {
        KnightLine = this.transform.Find("KnightLine").transform.localPosition;
        SpearmanLine = this.transform.Find("SpearmenLine").transform.localPosition;
        ArcherLine = this.transform.Find("ArcherLine").transform.localPosition;
    }

    public IList<Unit> GenerateArmy(int unitCount)
    {
        var result = new List<Unit>();

        for (int i = 0; i < unitCount; i++)
        {
            var gen = Random.Range(0, 3);
            result.Add(new Unit((UnitType)gen));
        }

        return result;
    }

    public IList<Unit> SmartGenerateArmy(int unitCount)
    {
        var result = new List<Unit>();

        int totalSpearmen = (SpearmanRatio*unitCount)/100;
        int totalArchers = (ArcherRatio * unitCount) / 100;
        int totalKnights = (KnightRatio * unitCount) / 100;

        for (int i = 0; i < totalArchers; i++)
        {
            result.Add(new Unit(UnitType.Archer));
        }

        for (int i = 0; i < totalKnights; i++)
        {
            result.Add(new Unit(UnitType.Knight));
        }

        for (int i = 0; i < totalSpearmen; i++)
        {
            result.Add(new Unit(UnitType.Footman));
        }

        return result;
    }

    public void SetUpArmyRandom(IList<Unit> army)
    {
        KnightLine = this.transform.Find("KnightLine").transform.localPosition;
        SpearmanLine = this.transform.Find("SpearmenLine").transform.localPosition;
        ArcherLine = this.transform.Find("ArcherLine").transform.localPosition;

        foreach (var u in army)
        {
            float yPos;

            switch (u.UnitType)
            {
                case UnitType.Footman:
                    var sp = Instantiate(Spearman, Vector3.zero, Quaternion.identity) as GameObject;
                    sp.transform.SetParent(this.transform);
                    sp.transform.localPosition = SpearmanLine;

                    yPos = Random.Range(-10f, 10f);
                    sp.transform.Translate(new Vector3(0f,yPos,0f));
                    sp.GetComponent<SpriteRenderer>().sortingOrder = CalculateZOrder(yPos);
                    break;
                case UnitType.Archer:
                    var ar = Instantiate(Archer, Vector3.zero, Quaternion.identity) as GameObject;
                    ar.transform.SetParent(this.transform);
                    ar.transform.localPosition = ArcherLine;

                    yPos = Random.Range(-10f, 10f);
                    ar.transform.Translate(new Vector3(0f, yPos, 0f));
                    ar.GetComponent<SpriteRenderer>().sortingOrder = CalculateZOrder(yPos);
                    break;
                case UnitType.Knight:
                    var kn = Instantiate(Knight, Vector3.zero, Quaternion.identity) as GameObject;
                    kn.transform.SetParent(this.transform);
                    kn.transform.localPosition = KnightLine;

                    yPos = Random.Range(-10f, 10f);
                    kn.transform.Translate(new Vector3(0f, yPos, 0f));
                    kn.GetComponent<SpriteRenderer>().sortingOrder = CalculateZOrder(yPos);
                    break;
            }
        }
    }

    public void SetUpArmySmart(IList<Unit> army)
    {
        var KnightMarker = this.transform.Find("KnightLine");
        var SpearmanMarker = this.transform.Find("SpearmenLine");
        var ArcherMarker = this.transform.Find("ArcherLine");

        var SpearmanOrder = SpearmanRanks;
        var KnightOrder = KnightRanks;
        var ArcherOrder = ArcherRanks;

        KnightMarker.Translate(new Vector3(0f, ((float) KnightRanks / 2) * unitSeparationVert, 0f));
        SpearmanMarker.Translate(new Vector3(0f, ((float)SpearmanRanks / 2) * unitSeparationVert, 0f));
        ArcherMarker.Translate(new Vector3(0f, ((float)ArcherRanks / 2) * unitSeparationVert, 0f));

        foreach (var u in army)
        {
            switch (u.UnitType)
            {
                case UnitType.Footman:
                    var sp = Instantiate(Spearman, Vector3.zero, Quaternion.identity) as GameObject;
                    sp.transform.SetParent(this.transform);
                    sp.transform.localPosition = SpearmanMarker.localPosition;
                    sp.GetComponent<SpriteRenderer>().sortingOrder = CalculateZOrder(SpearmanMarker.localPosition.y);
                    SpearmanOrder--;

                    if (SpearmanOrder >= 0)
                    {
                        SpearmanMarker.Translate(0f, -unitSeparationVert, 0f);
                    }
                    else
                    {
                        SpearmanMarker.Translate(-unitSeparationHoriz, ((float)SpearmanRanks) * unitSeparationVert, 0f);
                        SpearmanOrder = SpearmanRanks;
                    }
                    break;
                case UnitType.Archer:
                    var ar = Instantiate(Archer, Vector3.zero, Quaternion.identity) as GameObject;
                    ar.transform.SetParent(this.transform);
                    ar.transform.localPosition = ArcherMarker.localPosition;
                    ar.GetComponent<SpriteRenderer>().sortingOrder = CalculateZOrder(ArcherMarker.localPosition.y);
                    ArcherOrder--;

                    if (ArcherOrder >= 0)
                    {
                        ArcherMarker.Translate(0f, -unitSeparationVert, 0f);
                    }
                    else
                    {
                        ArcherMarker.Translate(-unitSeparationHoriz, ((float)ArcherRanks) * unitSeparationVert, 0f);
                        ArcherOrder = ArcherRanks;
                    }
                    break;
                case UnitType.Knight:
                    var kn = Instantiate(Knight, Vector3.zero, Quaternion.identity) as GameObject;
                    kn.transform.SetParent(this.transform);
                    kn.transform.localPosition = KnightMarker.localPosition;
                    kn.GetComponent<SpriteRenderer>().sortingOrder = CalculateZOrder(KnightMarker.localPosition.y);
                    KnightOrder--;
                    if (KnightOrder >= 0)
                    {
                        KnightMarker.Translate(0f, -unitSeparationVert, 0f);
                    }
                    else
                    {
                        KnightMarker.Translate(-unitSeparationHoriz, ((float)KnightRanks) * unitSeparationVert, 0f);
                        KnightOrder = KnightRanks;
                    }
                    break;
            }
        }
    }

    private int CalculateZOrder(float yPos)
    {
        return (int)((yPos*-1) + 30);
    }
}
