using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyView : MonoBehaviour
{
    #region Private Members

    [SerializeField] private Transform _healthSliderPoint;
    [SerializeField] private Transform _armorSliderPoint;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _camera;
    private float _speed;

    private SliderView healthView;
    private SliderView armorView;

    #endregion

    #region Public Methods

    public void Initialize(float speed, Camera camera)
    {
        _speed = speed;
        _camera = camera;
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime * _speed);
    }

    public void LookAtPlayer(Transform player)
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;

        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    public void InitializeHealthBar(float health, float maxHealth)
    {
        healthView = InitializeSlider(_healthSliderPoint, "Prefabs/HpSliderParent", Color.red, health, maxHealth);
    }

    public void InitializeArmorBar(float armor, float maxArmor)
    {
        armorView = InitializeSlider(_armorSliderPoint, "Prefabs/ArmorSliderParent", Color.blue, armor, maxArmor);
        if (armor <= 0)
        {
            armorView.Hide();
        }
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        if (healthView == null || health <= 0)
            return;

        healthView.UpdateValue(health, maxHealth);
    }

    public void UpdateArmorBar(float armor, float maxArmor)
    {
        if (armorView == null || armor <= 0)
            return;

        armorView.UpdateValue(armor, maxArmor);
    }

    public void TurnSlidersAtCamera()
    {
        if (healthView != null)
            healthView.LookAtPosition(_camera.transform);

        if (armorView != null)
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

    #endregion

    #region Private Methods

    private SliderView InitializeSlider(Transform sliderPoint, string prefabPath, Color color, float value, float maxValue)
    {
        SliderView sliderView = Instantiate(Resources.Load<SliderView>(prefabPath), sliderPoint);
        sliderView.Initialize(color);
        if (value > 0)
        {
            sliderView.UpdateValue(value, maxValue);
        }
        return sliderView;
    }

    #endregion
}
