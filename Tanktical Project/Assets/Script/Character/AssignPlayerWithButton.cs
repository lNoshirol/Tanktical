using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AssignPlayerWithButton : MonoBehaviour
{
    [SerializeField] Characters character;

    [SerializeField] private StartSkillRangePreview _startSkillRangePreview;

    private SkillsSandBox.Skill _skillToPreview;

    [SerializeField] private int idSkill;
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    [SerializeField] GameObject skillButton1;
    [SerializeField] GameObject skillButton2;
    [SerializeField] GameObject skillButton3;
    [SerializeField] GameObject skillButton4;


    [SerializeField] StartSkillRangePreview SkillRangePreviewManager;

    private async void Start()
    {
        await Task.Delay(50);

        if (character.Skill1 != null)
        {
            skillButton1.GetComponentInChildren<TextMeshProUGUI>().text = character.Skill1.skillName;
            SkillRangePreviewManager.GetComponent<StartSkillRangePreview>().Skill = character.Skill1;
        }

        if (character.Skill2 != null)
        {
            skillButton2.GetComponentInChildren<TextMeshProUGUI>().text = character.Skill2.skillName;
            SkillRangePreviewManager.GetComponent<StartSkillRangePreview>().Skill = character.Skill1;
        }

        if (character.Skill1 != null)
        {
            skillButton1.GetComponentInChildren<TextMeshProUGUI>().text = character.Skill1.skillName;
            SkillRangePreviewManager.GetComponent<StartSkillRangePreview>().Skill = character.Skill1;
        }

        if (character.Skill2 != null)
        {
            skillButton2.GetComponentInChildren<TextMeshProUGUI>().text = character.Skill2.skillName;
            SkillRangePreviewManager.GetComponent<StartSkillRangePreview>().Skill = character.Skill1;
        }
    }
}
