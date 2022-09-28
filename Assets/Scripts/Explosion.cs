using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float ttl = 0.3f;

    private void Start()
    {
        Destroy(gameObject, ttl);
    }
}