using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public Gate targetGate;
    public PlayerMovement player;

    [Header("Sprites")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    public bool isLightOn = true;
    public bool isActive = false;
    public float interactRange = 1.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogWarning($"{name}: SpriteRenderer eksik!");

        UpdateLeverVisual();
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isActive = !isActive;
                if (isActive)
                    Activate();
                else
                    Deactivate();
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        UpdateLeverVisual();

        if (targetGate != null)
            targetGate.Activate();

        Debug.Log("Lever turned on");
    }

    public void Deactivate()
    {
        isActive = false;
        UpdateLeverVisual();

        if (targetGate != null)
            targetGate.Deactivate();

        Debug.Log("Lever turned off");
    }

    private void UpdateLeverVisual()
    {
        if (spriteRenderer == null) return;
        spriteRenderer.sprite = isActive ? leverOnSprite : leverOffSprite;
    }
}
