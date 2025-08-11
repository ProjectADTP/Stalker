using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Vector3 ApplyGravity(Vector3 moveDirection)
    {
        return moveDirection += Physics.gravity * Time.fixedDeltaTime;
    }
}
