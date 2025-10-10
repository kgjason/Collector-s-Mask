using UnityEngine;

public class Lever_GateGate : MonoBehaviour
{
    [Header("Connections")]
    public Gate gateA;
    public GateOnEnable gateB;
    public PlayerMovement player;

    [Header("Sprites")]
    public Sprite leverOffSprite;
    public Sprite leverOnSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    public bool isActive = false;
    public float interactRange = 1.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gateB?.Activate();
        UpdateLeverVisual();
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector3.Distance(player.transform.position, transform.position) <= interactRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isActive = !isActive;
                ToggleGates();
            }
        }
    }

    private void ToggleGates()
    {
        if (isActive)
        {
            gateA?.Activate();
            gateB?.Activate();
        }
        else
        {
            gateA?.Deactivate();
            gateB?.Deactivate();
        }

        UpdateLeverVisual();
    }

    private void UpdateLeverVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = isActive ? leverOnSprite : leverOffSprite;
    }
}
