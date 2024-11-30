using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerView _playerView;
    private PlayerData _playerData;

    private bool _isActive;
    public void Initialize(PlayerView playerView, float playerBaseSpeed)
    {
        _playerData = new PlayerData();
        _playerData.SetPlayerSpeed(playerBaseSpeed);

        _playerView = playerView;
        _playerView.Initialize(playerBaseSpeed);
        _isActive = true;
    }

    void FixedUpdate()
    {
        if (!_isActive)
            return;

        if (_playerView == null)
            return;

        _playerView.Move();
        _playerView.LookAtMouse();

    }
}
