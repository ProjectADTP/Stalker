using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

[RequireComponent(typeof(Gravity))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private CharacterController _characterController;

    private InputReader _inputReader;
    private Gravity _gravity;

    private Vector3 _moveDirection = Vector3.zero;

    private void Awake()
    {
        _gravity = GetComponent<Gravity>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public void Initialize(InputReader inputReader)
    {
        _inputReader = inputReader;
    }

    private void Movement()
    {
        if (_characterController != null || _inputReader != null)
        {
            Vector3 horizontalMovement = CalculateHorizontalMovement(_inputReader.GetMovementInput());

            if (_characterController.isGrounded)
            {
                _moveDirection = horizontalMovement;
                _moveDirection = _moveDirection.normalized * _speed;
            }

            _moveDirection = _gravity.ApplyGravity(_moveDirection);
            _characterController.Move(_moveDirection * Time.fixedDeltaTime);
        }
    }

    private Vector3 CalculateHorizontalMovement(Vector2 input)
    {
        if (input.x == 0 && input.y == 0)
        {
            return Vector3.zero;
        }

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveVector = forward * input.y + right * input.x;
        return moveVector.normalized * _speed;
    }
}
