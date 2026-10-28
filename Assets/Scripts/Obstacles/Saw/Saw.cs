using UnityEngine;
using System.Collections.Generic;

public class Saw : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Transform> pivots;
    int currentPivotIndex;
    [SerializeField] float minDistance;
    [SerializeField] float distance;
    [SerializeField] bool finishOnLastPivot;

    private void OnEnable()
    {
        currentPivotIndex = 0;
        transform.position = pivots[currentPivotIndex].position;
    }

    private void Update()
    {
        if (currentPivotIndex < pivots.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, pivots[currentPivotIndex].position, speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, pivots[currentPivotIndex].position);
        }

        if (distance <= minDistance)
        {
            currentPivotIndex++;
            if (currentPivotIndex >= pivots.Count)
                if (finishOnLastPivot == false)
                    currentPivotIndex = 0;
        }
    }
}
