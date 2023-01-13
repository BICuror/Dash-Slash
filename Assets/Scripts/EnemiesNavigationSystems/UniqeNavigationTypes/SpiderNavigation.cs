// NOT IN USE CURRENTLY

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderNavigation : MonoBehaviour
{
    [Header("GeneralSettings")]

    [SerializeField] private float moveSpeed;

    [SerializeField] private LayerMask layerMask;

    [Range(0f, 1f)] [SerializeField] private float bodyEvaluationSpeed;

    [SerializeField] private float bodyHeight;

    [SerializeField] private Transform[] targets;

    [SerializeField] private Transform[] realTargets;

    [SerializeField] private float smoothness;

    [SerializeField] private float stepHeight;

    [Header("StepSettings")]

    [SerializeField] private float maxStepDistance;

    private bool isRotating = false;

    [SerializeField] private AnimationCurve rotationCurve;

    [SerializeField] private float rotationDuration;

    private float[] targetOffsets;

    [SerializeField] private float raycastHitLength;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        targetOffsets = new float[targets.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targetOffsets[i] = Vector3.Distance(targets[i].position, realTargets[i].position);
        }
    }

    private void EvaluateBody()
    {  
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, bodyHeight, layerMask);

        if (hit.collider != null)
        {
            transform.position = transform.position + transform.up * bodyEvaluationSpeed;
        }
        else
        {
            transform.position = transform.position + transform.up * -bodyEvaluationSpeed;
        }
    }

    private void Move()
    {
        transform.position = transform.position + transform.right * moveSpeed;
    }

    private void CheckToStep()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (Vector3.Distance(targets[i].position, realTargets[i].position) > maxStepDistance - targetOffsets[i])
            {
                RaycastHit2D hit = Physics2D.Raycast(realTargets[i].position, -realTargets[i].up, 100f, layerMask);

                StartCoroutine(PerformStep(i, targets[i].position));
            }
        }
    }

    private void FixedUpdate() 
    {
        EvaluateBody();
        
        Move();
        
        if (isRotating == false) CheckToRotate();
        
        CheckToStep();
    }

    private void CheckToRotate()
    {
        RaycastHit2D leftRaycastHit = Physics2D.Raycast(transform.position, -transform.right, raycastHitLength, layerMask);

        if (leftRaycastHit.collider != null) 
        {
            StartCoroutine(Rotate(-90f, rotationDuration));

            isRotating = true;
        }

        RaycastHit2D rightRaycastHit = Physics2D.Raycast(transform.position, transform.right, raycastHitLength, layerMask);

        if (rightRaycastHit.collider != null) 
        {
            StartCoroutine(Rotate(90f, rotationDuration));
            
            isRotating = true;
        }
    }

    private IEnumerator Rotate(float angleDifference, float rotDuration)
    {
        transform.Rotate(0f, 0f, angleDifference);
        yield return new WaitForSeconds(rotDuration);
        isRotating = false;
    }

    private IEnumerator PerformStep(int index, Vector3 startPos)
    {
        for(int i = 1; i <= smoothness; ++i)
        {
            targets[index].position = Vector3.Lerp(startPos, realTargets[index].position, i / (float)(smoothness + 1f));
            targets[index].position += transform.up * Mathf.Sin(i / (float)(smoothness + 1f) * Mathf.PI) * stepHeight;
            yield return new WaitForFixedUpdate();
        }

        targets[index].position = realTargets[index].position;
    }
}*/
