using UnityEngine;

public class MainSetuper : MonoBehaviour
{
    private void Awake()
    {
        Main.arenaManager = FindObjectOfType<ArenaManager>();

        Main.droneSelector = FindObjectOfType<Selector>();

        Main.droneContainer = FindObjectOfType<DroneContainer>();

        Main.playerHealth = FindObjectOfType<PlayerHealth>();

        Main.playerMovment = FindObjectOfType<PlayerMovment>();

        Main.roomSettings = FindObjectOfType<RoomSettings>();

        Main.enemyList = FindObjectOfType<EnemyList>();

        Main.enemySpawner = FindObjectOfType<EnemySpawner>();

        Main.trapSpawner = FindObjectOfType<TrapSpawner>();

        Main.timeManager = FindObjectOfType<TimeManager>();

        Main.variableJoystick = FindObjectOfType<VariableJoystick>();

        Main.playerAbility = FindObjectOfType<PlayerAbility>();

        Main.cameraEffects = FindObjectOfType<CameraEffects>();

        Main.droneInventory = FindObjectOfType<DroneInventory>();

        Main.inventoryNavigation = FindObjectOfType<InventoryNavigation>();

        Main.uiStateManager = FindObjectOfType<UIStateManager>();

        Main.perkInventory = FindObjectOfType<PerkInventory>();

        Main.curseManager = FindObjectOfType<CurseManager>();

        Main.colorManager = FindObjectOfType<ColorManager>();

        Main.playerController = FindObjectOfType<PlayerController>();

        Main.combatStats = FindObjectOfType<CombatStats>();

        Main.pause = FindObjectOfType<Pause>();

        Main.s_defeatMenu = FindObjectOfType<DefeatMenu>();

        Main.s_roundTimeManager = FindObjectOfType<RoundTimeManager>();

        Main.s_selectionStorage = FindObjectOfType<SelectionStorage>();

        Main.s_bossHealthBar = FindObjectOfType<BossHealthBar>();

        Main.playerTransform = Main.playerMovment?.transform;

        Destroy(gameObject);
    }
}
