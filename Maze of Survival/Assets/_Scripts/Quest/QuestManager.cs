using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int keysRequired = 5;
    private int keysCollected = 0;

    public GameObject questCompleteUI;
    public TMP_Text scoreText;

    void Start()
    {
        questCompleteUI.SetActive(false); // Hide UI initially
        SetScoreText(keysCollected, keysRequired);
    }

    public void CollectKey()
    {
        keysCollected++;
        if (keysCollected >= keysRequired)
        {
            CompleteQuest();
        }
        SetScoreText(keysCollected, keysRequired);
    }

    private void SetScoreText(int collected, int required)
    {
        scoreText.text = $"{collected}/{required}";
    }

    void CompleteQuest()
    {
        questCompleteUI.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
