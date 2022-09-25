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

    public GameObject bulletPrefab;

    public float horizontalMountOffset = 0.14f;
    public float verticalMountOffset = 0.08f;
    public float bulletSpawnOffset = 0.15f;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    public void UpdateDirection(EightDirection direction)
    {
        var orientation = EightDirectionUtil.DirectionToVector(direction);

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

        switch (direction)
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

    public void Fire(Vector2 orientation)
    {
        var dirInEight = EightDirectionUtil.GetEightDirection(orientation);
        Vector3 dirInVec3 = EightDirectionUtil.DirectionToVector(dirInEight);

        var rotation = Quaternion.Euler(0, 0, Mathf.Atan2(orientation.y, orientation.x) * Mathf.Rad2Deg);
        Instantiate(bulletPrefab, transform.position + dirInVec3 * bulletSpawnOffset, rotation);
    }
}