using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(EnemyRotator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float minDistance = 2.5f;

    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;
    private EnemyRotator _rotator;
    private Player _player;

    private readonly float _minMagnitude = 0.1f;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _rotator = GetComponent<EnemyRotator>();
        _rigidbody = GetComponent<Rigidbody>();
        
        _rigidbody.useGravity = true;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        if (_player == null) 
            return;

        Movement();
    }

    public void Initialize(Player player)
    {
        _player = player;
    }

    private void Movement()
    {
        Vector3 directionToPlayer = (_player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        _groundChecker.CheckGroundNormal();

        if (distanceToPlayer > minDistance)
        {
            Vector3 movementDirection = CalculateSurfaceMovement(directionToPlayer);

            if (movementDirection != Vector3.zero)
            {
                _rotator.RotateTowardsDirection(transform, movementDirection);

                Vector3 targetVelocity = movementDirection * moveSpeed;
                targetVelocity.y = _rigidbody.velocity.y;
                _rigidbody.velocity = targetVelocity;
            }
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private Vector3 CalculateSurfaceMovement(Vector3 direction)
    {
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, _groundChecker.GetGroundNormal());

        if (projectedDirection.magnitude > _minMagnitude)
        {
            return projectedDirection.normalized;
        }

        return Vector3.zero;
    }
}
