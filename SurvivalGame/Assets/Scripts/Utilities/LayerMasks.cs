//Dit script kan je aanroepen voor als je een tag nodig hebt. Hiermee kan je geen typefouten maken en heb je alle tags netjes op een rijtje.
//Bijvoorbeeld; Tags.Player

using UnityEngine;

public class LayerMasks
{
	public static LayerMask Player => LayerMask.NameToLayer("Player");
	public static LayerMask ItemDrop => LayerMask.NameToLayer("ItemDrop");
	public static LayerMask Enemy => LayerMask.NameToLayer("Enemy");
}