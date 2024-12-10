using UnityEngine;

public abstract class AgentView : MonoBehaviour
{
    [SerializeField] protected Transform _healthSliderPoint;
    [SerializeField] protected Transform _armorSliderPoint;
    [SerializeField] protected Rigidbody _rigidbody;

    protected GameObject _healthSliderPrefabs;
    protected GameObject _armorSliderPrefabs;
    protected Camera _camera;

    protected SliderView healthView;
    protected SliderView armorView;

    protected float _speed;

    public virtual void Initialize(float speed, Camera camera, GameObject healthSliderPrefabs, GameObject armorSliderPrefabs)
    {
        _speed = speed;
        _camera = camera;
        _healthSliderPrefabs = healthSliderPrefabs;
        _armorSliderPrefabs = armorSliderPrefabs;
    }

    public void InitializeHealthBar(float health, float maxHealth)
    {
        healthView = Instantiate(_healthSliderPrefabs, _healthSliderPoint).GetComponent<SliderView>();
        healthView.Initialize(health, maxHealth);      
    }

    public void InitializeArmorBar(float armor, float maxArmor)
    {
        armorView = Instantiate(_armorSliderPrefabs, _armorSliderPoint).GetComponent<SliderView>();
        armorView.Initialize(armor, maxArmor);
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthView.UpdateValue(health, maxHealth);
    }

    public void UpdateArmorBar(float armor, float maxArmor)
    {
        armorView.UpdateValue(armor, maxArmor);
    }

    public void ShowBars()
    {
        healthView.Show();
        armorView.Show();
    }
    public void TurnSlidersAtCamera()
    {
        healthView.LookAtPosition(_camera.transform);
        armorView.LookAtPosition(_camera.transform);
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
    }
}
