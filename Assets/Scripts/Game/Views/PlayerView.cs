using UnityEngine;

public class PlayerView : AgentView
{
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

    public void Transfer(Vector3 vector3)
    {
        transform.position = vector3;
    }
}
