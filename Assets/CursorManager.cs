using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    public Texture2D customCursor;
    public Vector2 hotspot = Vector2.zero;
    
    
    void Start()
    {
        hotspot = new Vector2(customCursor.width / 2, customCursor.height / 2);
        Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
    }
}
