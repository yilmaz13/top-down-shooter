using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{   
    private Rigidbody _rigidbody;
    private float _speed;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(float speed)
    {
        _speed = speed;
    }
    public void Move()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _rigidbody.MovePosition(transform.position + m_Input * Time.fixedDeltaTime * _speed);
    }
    public void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.yellow);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }      
    }
}
