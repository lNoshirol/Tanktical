using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamStateManager : MonoBehaviour
{
    [field: SerializeField]
    public Material CharacterOutline;

    // test with material
    [field: SerializeField]
    public MeshRenderer FirstCharacter;

    [field: SerializeField]
    public MeshRenderer SecondCharacter;

    [field: SerializeField]
    public MeshRenderer ThirdCharacter;

    [field: SerializeField]
    public MeshRenderer FourthCharacter;

    // actually used
    [field: SerializeField]
    public GameObject FirstCharacterUI;

    [field: SerializeField]
    public GameObject SecondCharacterUI;

    [field: SerializeField]
    public GameObject ThirdCharacterUI;

    [field: SerializeField]
    public GameObject FourthCharacterUI;

    public FirstTeammateTurnState FirstMateState = new();
    public SecTeammateTurnState SecondMateState = new();
    public ThirdTeammateTurnState ThirdMateState = new();
    public FourthTeammateTurnState FourthMateState = new();

    // TEMPORARY
    [SerializeField] private List<GameObject> FirstButtons = new();
    [SerializeField] private List<GameObject> SecButtons = new(); 
    [SerializeField] private List<GameObject> ThirdButtons = new();
    [SerializeField] private List<GameObject> FourthButtons = new();

    public TeamTurnBaseState CurrentState { get; private set; }

    private void Start()
    {
        foreach(GameObject button in FirstButtons) 
        {
            button.GetComponent<Button>().onClick.AddListener(() => SwitchState(SecondMateState));
        }
        foreach (GameObject button in SecButtons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => SwitchState(ThirdMateState));
        }
        foreach (GameObject button in ThirdButtons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => SwitchState(FourthMateState));
        }
        foreach (GameObject button in FourthButtons)
        {
            button.GetComponent<Button>().onClick.AddListener(() => SwitchState(FirstMateState));
        }

        CurrentState = FirstMateState;
        SwitchState(FirstMateState);
    }

    public void SwitchState(TeamTurnBaseState newState)
    {
        // Exit current state, then enter the new one
        CurrentState.ExitState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
}
