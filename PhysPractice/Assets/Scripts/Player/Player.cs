using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMovement;
    private InputReader _inputReader;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMover>();
        _inputReader = GetComponent<InputReader>();
        _playerMovement.Initialize(_inputReader);
    }
}
