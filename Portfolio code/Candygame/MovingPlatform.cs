using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] movePositions;
    Vector3[] moveLocations;

    public float moveTime;

    void Start()
    {
        moveLocations = new Vector3[movePositions.Length];
        
        for (int i = 0; i != movePositions.Length; i++)
        {
            moveLocations[i] = movePositions[i].position;
        }

        transform.DOPath(moveLocations, moveTime, PathType.Linear).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed);
    }
}
