using UnityEngine;

public class PlayerView : AgentView
{
    public void Move()
    {
        Vector2 _inputVector = InputManager.Instance.InputVector;
        Vector3 _moveVector = new Vector3(_inputVector.x, 0, _inputVector.y);

        _rigidbody.MovePosition(transform.position + _moveVector * Time.fixedDeltaTime * _speed);
    } 

    public void LookAtMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
       
        Ray ray = _camera.ScreenPointToRay(InputManager.Instance.MousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - _firePoint.position);
            targetRotation.x = 0;
            targetRotation.z = 0;

            transform.rotation = Quaternion.Slerp(_firePoint.rotation, targetRotation, 10f * Time.deltaTime);
        }        
    }

    public void Transfer(Vector3 vector3)
    {
        transform.position = vector3;
    }
}
