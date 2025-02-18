using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    void Start()
    {
        // Unlock the cursor and make it visible.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
