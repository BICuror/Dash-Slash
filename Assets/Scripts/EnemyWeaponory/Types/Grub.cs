using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grub : MonoBehaviour
{
    [SerializeField] private float segmentSpawnDelay;
    [SerializeField] private GameObject grubSegment;

    private List<GameObject> segments;

    private EnemyColorManager colorManager;

    private void Awake()
    {
        colorManager = gameObject.GetComponent<EnemyColorManager>();

        Setup();
    }

    private void Setup()
    {
        segments = new List<GameObject>();

        segments.Add(gameObject);

        for (int i = 1; i < gameObject.GetComponent<GrubEnemyHealth>().GetMaxSegmentsCount(); i++)
        {
            StartCoroutine(SpawnSegment(transform.position, i));
        }
    }

    private IEnumerator SpawnSegment(Vector3 spawnPosition, int index)
    {
        yield return new WaitForSeconds(index * segmentSpawnDelay);

        segments.Add(Instantiate(grubSegment, spawnPosition, transform.rotation));
        
        colorManager.AddRenderer(segments[segments.Count - 1].GetComponent<SpriteRenderer>());

        ConnectSegments(segments.Count - 2, segments.Count - 1);

        segments[segments.Count - 1].GetComponent<GrubSegment>().Setup(gameObject);
    }

    public void DestroyLastSegment()
    {
        segments[segments.Count - 1].GetComponent<Animator>().Play("GrubSegmentDisappear");

        segments.RemoveAt(segments.Count - 1);
    }
    
    private void ConnectSegments(int firstSegment, int secondSegment)
    {
        segments[secondSegment].GetComponent<DistanceJoint2D>().connectedBody = segments[firstSegment].GetComponent<Rigidbody2D>();
    }

    public int GetActiveSegmentsCount()
    {
        return segments.Count;
    }

    public void RemoveSegment(GameObject segment)
    {
        colorManager.RemoveRenderer(segment.GetComponent<SpriteRenderer>());
    }

    public void DestroyAllSegments()
    {
        StopAllCoroutines();

        for (int i = 0; i < segments.Count; i++)
        {
            segments[i].GetComponent<Animator>().Play("GrubSegmentDisappear");
        }
    }
}
