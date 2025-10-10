using UnityEngine;

public class PressurePlateL : MonoBehaviour, IInteractable
{
    public bool isActive;
    public int objectCount = 0;

    [Header("Animation")]
    public Animator animator; //  Eklendi

    private void Start()
    {
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCount++;
        if (objectCount >= 1)
        {
            isActive = true;
            if (animator != null)
                animator.SetBool("isPressed", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCount--;
        if (objectCount <= 0)
        {
            isActive = false;
            if (animator != null)
                animator.SetBool("isPressed", false);
        }
    }

    public void Activate() { }
    public void Deactivate() { }
}
