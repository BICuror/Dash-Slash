using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ShockEffectManager : MonoBehaviour
{
    private bool _isShoked;

    private float _elapsedTime;

    private EnemyColorManager _colorManager;
    
    private MoveAgent _moveAgent;

    public void Start()
    {
        _moveAgent = GetComponent<MoveAgent>();

        _colorManager = GetComponent<EnemyColorManager>();
    }
    
    public bool IsShocked() => _isShoked;
    
    public void Shock(float duration)
    {
        StopAllCoroutines();

        _isShoked = true;

        _moveAgent.Stun(duration);
        
        _elapsedTime = 0f;

        StartCoroutine(DeShockEnemy(duration));
    }
    
    private IEnumerator DeShockEnemy(float duration)
    {
        yield return new WaitForFixedUpdate();

        if (duration > _elapsedTime)
        {
            _elapsedTime += Time.deltaTime;

            _colorManager.SetColor(Color.Lerp(Main.colorManager.GetAreaColor(), Main.colorManager.GetEnemyColor(), _elapsedTime / duration));

            StartCoroutine(DeShockEnemy(duration));
        }
        else
        {
            _isShoked = false;

            _colorManager.SetMaterial(Main.colorManager.GetEnemyMaterial());

            _colorManager.SetColor(Main.colorManager.GetHighlightColor());
        }
    }
}
