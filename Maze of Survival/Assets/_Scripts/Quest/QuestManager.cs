using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int keysRequired = 5;
    private int keysCollected = 0;

    public GameObject questCompleteUI; // UI Canvas for Quest Complete
    public GameObject portalPrefab; // Prefab to spawn when quest is complete
    public Transform portalSpawnPoint; // Location to spawn portal

    void Start()
    {
        questCompleteUI.SetActive(false); // Hide UI initially
    }

    public void CollectKey()
    {
        keysCollected++;
        if (keysCollected >= keysRequired)
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
        questCompleteUI.SetActive(true); // Show UI
        if (portalPrefab != null)
        {
            Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
