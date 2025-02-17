using UnityEngine;
using System.Collections;

public class PlayerMagicShot : MonoBehaviour
{
    public float duration = 5f;

    void Start()
    {
        StartCoroutine(DestroyAfterTime(duration));
    }

    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay); 
        Destroy(gameObject);
    }
}
