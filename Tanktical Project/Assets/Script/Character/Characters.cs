using NaughtyAttributes;
using SkillsSandBox;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Characters : MonoBehaviour
{
    public CharacterDatas characterType;

    #region Character stats

    [SerializeField] private int _characterMaxLife;
    [SerializeField] private int _characterLife;
    [InfoBox("Le x représente la porté minimal, le y la porté maximal")]
    [SerializeField] private Vector2Int _range;
    [SerializeField] private int _baseAttaqueTDR;
    [SerializeField] private int _baseDamage;
    [SerializeField] private int _mobility;

    [SerializeField] private string _armorType;

    [ShowAssetPreview(96, 96)]
    [SerializeField] private Mesh _tankModel;

    #endregion

    #region Character Skills

    [Dropdown("SkillsList")]
    public string FirstSkill;
    
    [Dropdown("SkillsList")]
    public string SecondSkill;

    public SkillsSandBox.Skill Skill1;
    public SkillsSandBox.Skill Skill2;
    
    private List<string> SkillsList { get { return new List<string>() { "Basic Attack 1" ,"APFSDS 2"}; } }

    private List<SkillsSandBox.Skill> _skillsObjectList = new();

    #endregion

    #region Private Functions

    private void Awake()
    {
        Apply();

        
    }

    private void Start()
    {
        _skillsObjectList.Add(new BasicAttack("Basic Attack", this, "ennemy"));

        /*char test1 = FirstSkill[FirstSkill.Length - 1];
        char test2 = SecondSkill[SecondSkill.Length - 1];

        Debug.Log((int)test1 - 49);
        Debug.Log((int)test2 - 49);*/

        Skill1 = _skillsObjectList[(int)FirstSkill[FirstSkill.Length - 1] - 49];
        //Skill2 = _skillsObjectList[SecondSkill[SecondSkill.Length - 2] - 1];
    }

    private void Death()
    {
        

        Characters thisGameobject;
        TryGetComponent(out thisGameobject);

        thisGameobject.enabled = false;

        //voir pour modification du model 3d, genre un truc cramé par ex
    }

    #endregion

    #region Public Functions

    public void TakeDamage(int damage)
    {
        _characterLife = Mathf.Clamp(0, _characterMaxLife, damage);

        if (_characterLife <= 0)
        {
            Death();
        }
    }

    public void Heal(int heal)
    {
        _characterLife = Mathf.Clamp(0, _characterMaxLife, heal);
    }

    #endregion



    [Button("Apply stats")]
    private void Apply()
    {
        _characterMaxLife = characterType.characterMaxLife;
        _characterLife = _characterMaxLife;
        _range = characterType.range;
        _baseAttaqueTDR = characterType.baseAttaqueTDR;
        _baseDamage = characterType.baseDamage;
        _mobility = characterType.mobility;

        _armorType = characterType.armorType;

        if (characterType.tankModel != null)
        {
            _tankModel = characterType.tankModel;
        }
    }
}
