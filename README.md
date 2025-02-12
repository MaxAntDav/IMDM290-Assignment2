<img width="1348" alt="Screenshot 2025-02-12 at 3 01 42 PM" src="https://github.com/user-attachments/assets/cd605783-3519-493f-b25b-0900d09e318a" />
Here is the image of the project and below is the code.

using UnityEngine;

public class HeartGenerator : MonoBehaviour
{
    public GameObject spherePrefab; //Assigns a sphere.
    public int numPoints = 100; //Total number of spheres in the heart.
    public float scale = 0.3f; //Size of the spheres.
    public float spacing = 2.0f; //Adjusts sphere spacing.
    private GameObject[] spheres; //An array that stores the spheres.
    private float colorDuration = 0f; //Duration of heart color.

    void Start()
    {
        spheres = new GameObject[numPoints]; //Initializes array.
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
            float t = Mathf.PI * 2 * i / numPoints; //Spreads the points.

            float x = Mathf.Sqrt(2) * Mathf.Pow(Mathf.Sin(t), 3);
            float y = -Mathf.Pow(Mathf.Cos(t), 3) - Mathf.Pow(Mathf.Cos(t), 2) + 2 * Mathf.Cos(t); //Creates the heart shape.

            Vector3 position = new Vector3(x * spacing, y * spacing, 0);
            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity);
            sphere.transform.localScale = Vector3.one * scale;
            sphere.transform.parent = transform; //Parent to keep hierarchy clean.

            //Assign the sphere to the array.
            spheres[i] = sphere;

            //Sets the starting color to red.
            sphere.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void PulseEffect()
    {
        float scaleFactor = 1.0f + Mathf.Sin(Time.time * Mathf.PI) * 0.1f; //Creates a pulsing effect every second.
        transform.localScale = Vector3.one * scaleFactor;
    }

    void ChangeColor()
    {
        colorDuration += Time.deltaTime;
        float t = (Mathf.Sin(colorDuration * Mathf.PI) + 1) / 2; //Changes between 0 and 1 every second.

        Color red = Color.red;
        Color pink = new Color(1f, 0.6f, 0.8f); //Pink.
        Color colorChange = Color.Lerp(pink, red, t);

        foreach (GameObject sphere in spheres)
        {
            if (sphere != null)
            {
                sphere.GetComponent<Renderer>().material.color = colorChange;
            }
        }
    }
}



