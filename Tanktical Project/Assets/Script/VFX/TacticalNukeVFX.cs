using System.Collections;
using System.Collections.Generic;
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
    
    public void Init(Vector3 nukeFinalPosition, bool enemy)
    {
        _endPosition = nukeFinalPosition;
        _enemy = enemy;
        
        _camera = Camera.main;
    }

    public override async UniTask PlaySequence(OnHitCallback callback)
    {
        if (gameObject.scene.name != "Null")
        {
            if (_cts.IsCancellationRequested) return;
            
            this.gameObject.SetActive(true);
            
            _plane.transform.position = new Vector3(_endPosition.x - 10, transform.position.y + 10, _endPosition.z - 10);
            _plane.transform.rotation = Quaternion.Euler(0, 45, 0);
            _nuke.SetActive(false);
            _nuke.transform.position = new Vector3(_endPosition.x - 10, transform.position.y + 10, _endPosition.z - 10);

            bool planeFinished = false;
            bool bombDropped = false;
            
            _plane.transform.DOMove(new Vector3(_endPosition.x, transform.position.y+10, _endPosition.z), 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _nuke.transform.position = new Vector3(_endPosition.x, transform.position.y + 10, _endPosition.z);
                _nuke.SetActive(true);

                _nuke.transform.DOMove(_endPosition, 2f).SetEase(Ease.Linear).OnComplete(()=>
                {
                    bombDropped = true;
                    _nuke.SetActive(false);
                });
                
                _plane.transform.DOMove(new Vector3(_endPosition.x + 10, transform.position.y + 10, _endPosition.z + 10), 2f).SetEase(Ease.Linear).OnComplete(()=>planeFinished=true);
            });

            while (!planeFinished || !bombDropped)
            {
                if (_cts.IsCancellationRequested) break;
                await UniTask.DelayFrame(1);
            }
            
            this.gameObject.SetActive(false);
        }
    }

    public override void StopSequence()
    {
        
    }
}
