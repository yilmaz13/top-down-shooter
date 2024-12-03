using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyView : MonoBehaviour
{
    [SerializeField] private Transform _healthSliderPoint;
    [SerializeField] private Transform _armorSliderPoint; 
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera    _camera;
    private float _speed;

    private SliderView healthView;
    private SliderView armorView;

    public void Initialize(float speed, Camera camera)
    {
        _speed  = speed;
        _camera = camera;
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime * _speed);
    }

    public void LookAtPlayer(Transform player)
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Y ekseninde dönüþü engelle

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
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

        if (armor <= 0)
        {
            armorView.Hide();
        }
        else
        {
            armorView.UpdateValue(armor, maxArmor);
        }
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        if (health <= 0)
            return;

        healthView.UpdateValue(health, maxHealth);
    }

    public void UpdateArmorBar(float armor, float maxArmor)
    {
        if (armor <= 0)        
            return;
       
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
}
