using System;
using UnityEngine;

[@RequireComponent(typeof(Movable))]
[@RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite side;
    public Sprite diagUp;
    public Sprite diagDown;

    public float horizontalMountOffset = 0.14f;
    public float verticalMountOffset = 0.08f;

    public Movable owner;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        var dirInEight = owner.DirectionInEight;
        var orientation = EightDirectionUtil.DirectionToVector(dirInEight);

        void ApplyVerticalOffset()
        {
            transform.localPosition = new Vector3(0, orientation.y * verticalMountOffset, 0);
        }

        void ApplyDiagOffset()
        {
            transform.localPosition = new Vector3(
                Mathf.Abs(orientation.x) * horizontalMountOffset,
                orientation.y * verticalMountOffset,
                0
            );
        }

        switch (dirInEight)
        {
            case EightDirection.Right:
            case EightDirection.Left:
                _spriteRenderer.sprite = side;
                _spriteRenderer.sortingOrder = 1;
                transform.localPosition = new Vector3(Mathf.Abs(orientation.x) * horizontalMountOffset, 0, 0);
                break;
            case EightDirection.Up:
                _spriteRenderer.sprite = up;
                _spriteRenderer.sortingOrder = 0;
                ApplyVerticalOffset();
                break;
            case EightDirection.Down:
                _spriteRenderer.sprite = down;
                _spriteRenderer.sortingOrder = 1;
                ApplyVerticalOffset();
                break;
            case EightDirection.UpRight:
            case EightDirection.UpLeft:
                _spriteRenderer.sprite = diagUp;
                _spriteRenderer.sortingOrder = 0;
                ApplyDiagOffset();
                break;
            case EightDirection.DownRight:
            case EightDirection.DownLeft:
                _spriteRenderer.sprite = diagDown;
                _spriteRenderer.sortingOrder = 1;
                ApplyDiagOffset();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}