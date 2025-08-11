using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance = 1f;
    
    private Rigidbody _rigidbody;
    
    private Vector3 _currentGroundNormal = Vector3.up;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void CheckGroundNormal()
    {
        Ray ray = new Ray(_rigidbody.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, _groundCheckDistance))
        {
            _currentGroundNormal = hit.normal;
        }
        else
        {
            _currentGroundNormal = Vector3.up;
        }
    }

    public Vector3 GetGroundNormal()
    {
        return _currentGroundNormal;
    }
}