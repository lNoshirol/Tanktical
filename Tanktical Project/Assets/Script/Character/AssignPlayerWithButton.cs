using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class AssignPlayerWithButton : MonoBehaviour
{
    [SerializeField] Characters character;

    [SerializeField] GameObject skillButton1;
    [SerializeField] GameObject skillButton2;
    [SerializeField] GameObject skillButton3;
    [SerializeField] GameObject skillButton4;


    public GameObject test;

    private async void Start()
    {
        await Task.Delay(50);

        if (character.Skill1 != null)
        {
            skillButton1.GetComponentInChildren<TextMeshProUGUI>().text = character.Skill1._skillName;
            Debug.Log(character.Skill1._skillName);
        }

        if (character.Skill2 != null)
        {
            skillButton2.GetComponentInChildren<TextMeshProUGUI>().text = character.Skill2._skillName;
        }

        test.GetComponent<StartSkillRangePreview>().Skill = character.Skill1;
    }
}
