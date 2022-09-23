using System.Collections;
using System.Collections.Generic;
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
        if (owner.moveDirection.x != 0 && owner.moveDirection.y != 0)
        {
            // 方向左下或者右下
            if (owner.moveDirection.y < 0)
            {
                _spriteRenderer.sprite = diagDown;
                _spriteRenderer.sortingOrder = 1;
            }
            // 方向左上或者右上
            else
            {
                _spriteRenderer.sprite = diagUp;
                _spriteRenderer.sortingOrder = 0;
            }

            transform.localPosition = new Vector3(
                Mathf.Abs(owner.moveDirection.x) * horizontalMountOffset,
                owner.moveDirection.y * verticalMountOffset,
                0
            );
        }

        // 方向左或者右
        if (owner.moveDirection.x != 0 && owner.moveDirection.y == 0)
        {
            transform.localPosition = new Vector3(Mathf.Abs(owner.moveDirection.x) * horizontalMountOffset, 0, 0);
            _spriteRenderer.sprite = side;
            _spriteRenderer.sortingOrder = 1;
            return;
        }

        if (owner.moveDirection.x == 0 && owner.moveDirection.y != 0)
        {
            if (owner.moveDirection.y > 0)
            {
                _spriteRenderer.sprite = up;
                _spriteRenderer.sortingOrder = 0;
            }
            else
            {
                _spriteRenderer.sprite = down;
                _spriteRenderer.sortingOrder = 1;
            }

            transform.localPosition = new Vector3(0, owner.moveDirection.y * verticalMountOffset, 0);
        }
    }
}