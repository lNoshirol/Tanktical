using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectorManager : MonoBehaviour
{
    public static SkillSelectorManager Instance;

    public SkillsSandBox.Skill LastSkillSelected {  get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (LastSkillSelected != null)
        {
            LastSkillSelected.SkillSelected();
        }
    }

    public void SetSelectedSkill(SkillsSandBox.Skill skill)
    {
        LastSkillSelected = skill;
    }

    public void UseAttack(GameObject target)
    {
        float distance = Vector3.Distance(LastSkillSelected._skillOwner.transform.position, target.transform.position);

        if (LastSkillSelected!=null && distance >= LastSkillSelected.skillRange.x && distance <= LastSkillSelected.skillRange.y)
        {
            LastSkillSelected.Use(target);
        }
    }
}