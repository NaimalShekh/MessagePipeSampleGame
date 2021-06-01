using System;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Model
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int characterHp;
        [SerializeField] private int attackValue;
        [SerializeField] private Text playerHpText;
        [Inject] private ISubscriber<EnemyAttackData> PlayerDamage { get; set; }
        [Inject] private IPublisher<PlayerAttackData> AttackData { get; set; }
        private IDisposable _disposable;

        private void Awake()
        {
            playerHpText.text = $"player hp:{characterHp}";
        }

        private void Start()
        {
            var d = DisposableBag.CreateBuilder();
            PlayerDamage.Subscribe(e =>
            {
                characterHp -= e.Value;
                playerHpText.text = $"enemy hp:{characterHp}";
            }).AddTo(d);

            _disposable = d.Build();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("プレイヤーが攻撃");
                AttackData.Publish(new PlayerAttackData(){Value = attackValue});
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
    
}
