using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zoneObject;

    [SerializeField] private float zoneLifetime;

    [SerializeField] private float spawnPeriod;

    private Zone currentZone; 

    public void SetStats(float zoneLifetimeValue, float spawnPeriodValue)
    {
        spawnPeriod = spawnPeriodValue;

        zoneLifetime = zoneLifetimeValue;
    }

    private void Awake()
    {
        Main.arenaManager.ArenaStarted.AddListener(StartSpawinig);

        Main.arenaManager.ArenaStopped.RemoveListener(StopSpawinig);

        currentZone = Instantiate(zoneObject, Vector3.zero, Quaternion.identity).GetComponent<Zone>();

        currentZone.Activate(3f);

        ModifyZone(currentZone.gameObject);
    }

    private void StartSpawinig()
    {
        ModifyZone(currentZone.gameObject);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnPeriod);

        currentZone.Activate(zoneLifetime);

        Vector3 randomPosition = new Vector3(Random.Range(-Main.roomSettings.GetWidth() / 3 * 2, Main.roomSettings.GetHeight() / 3 * 2), Random.Range(-Main.roomSettings.GetHeight() / 3 * 2, Main.roomSettings.GetHeight() / 3 * 2), 0f);

        currentZone.transform.position = randomPosition;

        StartCoroutine(Spawn());
    }

    protected virtual void ModifyZone(GameObject zone) {}

    private void StopSpawinig()
    {
        StopAllCoroutines();

        currentZone.gameObject.SetActive(false);
    }
}
