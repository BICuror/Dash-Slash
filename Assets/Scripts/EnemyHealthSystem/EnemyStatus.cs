using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyColorManager))]
[RequireComponent(typeof(MoveAgent))]

public sealed class EnemyStatus : MonoBehaviour, IShockable, IBurnable
{
    private EnemyColorManager _colorManager;

    private IDamagable _damagable;
    
    private MoveAgent _moveAgent;

    private ShockEffectManager _shockManager;

    private BurnEffectManager _burnManager;

    private readonly float _higlightDuration = 0.13f;

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();

        _moveAgent = GetComponent<MoveAgent>();

        _colorManager = GetComponent<EnemyColorManager>();

        _burnManager = GetComponent<BurnEffectManager>();

        _shockManager = GetComponent<ShockEffectManager>();
    }

    public bool IsShocked() => _shockManager.IsShocked();
    
    public void Shock(float duration) => _shockManager.Shock(duration);

    public bool IsOnFire() => _burnManager.IsOnFire();

    public void SetOnFire() 
    {
        _burnManager.SetOnFire();
    
        HighlightEnemy();
    }

    public void HighlightEnemy()
    {
        StartCoroutine(UnHighlightEnemy());

        _colorManager.SetMaterial(Main.colorManager.GetHighlightMaterial());

        _colorManager.SetColor(Main.colorManager.GetHighlightColor());

        StartCoroutine(UnHighlightEnemy());
    }

    private IEnumerator UnHighlightEnemy()
    {
        yield return new WaitForSeconds(_higlightDuration);
            
        if (IsOnFire()) _colorManager.SetColor(Main.colorManager.GetDefenceColor());
    
        else if (IsShocked()) _colorManager.SetMaterial(Main.colorManager.GetEnemyMaterial());

        else
        {
            _colorManager.SetMaterial(Main.colorManager.GetEnemyMaterial());

            _colorManager.SetColor(Main.colorManager.GetEnemyColor());
        }
    }
}
