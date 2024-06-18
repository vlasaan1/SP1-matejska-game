using UnityEngine;

public class MissEffect : MonoBehaviour
{
    [SerializeField] float timeToDestroy = .5f;
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }
}
