using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour
{
    public Texture2D DefaultCursor;

    void Start()
    {
        Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
    }
}