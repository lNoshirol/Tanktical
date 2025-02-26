using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FireVFX : VFXSequenceBase
{
    /// <inheritdoc cref="VFXSequenceBase"/>
    /// <param name="cible"> Position the fire will be spawned at </param>
    public override void Init(Vector3 cible, bool _)
    {
        transform.position = cible;
    }

    /// <inheritdoc cref="VFXSequenceBase"/>
    /// <remarks>
    /// FireVFX is a infinite looping VFX, with no finish finish, so you can't await it. <br/> 
    /// To clean it, you need to manually call StopSequence. <br/><br/>
    ///
    /// The callback is also never called, as PlaySequence is instantaneous. <br/>
    /// </remarks>
    public override UniTask PlaySequence(OnHitCallback callback)
    {
#if UNITY_EDITOR
        Debug.LogWarning("FireVFX is a infinite looping VFX, with no finish finish, so you can't await it.");
        Debug.LogWarning("To clean it, you need to manually call StopSequence");
        
        Debug.LogWarning("The callback is also never called, as PlaySequence is instantaneous");
#endif
        if (gameObject.scene.name != "Null")
        {
            gameObject.SetActive(true);
        }
        return UniTask.CompletedTask;

    }

    /// <inheritdoc cref="VFXSequenceBase"/>
    public override void StopSequence()
    {
        if (gameObject.scene.name != "Null")
        {
            this.gameObject.SetActive(false);
        }
    }
}
