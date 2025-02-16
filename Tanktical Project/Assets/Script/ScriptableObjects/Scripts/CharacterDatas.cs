using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Character", fileName = "Character")]
public class CharacterDatas : ScriptableObject
{
    public int characterMaxLife;
    [MinMaxSlider(0, 20)]
    public Vector2Int range;
    public int baseAttaqueTDR;
    public int baseDamage;
    public int mobility;

    [Dropdown("_armorType")]
    public string armorType;
    private List<string> _armorType { get { return new List<string>() { "light", "medium", "heavy" }; } }

    [ShowAssetPreview(96, 96)]
    public Mesh tankModel;

}
