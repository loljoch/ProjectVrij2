public interface IInteractable
{
    string UseName { get; }
    string InteractionType { get; }
    float HoldTime { get; }
    bool HighLighted { set; }
    void Interact();
}
