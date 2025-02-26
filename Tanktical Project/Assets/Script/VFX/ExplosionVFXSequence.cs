using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class ExplosionVFXSequence : VFXSequenceBase
{
    VisualEffect _visualEffect;
    
    public override void Init(Vector3 cible, bool _)
    {
        transform.position = cible;
        _visualEffect = GetComponent<VisualEffect>();
        _cts = new CancellationTokenSource();
    }

    /// <summary>
    /// FireVFX is a infinite looping VFX, with no finish finish, so you can't await it. <br/> 
    /// To clean it, you need to manually call StopSequence. <br/><br/>
    ///
    /// The callback is also never called, as PlaySequence is instantaneous. <br/>
    /// </summary>
    public async override UniTask PlaySequence(OnHitCallback callback)
    {
#if UNITY_EDITOR
        Debug.LogWarning("The callback is never called, as PlaySequence is instantaneous");
#endif

        gameObject.SetActive(true);
        
        await UniTask.DelayFrame(1);

        do
        {
            if (_cts.IsCancellationRequested) break;
            await UniTask.DelayFrame(1);
        } while (_visualEffect.HasAnySystemAwake());

        _visualEffect.Stop();
        gameObject.SetActive(false);

    }

    public override void StopSequence()
    {
        _cts.Cancel();
        if (gameObject.scene.name != "Null")
        {
            this.gameObject.SetActive(false);
        }
    }
}
