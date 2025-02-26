using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;


public abstract class VFXSequenceBase : MonoBehaviour
{
    protected CancellationTokenSource _cts = new CancellationTokenSource();

    public delegate void OnHitCallback();
    
    /// <summary>
    /// The simple initialization method, prepare the object for playing the sequence, must be called before PlaySequence
    /// </summary>
    /// <example>
    /// The init function is simply called once all the informatiosn needed are pulled <br/>
    /// Refers to the child documentation for needed information. <br/>
    /// This exemple assume the VFX is launched at a target,
    /// and need to know whether or not the one calling the vfx is the enemy or not
    /// <code>
    ///     public class TankVFXPlayer : MonoBehaviour {
    ///         [SerializeField] private VFXSequenceBase _vfx;
    ///         [SerializeField] private GameObject _target;
    ///         public void Start(){
    ///             _vfx.Init(_target.transform.position, false);
    ///         }
    ///     }
    /// </code>
    /// </example>
    public abstract void Init(Vector3 cible, bool fromTheEnemy);
    
    /// <summary>
    /// The main method, that will launch the VFX sequence, then return an UniTask, that can be awaited or remembered. <br/>
    /// Can be async or just return UniTask.CompletedTask
    /// </summary>
    /// <param name="callback"> The callback that will be executed once the VFX hit the target, if applicable, read the child class "Remarks" section for more details </param>
    /// <example>
    /// The init function should be called already, if not, it won't execute properly <br/>
    /// Check the child function signature and remarks to know if it can be awaited or nto
    /// <code>
    ///     public class TankVFXPlayer : MonoBehaviour {
    ///         [SerializeField] private VFXSequenceBase _vfx;
    ///         [SerializeField] private GameObject _target;
    ///         public void Start(){
    ///             _vfx.Init(_target.transform.position, false);
    ///             await _vfx.PlaySequence(() => Debug.Log("Target Hit"))
    ///             Debug.Log("The VFX is completely finished, and object have been put back into default configuration, ready to be used again");
    ///         }
    ///     }
    /// </code>
    /// </example>
    public abstract UniTask PlaySequence(OnHitCallback callback);
    
    /// <summary>
    /// Cancel the sequence, if running, and NOT the prefab, but an instance
    /// </summary>
    public abstract void StopSequence();   
}
