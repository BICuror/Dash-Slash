using UnityEngine;

public sealed class TutorialLaser : MonoBehaviour
{
    public void HurtPlayer() => Main.playerHealth.TryToDealDamage(null);
}
