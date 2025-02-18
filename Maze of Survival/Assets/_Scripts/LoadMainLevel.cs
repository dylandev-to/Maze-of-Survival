using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainLevel : MonoBehaviour
{
    // This method will be called when the button is clicked.
    public void LoadDungeonDemo()
    {
        // Ensure the scene name matches exactly.
        SceneManager.LoadScene("Dungeon_Demo");
    }
}
