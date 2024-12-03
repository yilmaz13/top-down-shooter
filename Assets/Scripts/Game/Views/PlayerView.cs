using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _healthSliderPoint;
    [SerializeField] private Transform _armorSliderPoint; 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera    _camera;
    private float _speed;

    private SliderView healthView;
    private SliderView armorView;
    private float turnSmoothVelocity;
    public void Initialize(float speed, Camera camera)
    {
        _speed  = speed;
        _camera = camera;
    }
    public void Move()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rigidbody.MovePosition(transform.position + m_Input * Time.fixedDeltaTime * _speed);
    }
    public void LookAtMouse()
    {
        Vector2 playerScreenPosition = _camera.WorldToScreenPoint(transform.position);
        Vector2 mouseScreenPosition = Input.mousePosition;        
        Vector2 directionToMouse = mouseScreenPosition - playerScreenPosition;

        Vector3 worldDirection = new Vector3(directionToMouse.x, 0, directionToMouse.y);
        worldDirection = Quaternion.Euler(0, -90, 0) * worldDirection;
     
        if (worldDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(worldDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }  

    public void InitializeHealthBar(float health, float maxHealth)
    {
        healthView = Instantiate(Resources.Load<SliderView>("Prefabs/HpSliderParent"), _healthSliderPoint);
        healthView.Initialize(Color.red);
        healthView.UpdateValue(health, maxHealth);
    }

    public void InitializeArmorBar(float armor, float maxArmor)
    {
        armorView = Instantiate(Resources.Load<SliderView>("Prefabs/ArmorSliderParent"), _armorSliderPoint);
        armorView.Initialize(Color.blue);
        armorView.UpdateValue(armor, maxArmor);
    }
    public void UpdateHealthBar(float health, float maxHealth)
    {        
        healthView.UpdateValue(health, maxHealth);
    }

    public void UpdateArmorBar(float armor, float maxArmor)
    {
       armorView.UpdateValue(armor, maxArmor);
    }

    public void TurnSlidersAtCamera()
    {
        healthView.LookAtPosition(_camera.transform);
        armorView.LookAtPosition(_camera.transform);
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }

    public void Transfer(Vector3 vector3)
    { 
        transform.position = vector3;
    }

}
