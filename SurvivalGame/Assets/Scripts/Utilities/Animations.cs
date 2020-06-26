//Common animation names

using System;

public class Animations
{
    public static string Idle = "Idle";
    public static string Walking = "Walking";
    public static string Attack = "Attack";
    public static string TakeDamage = "TakeDamage";
    public static string Death = "Death";

    public static string GetWeaponAnimation(WeaponAnimation animation)
    {
        return Enum.GetName(typeof(WeaponAnimation), animation);
    }

    public enum WeaponAnimation
    {
        Slash = 0,
        Stab,
        Punch,
        Smash,
        Block,
        SpearThrow,
        Crossbow
    }
}
