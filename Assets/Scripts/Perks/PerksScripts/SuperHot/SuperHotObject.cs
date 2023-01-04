using UnityEngine;

public sealed class SuperHotObject : MonoBehaviour
{
    private void Awake()
    {
        Main.arenaManager.StartArena += ActivatePerk;

        Main.arenaManager.StopArena += StopPerk;
    }

    public void ActivatePerk()
    {
        gameObject.SetActive(true);
    }

    private void Update() 
    {
        float magnitude = Main.playerController.GetMoveDirection().magnitude;

        Time.timeScale = (0.4f + (magnitude * 0.6f)) * Main.timeManager.DefaultTimeScale;
    }

    private void StopPerk()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}