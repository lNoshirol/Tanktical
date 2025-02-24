using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSkillRangePreview : MonoBehaviour
{
    public SkillsSandBox.Skill Skill;

    public void OnClick()
    {
        Debug.Log("SAC A MERDE");

        Skill.SkillSelected();
        SkillSelectorManager.Instance.SetSelectedSkill(Skill);
    }
}
