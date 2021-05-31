using System;
using InputAsRx;
using UniRx;
using UnityEngine;

namespace View
{
    public class InputController : MonoBehaviour
    {
        private readonly Subject<Unit> _attackSubject = new Subject<Unit>();

        public IObservable<Unit> AttackObserve() => _attackSubject;
        // Start is called before the first frame update
        private void Awake()
        {
            InputAsObservable.GetKeyDown(KeyCode.A)
                .Subscribe(_ =>
                {
                    _attackSubject.OnNext(Unit.Default);
                }).AddTo(this);
        }
    }
    
}
