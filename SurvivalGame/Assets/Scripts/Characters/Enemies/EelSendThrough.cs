using UnityEngine;

public class EelSendThrough : MonoBehaviour
{
    [SerializeField] private EelEnemy eel;

    public void SendMeleeAttack()
    {
        eel.MeleeAttack();
    }

    public void SendAOEAttack()
    {
        eel.AOEAttack();
    }

    public void SendDie()
    {
        eel.Die();
    }

    public void SendStopAttacking()
    {
        eel.StopAttacking();
    }
}
