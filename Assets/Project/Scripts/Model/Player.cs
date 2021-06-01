using System;
using MessagePipe;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Model
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int playerHp;
        [SerializeField] private int attackValue;
        [SerializeField] private Text playerHpText;
        [Inject] private ISubscriber<EnemyData> EnemyData { get; set; }
        [Inject] private IPublisher<PlayerData> PlayerData { get; set; }
        private IDisposable _disposable;

        private Animator _animator;
        private bool isPlay;

        private void Awake()
        {
            playerHpText.text = $"player hp:{playerHp}";
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            var d = DisposableBag.CreateBuilder();
            EnemyData.Subscribe(e =>
            {
                playerHp -= e.AttackValue;
                isPlay = e.IsLose;
                _animator.SetTrigger("IsDamage");
                playerHpText.text = $"enemy hp:{playerHp}";

                if (playerHp <= 0)
                {
                    _animator.SetBool("IsLose",true);
                    PublishData(0,true);
                }
            }).AddTo(d);

            _disposable = d.Build();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) && !isPlay)
            {
                Debug.Log("プレイヤーが攻撃");
                _animator.SetTrigger("IsAttack");
                PublishData(3,false);
            }
        }

        private void PublishData(int value, bool isLose)
        {
            PlayerData.Publish(new PlayerData(){AttackValue = value,IsLose = isLose});
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
    
}
