using System;
using MessagePipe;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyHp;
    [SerializeField] private Text enemyHpText;
    private bool _isPlay;

    [Inject] private ISubscriber<PlayerData> PlayerData { get; set; }
    [Inject] private IPublisher<EnemyData> EnemyData { get; set; }

    private Animator _animator;
    private IDisposable _disposable;

    private void Awake()
    {
        enemyHpText.text = $"enemy hp:{enemyHp}";
        _animator = GetComponent<Animator>();
    }

    public void Start()
    {
        var d = DisposableBag.CreateBuilder();
        PlayerData.Subscribe(e =>
        {
            enemyHp -= e.AttackValue;
            _isPlay = e.IsLose;
            _animator.SetTrigger("IsDamage");
            enemyHpText.text = $"enemy hp:{enemyHp}";

            if (enemyHp > 0) return;
            PublishData(0,true);
            _animator.SetBool("IsLose",true);
        }).AddTo(d);

        _disposable = d.Build();

        Observable.Interval(TimeSpan.FromSeconds(5f))
            .Where(_=>!_isPlay)
            .Subscribe(_ =>
            {
                _animator.SetTrigger("IsAttack");
                PublishData(3,false);
            }).AddTo(this);
    }
    
    private void PublishData(int damage, bool isLose)
    {
        EnemyData.Publish(new EnemyData(){AttackValue = damage,IsLose = isLose});
    }

    private void OnDestroy()
    {
        _disposable?.Dispose();
    }
}