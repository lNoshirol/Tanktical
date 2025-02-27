using SkillsSandBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAssignToButton : MonoBehaviour
{
    public ActiveSkill Skill;

    public void OnClick()
    {
        foreach (GameObject cell in GridHandler.Instance.CellsList)
        {
            cell.GetComponent<SpriteRenderer>().color = GridHandler.Instance.BlankCellColor;
        }

        SkillSelectorManager.Instance.SetSelectedSkill(Skill);
    }
}
