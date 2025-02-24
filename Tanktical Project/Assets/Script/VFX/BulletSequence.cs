using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;
using UnityEngine.Serialization;

public class BulletSequence : VFXSequenceBase
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private VisualEffect _smoke;
    [SerializeField] private VisualEffect _fire;

    private Vector3 _endPosition;

    public void Init(Vector3 bulletOffset)
    {
        _endPosition = bulletOffset;
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
            bullet.transform.position = this.transform.position;
            
            bullet.gameObject.SetActive(true);

            _smoke.gameObject.SetActive(true);
            _smoke.Play();

            _fire.gameObject.SetActive(true);
            _fire.Play();

            bool isbulletFinished = false;

            bullet.transform.DOMove(_endPosition, 1.0f, false).SetEase(Ease.Linear).OnComplete(() =>
            {
                isbulletFinished = true;
                bullet.gameObject.SetActive(false);
                callback();
            });

            while (!isbulletFinished || _fire.HasAnySystemAwake() || _smoke.HasAnySystemAwake())
            {
                if (_cts.IsCancellationRequested) break;
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
            this.gameObject.SetActive(false);
        }
    }
}
