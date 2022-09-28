using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Bullets
{
    public class BulletLinear : Bullet
    {
        public float speed = 1f;
        public float ttl = 1f;
        public GameObject explosionPrefab;

        private void Update()
        {
            ttl -= Time.deltaTime;
            if (ttl < 0)
            {
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            var transform1 = transform;
            transform1.position += transform1.right * (Time.deltaTime * speed);
        }

        void OnDestroy()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}