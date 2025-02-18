using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioSource collectFX;
    
    private bool _collected;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager questManager = FindObjectOfType<QuestManager>();
            if (questManager != null && _collected == false)
            {
                collectFX.Play();
                _collected = true;
                questManager.CollectKey();
                Destroy(gameObject, 1);
            }
        }
    }
}

