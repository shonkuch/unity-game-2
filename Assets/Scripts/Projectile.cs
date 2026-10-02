using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private const float BaseProjectileSpeed = 5F;
    private const float BaseProjectileDamage = 10;
    
    void Update()
    {
        transform.position += Vector3.right * (Time.deltaTime * BaseProjectileSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Zombie zombie))
        {
            zombie.TakeDamage(BaseProjectileDamage);
            Destroy(gameObject);
        }

        if (other.CompareTag("KillBox"))
        {
            Destroy(gameObject);
        }
    }
}
