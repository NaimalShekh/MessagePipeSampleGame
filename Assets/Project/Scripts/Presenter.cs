using System;
using UniRx;
using UnityEngine;
using VContainer.Unity;
using View;
using CharacterController = Model.CharacterController;

namespace Project.Scripts
{
    public class Presenter : IStartable,IDisposable
    {
        private CharacterController _characterController;
        private readonly InputController _inputController;

        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Presenter(CharacterController characterController,InputController inputController)
        {
            _characterController = characterController;
            _inputController = inputController;
        }
        public void Start()
        {
            _inputController.AttackObserve().Subscribe(_ =>
            {
                Debug.Log("攻撃キーを押した");
            }).AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}