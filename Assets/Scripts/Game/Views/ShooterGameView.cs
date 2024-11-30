using UnityEngine;

public class ShooterGameView : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    private IShooterGameViewListener _listener;
    private Camera _gameCamera;
    private GameResources _gameResources;

    public Transform PlayerSpawnPoint => _playerSpawnPoint;
    public void Initialize(IShooterGameViewListener listener, Camera gameCamera, GameResources gameResources)
    {
        _listener = listener;
        _gameCamera = gameCamera;
        _gameResources = gameResources;
    }
    public void Clear()
    {       
    }

    public void Create()
    {       
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
