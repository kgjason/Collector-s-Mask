using UnityEngine;

public class Lever1C : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public Gate targetGate;
    public PressurePlateL activater;
    public PlayerMovement player;

    [Header("Sprites")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    public bool isLightOn = false;
    public bool isActive = false;
    public float interactRange = 1.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateLeverVisual();
    }

    private void Update()
    {
        if (player == null) return;

        if (activater != null)
            isLightOn = activater.isActive;

        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange && isLightOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isActive = !isActive;
                if (isActive) Activate();
                else Deactivate();
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        UpdateLeverVisual();
        targetGate?.Activate();
        Debug.Log("Lever1C turned on");
    }

    public void Deactivate()
    {
        isActive = false;
        UpdateLeverVisual();
        targetGate?.Deactivate();
        Debug.Log("Lever1C turned off");
    }

    private void UpdateLeverVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = isActive ? leverOnSprite : leverOffSprite;
    }
}
