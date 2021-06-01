using MessagePipe;
using VContainer;
using VContainer.Unity;


public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<PlayerData>(options);
        builder.RegisterMessageBroker<EnemyData>(options);

    }
}
