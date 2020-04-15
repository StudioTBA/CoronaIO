using System;
using Com.StudioTBD.CoronaIO;
using UnityEngine;

namespace Agents.Aggressor
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _force = 1000f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Destroy(this.gameObject, 1.0f);
        }

        public void Shoot(Vector3 direction)
        {
            _rigidbody.AddForce(direction.normalized * _force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(GameManager.Tags.EnemyTag))
            {
                var zombie = other.gameObject.GetComponent<Flocker>();
                zombie.GotHit();
            }

            Destroy(gameObject);
        }
    }
}