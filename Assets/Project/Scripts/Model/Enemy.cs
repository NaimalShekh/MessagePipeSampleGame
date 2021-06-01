using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Model
{
    public class Enemy : MonoBehaviour,IStartable
    {
        [SerializeField] private int enemyHp;

        [Inject] private ISubscriber<AttackData> Damage { get; set; }

        private IDisposable _disposable;

        public void Start()
        {
            var d = DisposableBag.CreateBuilder();
            Damage.Subscribe(e =>
            {
                enemyHp -= e.Value;
                Debug.Log($"enemyHP:{enemyHp}"); 
            }).AddTo(d);

            _disposable = d.Build();
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}