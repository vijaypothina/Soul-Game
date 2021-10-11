using UnityEngine;
using CharacterStats;

public enum EquipmentType
{
    Potion1,
    Potion2,
    Potion3,
    Accessory1,
    Accessory2,
    Accessory3,
    Bag,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public int FriendshipBonus;
    public int KnowledgeBonus;
    public int StrengthBonus;
    [Space]
    public float FriendshipPercentBonus;
    public float KnowledgePercentBonus;
    public float StrengthPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(Character c)
    {
        if(FriendshipBonus != 0)
            c.Friendship.AddModifier(new StatModifier(FriendshipBonus, StatModType.Flat, this));
        if (KnowledgeBonus != 0)
            c.Knowledge.AddModifier(new StatModifier(KnowledgeBonus, StatModType.Flat, this));
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));

        if (FriendshipPercentBonus != 0)
            c.Friendship.AddModifier(new StatModifier(FriendshipPercentBonus, StatModType.PercentMult, this));
        if (KnowledgePercentBonus != 0)
            c.Knowledge.AddModifier(new StatModifier(KnowledgePercentBonus, StatModType.PercentMult, this));
        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));


    }

    public void Unequip(Character c)
    {
        c.Friendship.RemoveAllModifiersFromSource(this);
        c.Knowledge.RemoveAllModifiersFromSource(this);
        c.Strength.RemoveAllModifiersFromSource(this);
    }


}
