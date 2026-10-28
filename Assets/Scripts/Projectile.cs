using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30f;
    public float damage = 25f;
    private Vector3 direction;
    private ShootPool shootPool;

    void OnEnable()
    {
        Invoke("SpawnTime", 5f);
    }
    public void StartProjectile(Vector3 direction, ShootPool shooter)
    {
        this.direction = direction;
        this.shootPool = shooter;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        HealthManager enemy = collision.gameObject.GetComponent<HealthManager>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        CancelInvoke("SpawnTime");
        shootPool.ReturnProjectile(gameObject);
    }

    void SpawnTime()
    {
        shootPool.ReturnProjectile(gameObject);
    }
}