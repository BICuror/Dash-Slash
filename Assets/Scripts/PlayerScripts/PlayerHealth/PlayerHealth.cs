using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("InvinsibilitySettings")]

    [SerializeField] private float invinsibilityTime;

    private bool isInvisible;

    [Header("Links")]

    [SerializeField] private PlayerHealthStats _playerHealthStats;

    [SerializeField] private Animator anim;
 
    public UnityEvent PlayerHit;

    public UnityEvent PlayerDied;

    private void Awake() => _playerHealthStats.SetDefaultHealthPoints();

    private void OnEnable() => GiveInvincibilityTime(3f);

    private void OnDisable()
    {
        FullyHeal();

        isInvisible = true;
    } 

    public void FullyHeal()
    {
        if (_playerHealthStats.IsFullHealthPoints() == false)
        {
            _playerHealthStats.SetHealthPoints(_playerHealthStats.GetMaxHealthPoints());
        }
    }

    public void Heal()
    {
        if (_playerHealthStats.IsFullHealthPoints() == false)
        { 
            _playerHealthStats.SetHealthPoints(_playerHealthStats.GetCurrentHealthPoints() + 1);
        }
    }

    public void TryToDealDamage(EnemyProfile damageSourceProfile)
    {
        if (isInvisible == false)
        {
            GetHurt();
    
            GiveInvincibilityTime(invinsibilityTime);

            if (_playerHealthStats.GetCurrentHealthPoints() == 0) Lose(damageSourceProfile);    
        }
    }
    
    private void GetHurt()
    {
        PlayerHit?.Invoke();

        _playerHealthStats.SetHealthPoints(_playerHealthStats.GetCurrentHealthPoints() - 1);
    }

    private void Lose(EnemyProfile killerProfile)
    {
        PlayerDied.Invoke();
        
        Main.s_defeatMenu.OpenDefeatPanel(killerProfile);
    }

    public void GiveInvincibilityTime(float time)
    {
        StopAllCoroutines();

        MakeInvincible();

        StartCoroutine(InvincibilityTimer(time));
    }

    private IEnumerator InvincibilityTimer(float time)
    {
        yield return new WaitForSeconds(time);
        {
            MakeVincible();
        }
    }

    public void MakeInvincible()
    {
        isInvisible = true;

        anim.SetBool("isInvisible", true);
    }

    public void MakeVincible()
    {
        anim.SetBool("isInvisible", false);
    }

    public void SetVincibleState()
    {
        isInvisible = false;
    }

    public bool IsVinsible() => !isInvisible;
}
