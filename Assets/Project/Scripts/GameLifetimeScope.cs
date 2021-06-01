using MessagePipe;
using Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private InputController inputController;
    protected override void Configure(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<AttackData>(options);

    }
}
