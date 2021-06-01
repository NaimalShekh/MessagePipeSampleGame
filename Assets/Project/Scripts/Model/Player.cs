using MessagePipe;
using UnityEngine;
using VContainer;

namespace Model
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int characterHp;
        [SerializeField] private int attackValue;
        [Inject] private IPublisher<AttackData> AttackData { get; set; }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AttackData.Publish(new AttackData(){Value = attackValue});
                Debug.Log($"プレイヤーの攻撃:{attackValue}");
            }
        }

    }
    
}
