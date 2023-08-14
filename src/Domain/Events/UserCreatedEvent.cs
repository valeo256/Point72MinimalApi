namespace Domain.Events;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(UserExample item)
    {
        Item = item;
    }

    public UserExample Item { get; }
}
