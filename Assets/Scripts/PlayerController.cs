using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerController : Movable
{
    public float speed = 5f;

    private Animator _animator;
    private PlayerInputAction _playerInputAction;
    private Vector2 _moveDir;

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
        _playerInputAction.Player.Aim.performed += PerformAim;
        _playerInputAction.Player.Move.performed += PerformMove;
        _playerInputAction.Player.Move.canceled += CancelMove;
    }

    private void PerformAim(InputAction.CallbackContext context)
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        orientation = (mousePos - (Vector2)transform.position).normalized;
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


    // Update is called once per frame
    void Update()
    {
        transform.Translate(_moveDir * (speed * Time.deltaTime));
    }

    void UpdateAnimatorParams()
    {
        _animator.SetFloat(Vx, orientation.x);
        _animator.SetFloat(Vy, orientation.y);
    }
}