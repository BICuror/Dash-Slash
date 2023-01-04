using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private WeaponArea explosionArea;

    [SerializeField] private float lifetime;

    private bool isActivated;

    public void Setup(float damageValue)
    {
        explosionArea.Setup(damageValue);

        StartCoroutine(DestroyAfter(lifetime));
    }

    private IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        
        GetComponent<Animator>().Play("MineExplode");
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (isActivated == false)
        {    
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                isActivated = true;

                GetComponent<Animator>().Play("MineExplode");   
            }    
        }    
    }

    public void DestroyMine()
    {
        Destroy(gameObject);
    }
}
