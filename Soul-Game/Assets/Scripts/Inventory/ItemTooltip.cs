using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.ItemName;
        ItemSlotText.text = item.EquipmentType.ToString();
         
        sb.Length = 0;
        AddStat(item.FriendshipBonus, "Friendship");
        AddStat(item.KnowledgeBonus, "Knowledge");
        AddStat(item.StrengthBonus, "Strength");

        AddStat(item.FriendshipPercentBonus, "Friendship", isPercent: true);
        AddStat(item.KnowledgePercentBonus, "Knowledge", isPercent: true);
        AddStat(item.StrengthPercentBonus, "Strength", isPercent: true);

        ItemStatsText.text = sb.ToString();
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if(value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if(isPercent)
            {
                sb.Append(value * 100);
                sb.Append("%");
            }else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            
            sb.Append(statName);

        }
    }
}
