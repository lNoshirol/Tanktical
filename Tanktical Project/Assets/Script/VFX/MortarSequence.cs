using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class MortarSequence : VFXSequenceBase
{
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private VisualEffect _smoke;
    [SerializeField] private VisualEffect _fire;

    private Vector3 _endPosition;
    
    /// <inheritdoc cref="VFXSequenceBase"/>
    /// <param name="bulletOffset"> Where the final position of the cannon ball will be, where to shoot </param>
    /// <remarks> The spawn point of the bullet is the initial position of this GameObject. </remarks>
    public override void Init(Vector3 cannonBallOffset, bool _)
    {
        _endPosition = cannonBallOffset;
    }

    public override async UniTask PlaySequence(OnHitCallback callback)
    {
        if (gameObject.scene.name != "Null")
        {
            this.gameObject.SetActive(true);
            
            if (_cts.IsCancellationRequested)
            {
                return;
            }

            _smoke.transform.position = this.transform.position;
            _fire.transform.position = this.transform.position;
            cannonBall.transform.position = this.transform.position;
            
            cannonBall.gameObject.SetActive(true);

            _smoke.gameObject.SetActive(true);
            _smoke.Play();

            _fire.gameObject.SetActive(true);
            _fire.Play();

            bool isCannonBallFinished = false;

            cannonBall.transform.DOLocalJump(_endPosition, 1.5f, 1, 2.0f, false).SetEase(Ease.Linear).OnComplete(() =>
            {
                isCannonBallFinished = true;
                cannonBall.gameObject.SetActive(false);
                callback();
            });

            while (!isCannonBallFinished || _fire.HasAnySystemAwake() || _smoke.HasAnySystemAwake())
            {
                if (_cts.IsCancellationRequested) return;
                await UniTask.DelayFrame(1);
            }

            this.gameObject.SetActive(false);
        }
    }

    public override void StopSequence()
    {
        _cts.Cancel();

        if(gameObject.scene.name != "Null")
        {
            this.gameObject.SetActive(true);
        }
    }
}
