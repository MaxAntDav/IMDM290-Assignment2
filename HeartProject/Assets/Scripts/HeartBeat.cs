using UnityEngine;

public class HeartGenerator : MonoBehaviour
{
    public GameObject spherePrefab; // Assign a sphere prefab in the Inspector
    public int numPoints = 100; // Number of spheres in the heart
    public float scale = 0.3f; // Size of the spheres
    public float spacing = 2.0f; // Adjust spacing for better shape
    private GameObject[] spheres; // Array to store spheres
    private float colorLerpTime = 0f; // Time for color lerp

    void Start()
    {
        spheres = new GameObject[numPoints]; // Initialize array
        GenerateHeartShape();
    }

    void Update()
    {
        PulseEffect();
        ChangeColor();
    }

    void GenerateHeartShape()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = Mathf.PI * 2 * i / numPoints; // Spread points evenly

            float x = Mathf.Sqrt(2) * Mathf.Pow(Mathf.Sin(t), 3);
            float y = -Mathf.Pow(Mathf.Cos(t), 3) - Mathf.Pow(Mathf.Cos(t), 2) + 2 * Mathf.Cos(t);

            Vector3 position = new Vector3(x * spacing, y * spacing, 0);
            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            sphere.transform.localScale = Vector3.one * scale;
            sphere.transform.parent = transform; // Parent to keep hierarchy clean

            // Assign the sphere to the array
            spheres[i] = sphere;

            // Set initial color to red
            sphere.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void PulseEffect()
    {
        float scaleFactor = 1.0f + Mathf.Sin(Time.time * Mathf.PI) * 0.1f; // Pulsating effect every second
        transform.localScale = Vector3.one * scaleFactor;
    }

    void ChangeColor()
    {
        colorLerpTime += Time.deltaTime;
        float t = (Mathf.Sin(colorLerpTime * Mathf.PI) + 1) / 2; // Oscillates between 0 and 1 every second

        Color red = Color.red;
        Color pink = new Color(1f, 0.6f, 0.8f); // Custom pink color

        Color lerpedColor = Color.Lerp(pink, red, t);

        foreach (GameObject sphere in spheres)
        {
            if (sphere != null)
            {
                sphere.GetComponent<Renderer>().material.color = lerpedColor;
            }
        }
    }
}
