using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LinkPlayersButton : MonoBehaviour
{
    [SerializeField] Characters _character;

    [SerializeField] List<GameObject> _buttonsList = new List<GameObject>();

    private async void Start()
    {
        await Task.Delay(50);

        _buttonsList[0].GetComponent<SkillAssignToButton>().Skill = _character.Skill1;
        _buttonsList[1].GetComponent<SkillAssignToButton>().Skill = _character.Skill2;
        //_buttonsList[2].GetComponent<SkillAssignToButton>().Skill = _character.Skill3;
        //_buttonsList[3].GetComponent<SkillAssignToButton>().Skill = _character.Skill4;

        foreach (GameObject button in _buttonsList)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = button.GetComponent<SkillAssignToButton>().Skill._skillName;
        }
    }
}
