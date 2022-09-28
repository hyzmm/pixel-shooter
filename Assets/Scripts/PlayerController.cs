using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerController : Movable
{
    public float speed = 5f;
    public GameObject weaponPrefab;
    public Transform weaponSlot;

    private GameObject _weapon;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private PlayerInputAction _playerInputAction;
    private Vector2 _moveDir;

    private static readonly int Vx = Animator.StringToHash("vx");
    private static readonly int Vy = Animator.StringToHash("vy");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.speed = 0;

        _weapon = Instantiate(weaponPrefab, new Vector3(0, -0.04f, 0), Quaternion.identity, weaponSlot);
    }

    void OnEnable()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();
        _playerInputAction.Player.Aim.performed += PerformAim;
        _playerInputAction.Player.Move.performed += PerformMove;
        _playerInputAction.Player.Move.canceled += CancelMove;
        _playerInputAction.Player.Fire.performed += PerformFire;
    }

    private void PerformAim(InputAction.CallbackContext context)
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        orientation = (mousePos - (Vector2)transform.position).normalized;
        _weapon.GetComponent<Weapon>().UpdateDirection(DirectionInEight);

        UpdateAnimatorParams();

        // 此判断是为了维持上次的移动方向
        if (orientation.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(orientation.x), 1, 1);
        }
    }

    private void PerformMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector2>();
        _animator.speed = 1;
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        _moveDir = Vector2.zero;
        _animator.speed = 0;
    }

    private void PerformFire(InputAction.CallbackContext obj)
    {
        _weapon.GetComponent<Weapon>().Fire(orientation);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.MovePosition((Vector2)transform.position + _moveDir * (speed * Time.deltaTime));
    }

    void UpdateAnimatorParams()
    {
        _animator.SetFloat(Vx, orientation.x);
        _animator.SetFloat(Vy, orientation.y);
    }
}