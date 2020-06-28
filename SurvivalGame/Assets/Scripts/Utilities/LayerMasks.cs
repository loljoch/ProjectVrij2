//Dit script kan je aanroepen voor als je een tag nodig hebt. Hiermee kan je geen typefouten maken en heb je alle tags netjes op een rijtje.
//Bijvoorbeeld; Tags.Player

using UnityEngine;

public class LayerMasks
{
	public static LayerMask Player => LayerMask.GetMask("Player");
	public static LayerMask ItemDrop => LayerMask.GetMask("ItemDrop");
	public static LayerMask AllInteractable => LayerMask.GetMask("Interactable", "InteractableWalkthrough");
	public static LayerMask Enemy => LayerMask.GetMask("Enemy");

	public static LayerMask PhysicsEnemy => LayerMask.NameToLayer("Enemy");
	public static LayerMask PhysicsInteractable => LayerMask.NameToLayer("Interactable");
}