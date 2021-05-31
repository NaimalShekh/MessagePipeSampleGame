using Project.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View;
using CharacterController = Model.CharacterController;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private InputController inputController;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(characterController);
        builder.RegisterComponent(inputController);
        builder.RegisterEntryPoint<Presenter>(Lifetime.Singleton);
    }
}
