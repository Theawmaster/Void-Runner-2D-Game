using UnityEngine;

[System.Serializable]
public class BackgroundElement
{
    public Transform backgroundTransform; // The transform of the background element
    public float scrollSpeed; // Speed of scrolling (positive for right, negative for left)
}

public class Background : MonoBehaviour
{
    [SerializeField] private BackgroundElement[] backgroundElements; // Array of background layers

    [SerializeField] private float resetPosition = 10f; // Position where the sprite loops back
    [SerializeField] private float startPosition = 10f; // Starting position after the loop

    private void Update()
    {
        foreach (BackgroundElement element in backgroundElements)
        {
            // Move the background horizontally
            element.backgroundTransform.position += Vector3.left * element.scrollSpeed * Time.deltaTime;

            // Reset the position when it goes out of bounds (looping effect)
            if (element.backgroundTransform.position.x < resetPosition)
            {
                element.backgroundTransform.position = new Vector3(startPosition, element.backgroundTransform.position.y, element.backgroundTransform.position.z);
            }
        }
    }
}
