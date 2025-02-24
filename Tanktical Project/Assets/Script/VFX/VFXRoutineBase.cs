using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class VFXSequenceBase : MonoBehaviour
{
    protected CancellationTokenSource _cts = new CancellationTokenSource();

    public delegate void OnHitCallback();
    public abstract UniTask PlaySequence(OnHitCallback callback);
    public abstract void StopSequence();   
}
