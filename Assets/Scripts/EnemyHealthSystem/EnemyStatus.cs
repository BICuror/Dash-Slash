using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyStatus : MonoBehaviour, IShockable, IBurnable
{
    private EnemyColorManager _colorManager;

    private IDamagable _damagable;
    
    private MoveAgent _moveAgent;

    private bool _isShoked;

    private bool _isOnFire;

    [Header("HighlightSettings")]

    [SerializeField] private Material highlightMaterial;
    
    [SerializeField] private Material defaultMaterial;

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();

        _moveAgent = GetComponent<MoveAgent>();

        _colorManager = GetComponent<EnemyColorManager>();
    }

    public bool IsShocked() => _isShoked;

    public bool IsOnFire() => _isOnFire;

    public void Shock(float duration)
    {
        StopAllCoroutines();

        _isShoked = true;

        _moveAgent.Stun(duration);
        
        float elapsedTime = 0f;

        _colorManager.SetMaterial(highlightMaterial);

        StartCoroutine(DeShockEnemy(duration));
        
        IEnumerator DeShockEnemy(float duration)
        {
            yield return new WaitForFixedUpdate();
            
            if (duration > elapsedTime)
            {
                elapsedTime += Time.deltaTime;

                _colorManager.SetColor(Color.Lerp(Main.colorManager.GetAreaColor(), defaultMaterial.color, elapsedTime / duration));

                StartCoroutine(DeShockEnemy(duration));
            }
            else
            {
                _isShoked = false;

                _colorManager.SetMaterial(defaultMaterial);

                _colorManager.SetColor(Color.white);
            }
        }
    }

    public void SetOnFire()
    {
        if (_isOnFire == false)
        {
            _isOnFire = true;

            _damagable.GetPercentHurt(0.1f);

            StartCoroutine(Burn());
        }
    }

    IEnumerator Burn()
    {
        yield return new WaitForSeconds(1f);

        _damagable.GetPercentHurt(0.2f);

        StartCoroutine(Burn());
    }

    public void HighlightEnemy()
    {
        StartCoroutine(UnHighlightEnemy());

        _colorManager.SetMaterial(highlightMaterial);

        if (_isShoked == false) _colorManager.SetColor(highlightMaterial.color);

        StartCoroutine(UnHighlightEnemy());
    }
    
    private IEnumerator UnHighlightEnemy()
    {
        yield return new WaitForSeconds(0.13f);
            
        if (_isOnFire == true) _colorManager.SetColor(Main.colorManager.GetDefenceColor());
    
        else if (_isShoked == false) _colorManager.SetMaterial(defaultMaterial);
    }
}
