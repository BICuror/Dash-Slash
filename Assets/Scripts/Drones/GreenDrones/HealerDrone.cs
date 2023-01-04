using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerDrone : DroneBasis
{
    [Header("AdditionalSettings")]
    
    [SerializeField] private GameObject healingOrb;

    [SerializeField] private float orbLifetime;

    [SerializeField] private float neededDistance;

    protected override void DoTask()
    {
        float y = Random.Range(-Main.roomSettings.GetHeight(), Main.roomSettings.GetHeight());
        float x = Random.Range(-Main.roomSettings.GetWidth(), Main.roomSettings.GetWidth());

        Vector3 pos = new Vector3(x, y, 0f);
        
        if (_enemyList != Main.enemyList)
        {
            y = transform.position.y + Random.Range(-1f, 1f);
            x = transform.position.x + Random.Range(-1f, 1f);

            pos = transform.position + new Vector3(x, y, 0f).normalized * 2f;
        }

        if (Vector3.Distance(Main.playerTransform.position, pos) > neededDistance)
        {
            GameObject curOrb = Instantiate(healingOrb, pos, Quaternion.Euler(0f, 0f, 0f));

            curOrb.GetComponent<HealingOrb>().Setup(orbLifetime);
        }
        else
        {
            DoTask();
        }          
    }
    
    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: restoringTime -= 0.5f; break; 
            case 3: neededDistance -= 1f; break;
            case 4: restoringTime -= 0.5f;  break;
            case 5: orbLifetime += 0.8f; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
