using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerController : Movable
{
    public float speed = 5f;

    private Animator _animator;
    private PlayerInputAction _playerInputAction;
    private Vector2 _lastMoveDir;

    private static readonly int Vx = Animator.StringToHash("vx");
    private static readonly int Vy = Animator.StringToHash("vy");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();
        _playerInputAction.Player.Move.performed += PerformMove;
        _playerInputAction.Player.Move.canceled += CancelMove;
    }

    private void PerformMove(InputAction.CallbackContext context)
    {
        _lastMoveDir = moveDirection = context.ReadValue<Vector2>();
        UpdateAnimatorVariables();
        _animator.enabled = true;
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
        UpdateAnimatorVariables();
        _animator.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * (speed * Time.deltaTime));
        // 此判断是为了维持上次的移动方向
        if (_lastMoveDir.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(_lastMoveDir.x), 1, 1);
        }
    }

    void UpdateAnimatorVariables()
    {
        _animator.SetFloat(Vx, moveDirection.x);
        _animator.SetFloat(Vy, moveDirection.y);
    }
}