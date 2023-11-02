using UnityEngine;

public class FlipSpriteOnRotation : MonoBehaviour
{
    public Transform childTransform; // Reference to the child object
    public float flipThreshold = 0.0f; // Adjust this threshold to control when the flip happens
    public float childRotation;

    [SerializeField] SpriteRenderer spriteRenderer;
    private bool isFlipped = false;


    private void Update()
    {
        // Check the child's rotation
        childRotation = childTransform.eulerAngles.z;

        if (childRotation > flipThreshold && childRotation < 270 && !isFlipped)
        {
            // Flip the parent sprite
            spriteRenderer.flipX = true;
            isFlipped = true;
        }
        else if (childRotation < flipThreshold || childRotation > 270 && isFlipped)
        {
            // Unflip the parent sprite
            spriteRenderer.flipX = false;
            isFlipped = false;
        }
    }
}