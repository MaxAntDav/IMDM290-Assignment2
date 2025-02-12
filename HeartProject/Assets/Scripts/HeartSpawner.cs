using UnityEngine;
using System.Collections.Generic;

public class HeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab; // Assign a heart prefab in Unity
    public int numHearts = 6; // Number of hearts around the center
    public float distance = 3.0f; // Distance from the center heart
    private List<GameObject> hearts = new List<GameObject>(); // Store heart objects

    void Start()
    {
        // Spawn the center heart
        GameObject centerHeart = Instantiate(heartPrefab, transform.position, Quaternion.identity);
        centerHeart.transform.parent = transform;
        hearts.Add(centerHeart);

        // Spawn additional hearts around the center
        for (int i = 0; i < numHearts; i++)
        {
            float angle = (i * Mathf.PI * 2) / numHearts; // Evenly distribute hearts
            Vector3 position = new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);
            GameObject newHeart = Instantiate(heartPrefab, position, Quaternion.identity);
            newHeart.transform.parent = transform; // Keep hierarchy clean
            hearts.Add(newHeart);
        }
    }

    void Update()
    {
        PulseEffect();
    }

    void PulseEffect()
    {
        float scaleFactor = 1.0f + Mathf.Sin(Time.time * Mathf.PI) * 0.1f; // Pulsate effect
        foreach (GameObject heart in hearts)
        {
            if (heart != null)
            {
                heart.transform.localScale = Vector3.one * scaleFactor;
            }
        }
    }
}
