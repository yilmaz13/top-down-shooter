public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
    void SetActive(bool active);
}
