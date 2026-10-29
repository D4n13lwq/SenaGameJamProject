using UnityEngine;

public class LevelObstacle : MonoBehaviour
{
    [SerializeField, Min(1)] int level = 1;

    public int Level => level;
}
