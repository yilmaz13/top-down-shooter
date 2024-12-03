using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int poolSize = 20;

    private IObjectPool<Projectile> _pool;

    void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<Projectile>(projectilePrefab, poolSize, transform);
    }

    public IPoolable GetObject()
    {
        return _pool.Get();
    }

    public void ReturnProjectile(Projectile projectile)
    {
        _pool.Return(projectile);
    }
}
