public interface IInteractable
{
    string UseName { get; }
    string InteractionType { get; }
    float HoldTime { get; }
    void Interact();
}
