using UnityEngine;

public class Lever_PlateAndButton : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public Gate targetGate;
    public PressurePlate activatorPlate;
    public Button_PlateAndLever activatorButton;
    public PlayerMovement player;

    [Header("Sprites")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    public bool isActive = false;
    public bool isLightOn = false;
    public float interactRange = 1.5f;

    private bool hasBeenActivated = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateLeverVisual();
    }

    private void Update()
    {
        if (player == null) return;
        if (hasBeenActivated) return;

        if (activatorPlate != null && activatorButton != null)
            isLightOn = activatorPlate.isActive && activatorButton.isActive;

        if (isLightOn && Vector3.Distance(player.transform.position, transform.position) <= interactRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Activate();
                hasBeenActivated = true;
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        UpdateLeverVisual();
        targetGate?.Activate();
        Debug.Log("Lever (Plate+Button) permanently turned ON");
    }

    public void Deactivate()
    {
        if (hasBeenActivated) return;

        isActive = false;
        UpdateLeverVisual();
        targetGate?.Deactivate();
        Debug.Log("Lever (Plate+Button) turned OFF");
    }

    private void UpdateLeverVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = isActive ? leverOnSprite : leverOffSprite;
    }
}
