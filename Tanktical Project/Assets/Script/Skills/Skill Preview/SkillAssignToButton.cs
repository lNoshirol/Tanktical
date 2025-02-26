using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAssignToButton : MonoBehaviour
{
    public SkillsSandBox.Skill Skill;

    public void OnClick()
    {
        SkillSelectorManager.Instance.SetSelectedSkill(Skill);
    }
}
