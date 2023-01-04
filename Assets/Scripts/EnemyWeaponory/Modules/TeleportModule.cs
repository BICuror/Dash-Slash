using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportModule : MonoBehaviour
{
    [SerializeField] private GameObject teleportPrefab;

    [SerializeField] private SpriteRenderer originalRenderer;

    private GameObject teleportPointer;

    public void Teleport(Vector3 position, float teleportDuration)
    {
        teleportPointer = Instantiate(teleportPrefab, position, Quaternion.identity);

        teleportPointer.GetComponent<TeleportPosition>().Setup(originalRenderer.sprite);

        StartCoroutine(ChangePosition(teleportDuration));
    }

    private IEnumerator ChangePosition(float teleportDuration)
    {
        yield return new WaitForSeconds(teleportDuration);

        gameObject.transform.position = teleportPointer.transform.position;

        gameObject.transform.rotation = teleportPointer.transform.rotation;

        Destroy(teleportPointer);

        teleportPointer = null;
    }

    private void OnDestroy()
    {
        if (teleportPointer != null) Destroy(teleportPointer);
    }
}
