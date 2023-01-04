using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeDrone : DroneBasis
{
    [Header("AdditionalSettings")]
    
    [SerializeField] private GameObject sawbladePerfab;

    private List<Sawblade> sawblades;

    [SerializeField] private float sawbladeSpeed;

    [SerializeField] private float damage;

    [SerializeField] private int maxSawblades;

    private void Start() 
    {
        sawblades = new List<Sawblade>();

        for (int i = 0; i < maxSawblades; i++)
        {
            CreateSawblade();
        }
    }

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            for (int i = 0; i < maxSawblades; i++)
            {
                if (sawblades[i].gameObject.activeSelf == false)
                {
                    sawblades[i].transform.position = transform.position;

                    sawblades[i].transform.right = _enemyList.GetEnemy().position - transform.position;
                    
                    sawblades[i].gameObject.SetActive(true);

                    sawblades[i].gameObject.GetComponent<Rigidbody2D>().AddForce(sawblades[i].transform.right * sawbladeSpeed, ForceMode2D.Impulse);
                    
                    return;
                }
            }
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 1:  break;
            case 2:  break; 
            case 3:  break;
            case 4:  break;
            case 5:  break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }

    private void CreateSawblade()
    {
        GameObject sawblade = Instantiate(sawbladePerfab, transform.position, Quaternion.identity);

        Sawblade sawbladeScript = sawblade.GetComponent<Sawblade>();

        sawblades.Add(sawbladeScript);

        sawbladeScript.Setup(damage, this);
        
        sawblade.SetActive(false);
    }

    public void OnDestroy()
    {
        foreach (Sawblade sawblade in sawblades)
        {
            Destroy(sawblade.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        foreach (Sawblade sawblade in sawblades)
        {
            sawblade.gameObject.SetActive(false);
        }
    }
}
