using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Texture2D cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(
            cursorTexture,
            new Vector2(cursorTexture.width / 2f, y: cursorTexture.height / 2f),
            CursorMode.Auto
        );
    }

    // Update is called once per frame
    void Update()
    {
    }
}