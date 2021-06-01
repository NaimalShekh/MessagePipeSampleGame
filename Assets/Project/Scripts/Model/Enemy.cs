using System;
using MessagePipe;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Model
{
    public class Enemy : MonoBehaviour,IStartable
    {
        [SerializeField] private int enemyHp;
        [SerializeField] private Text enemyHpText;

        [Inject] private ISubscriber<PlayerAttackData> Damage { get; set; }
        [Inject] private IPublisher<EnemyAttackData> EnemyAttack { get; set; }

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
            Damage.Subscribe(e =>
            {
                enemyHp -= e.Value;
                enemyHpText.text = $"enemy hp:{enemyHp}";
            }).AddTo(d);

            _disposable = d.Build();

            Observable.Interval(TimeSpan.FromSeconds(5f))
                .Subscribe(_ =>
                {
                    Debug.Log("敵が攻撃");
                    _animator.SetTrigger("IsAttack");
                    EnemyAttack.Publish(new EnemyAttackData(){Value = 2});
                }).AddTo(this);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}