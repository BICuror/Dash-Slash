using UnityEngine;

public class RandomPointMoveAgent : MoveAgent
{   
    private void Awake()
    {
        MoveToRandomPointBehaviour behaviour = new MoveToRandomPointBehaviour();

        behaviour.SetNewRandomPosition();

        SetBehabiouralOffset(Vector3.zero);

        SetMoveBehaviour(behaviour);
    }
}
