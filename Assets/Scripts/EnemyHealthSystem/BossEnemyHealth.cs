using UnityEngine;
using UnityEngine.Events;

public sealed class BossEnemyHealth : EnemyHealth
{
    [Header("UnlockSettings")]

    [SerializeField] private GameObject _unlockPointPrefab;

    [SerializeField] private int _unlockPointsAmount;

    [Header("Links")]

    [SerializeField] private ParticleSystem _hurtParticleSystem;

    [SerializeField] private ParticleSystem _deathParticleSystem;

    [SerializeField] private UnityEvent OnDeath;

    private bool _died;

    private void OnEnable() => Main.s_bossHealthBar.ActivateHealthBar();

    private void OnDestroy() 
    {
        Main.s_bossHealthBar.DeactivateHealthBar();
        
        Main.arenaManager.StopArenaBattle();
    } 

    public override void GetPercentHurt(float percent) => GetHurt(maxHealth * percent * 0.1f);

    public override void GetHurt(float damage)
    {
        _hurtParticleSystem.Play();

        damage = MultyplyDamageByStatus(damage);

        currentHealth -= damage;

        Main.s_bossHealthBar.UpdateHealthBar(currentHealth / maxHealth);
            
        if (currentHealth <= 0f) 
        {
            if (_died == false) Die();
        }
        else statusManager.HighlightEnemy();   
    }

    private void Die()
    {
        _died = true;
        
        _deathParticleSystem.Play();

        OnDeath.Invoke();

        CreatePoints();
    }

    private void CreatePoints()
    {
        for (int i = 0; i < _unlockPointsAmount; i++)
        {
            Instantiate(_unlockPointPrefab, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }
    }
}
