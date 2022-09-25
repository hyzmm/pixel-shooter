using System;
using UnityEngine;

namespace Bullets
{
    public class BulletLinear : MonoBehaviour
    {
        public float speed = 1f;

        private void Update()
        {
            var transform1 = transform;
            transform1.position += transform1.right * (Time.deltaTime * speed);
        }
    }
}