using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 _inputVector;
    private Vector2 _mousePosition;
    public static InputManager Instance { get; private set; }

    public Vector2 InputVector   => _inputVector;
    public Vector3 MousePosition => _mousePosition;

    public event Action OnFire;
    public event Action<int> OnSwitchWeapon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical   = Input.GetAxis("Vertical");

        _inputVector   = new Vector2(horizontal, vertical);
        _mousePosition = Input.mousePosition;

        if (Input.GetButtonDown("Fire1"))
        {
            OnFire?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnSwitchWeapon?.Invoke(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnSwitchWeapon?.Invoke(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnSwitchWeapon?.Invoke(2);
        }
    }
}
