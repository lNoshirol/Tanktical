using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class TacticalNukeVFX : VFXSequenceBase
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private GameObject _nuke;
    
    private Vector3 _endPosition;
    private bool _enemy;
    
    private Camera _camera;
    
    /// <inheritdoc cref="VFXSequenceBase"/>
    /// <param name="bulletOffset"> Where the bomb will be dropped </param>
    /// <param name="enemy"> The Enemy parameter calculate if the plane will go from left to right or right to left. </param>
    public override void Init(Vector3 nukeFinalPosition, bool enemy)
    {
        _cts = new CancellationTokenSource();
        
        _endPosition = nukeFinalPosition;
        _enemy = enemy;
        
        _camera = Camera.main;
    }

    public override async UniTask PlaySequence(OnHitCallback callback)
    {
        if (gameObject.scene.name != "Null")
        {
            this.gameObject.SetActive(true);
            this.transform.position = new Vector3(0, 0, 0);
            if (_cts.IsCancellationRequested) return;
            
            _plane.transform.position = new Vector3(_endPosition.x - (_enemy ? -10 : 10), _endPosition.y + 10, _endPosition.z - (_enemy ? -10 : 10));
            _plane.transform.rotation = Quaternion.Euler(0, 45, 0);
            _nuke.SetActive(false);
            _nuke.transform.position = new Vector3(_endPosition.x - (_enemy ? -10 : 10), _endPosition.y + 10, _endPosition.z - (_enemy ? -10 : 10));

            bool planeFinished = false;
            bool bombDropped = false;
            
            _plane.transform.DOMove(new Vector3(_endPosition.x, _endPosition.y + 10, _endPosition.z), 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _nuke.transform.position = new Vector3(_endPosition.x, _endPosition.y + 10, _endPosition.z);
                _nuke.SetActive(true);

                _nuke.transform.DOMove(_endPosition, 2f).SetEase(Ease.Linear).OnComplete(()=>
                {
                    bombDropped = true;
                    if(callback != null) callback();
                    _nuke.SetActive(false);
                });
                
                _plane.transform.DOMove(new Vector3(_endPosition.x + (_enemy ? -10 : 10), _endPosition.y + 10, _endPosition.z + (_enemy ? -10 : 10)), 2f).SetEase(Ease.Linear).OnComplete(()=>planeFinished=true);
            });

            do {
                if (_cts.IsCancellationRequested) break;
                await UniTask.DelayFrame(1);
            } while (!planeFinished || !bombDropped);
            
            _plane.transform.DOKill();
            _nuke.transform.DOKill();
            
            this.gameObject.SetActive(false);
        }
    }

    public override void StopSequence()
    {
        _cts.Cancel();
        if (gameObject.scene.name != "Null")
        {
            _nuke.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
