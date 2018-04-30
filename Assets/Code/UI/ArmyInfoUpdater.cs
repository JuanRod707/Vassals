using UnityEngine;

public class ArmyInfoUpdater : MonoBehaviour
{
    public LabelWrite lblGeneral;
    public LabelWrite lblStrength;
    public LabelWrite lblCavalry;
    public LabelWrite lblArchers;
    public LabelWrite lblInfantry;

    public void PopulateFields(ArmyInfo data)
    {
        this.gameObject.SetActive(true);
        lblGeneral.WriteLabel(data.General);
        lblStrength.WriteLabel(data.Strength);
        lblCavalry.WriteLabel(data.Knights);
        lblArchers.WriteLabel(data.Archers);
        lblInfantry.WriteLabel(data.Spearmen);
    }
}
