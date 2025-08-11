using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private readonly float _minMagnitude = 0.1f;

    public void RotateTowardsDirection(Transform transform, Vector3 direction)
    {
        if (direction.magnitude > _minMagnitude)
        {
            Vector3 flatDirection = new Vector3(direction.x, 0, direction.z).normalized;

            if (flatDirection.magnitude > _minMagnitude)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                                    _rotationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}