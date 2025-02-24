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
        LastSkillSelected.SkillSelected();
    }

    public void SetSelectedSkill(SkillsSandBox.Skill skill)
    {
        LastSkillSelected = skill;
    }
}
