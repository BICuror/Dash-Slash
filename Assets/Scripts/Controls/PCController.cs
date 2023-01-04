using UnityEngine;

public sealed class PCController : PlayerController
{
    [Header("PCMovmentScheme")]

    [SerializeField] private Camera _camera;

    private PCControls _pcControls;
    
    private PlayerAbility _playerAbility;

    public override void SetActivePlayer(bool state)
    {
        Main.droneContainer.gameObject.SetActive(state);
        
        Main.playerTransform.gameObject.SetActive(state);
    }

    public override Vector3 GetMoveDirection()
    {
        Vector3 moveDirection;

        moveDirection = (_camera.ScreenToWorldPoint(_pcControls.PC.PointerPosition.ReadValue<Vector2>()) - transform.position);
        
        if (moveDirection.magnitude > 1f) moveDirection = moveDirection.normalized;

        return moveDirection;
    }

    private void Awake()
    {
        _playerAbility = GetComponent<PlayerAbility>();

        _pcControls = new PCControls();

        _pcControls.Enable();

        _pcControls.PC.ActivateAbility.started += _ => TryActivatingAbility();

        _pcControls.PC.PauseGame.started += _ => PauseGame();
    }

    private void OnDestroy() => _pcControls.Disable();

    private void PauseGame()
    {
        Main.pause.OpenPauseMenu();
    }

    private void TryActivatingAbility()
    {
        if (Main.arenaManager.ArenaIsActive() == true) 
        {
            _playerAbility.ActivateAbility(_camera.ScreenToWorldPoint(_pcControls.PC.PointerPosition.ReadValue<Vector2>()) - Main.playerTransform.position);
        }
    }
}
