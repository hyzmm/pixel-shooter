using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            Destroy(gameObject);
        }
    }
}