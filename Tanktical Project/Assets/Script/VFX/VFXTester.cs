using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct VFXTestInfo
{
    public VFXSequenceBase Sequence;
    public Vector3 Param1;
    public bool Param2;
}

public class VFXTester : MonoBehaviour
{
    [field: SerializeField] public VFXTestInfo[] TestInfos { get; private set; }

    public async Task OnTest(int index = -1)
    {
        if (index != -1)
        {
            await TestOneSequence(index);
        }
        else
        {
            for (int i = 0; i < TestInfos.Length; i++)
            {
                await TestOneSequence(i);
            }
        }
    }
    
    public async Task OnCancel(int index = -1)
    {
        if (index != -1)
        {
            CancelOneSequence(index);
        }
        else
        {
            for (int i = 0; i < TestInfos.Length; i++)
            {
                CancelOneSequence(i);
            }
        }
    }

    private async UniTask TestOneSequence(int index)
    {
        VFXTestInfo testInfo = TestInfos[index];
        testInfo.Sequence.Init(testInfo.Param1, testInfo.Param2);
        await testInfo.Sequence.PlaySequence(() =>
        {
            Debug.Log("Sequence : " + testInfo.Sequence.GetType().Name + " : Has hit its target");
        });
            
        Debug.Log("Sequence : " + testInfo.Sequence.GetType().Name + " : Finished");
    }
    
    private void CancelOneSequence(int index)
    {
        VFXTestInfo testInfo = TestInfos[index];
        testInfo.Sequence.StopSequence();
            
        Debug.Log("Sequence : " + testInfo.Sequence.GetType().Name + " : Cancelled");
    }
}
