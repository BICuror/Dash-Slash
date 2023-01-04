using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDrone : DroneBasis
{
    [Header("AdditionalSettings")]

    [Range(0, 10)]
    [SerializeField] private int maxEnemies;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private float damage;

    [SerializeField] private float stunDuration;

    private Vector3[] positions;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected override void DoTask()
    {
        if (_enemyList.CheckIfEmpty() == false)
        {
            List<Transform> enemies = _enemyList.GetRandomGroup(maxEnemies);

            Vector3[] positions = new Vector3[enemies.Count + 1];

            positions[enemies.Count] = gameObject.transform.position;

            for (int i = 0; i < enemies.Count; i++)
            {
                positions[i] = enemies[i].position;
                
                enemies[i].GetComponent<IShockable>().Shock(stunDuration);
                
                enemies[i].gameObject.GetComponent<IDamagable>().GetHurt(Main.combatStats.MultiplyDamage(damage, DroneType.Area)); 
            }

            lineRenderer.positionCount = enemies.Count + 1;
            lineRenderer.SetPositions(positions);

            anim.Play("LightningDroneShoot");

            StartCoroutine(KeepupLine(enemies.Count));
        }    
    }

    public override void StopTask()
    {
        StopAllCoroutines();
        lineRenderer.positionCount = 0;
    }

    private IEnumerator KeepupLine(int index)
    {
        if (lineRenderer.positionCount != 0) yield break;

        lineRenderer.SetPosition(index, transform.position);

        yield return new WaitForFixedUpdate();

        StartCoroutine(KeepupLine(index));
    }

    public void TrailDisappear()
    {
        lineRenderer.positionCount = 0;
    }

    protected override void UpgradeDrone()
    {
        int level = GetLevel();

        switch(level)
        {
            case 2: maxEnemies++; break; 
            case 3: restoringTime -= 0.2f; damage += 1f; break;
            case 4: stunDuration += 0.5f; break;
            case 5: maxEnemies++; break;
            default: Debug.LogError("Wrong upgrade in " + gameObject.name); break;
        }
    }
}
