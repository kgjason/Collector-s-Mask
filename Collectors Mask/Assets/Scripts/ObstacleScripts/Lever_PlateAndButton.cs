using UnityEngine;

public class Lever_PlateAndButton : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public Gate targetGate;
    public PressurePlate activatorPlate;
    public Button_PlateAndLever activatorButton;
    public PlayerMovement player;

    [Header("Settings")]
    public bool isActive;   // Lever a��k m�?
    public bool isLightOn;  // Kullan�labilir mi?
    public float interactRange = 1.5f;

    private bool hasBeenActivated = false; // Lever kal�c� olarak a��ld� m�?

    private void Start()
    {
        isActive = false;
        isLightOn = false;
    }

    private void Update()
    {
        // E�er zaten kal�c� olarak aktifse art�k hi�bir �ey kontrol etme
        if (hasBeenActivated)
            return;

        // Plate ve buton kontrol�
        if (activatorPlate != null && activatorButton != null)
        {
            // Her iki �n �art da aktif olmal�
            if (activatorPlate.isActive && activatorButton.isActive)
            {
                isLightOn = true;
            }
            else
            {
                isLightOn = false;
                isActive = false;
            }
        }

        // Oyuncu menzildeyse ve lever aktif edilebilirse
        if (isLightOn && Vector3.Distance(player.transform.position, transform.position) <= interactRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Activate();
                hasBeenActivated = true; // Art�k hep aktif kalacak
            }
        }
    }

    public void Activate()
    {
        isActive = true;

        if (targetGate != null)
            targetGate.Activate();

        Debug.Log("Lever (Plate+Button) permanently turned ON");
    }

    public void Deactivate()
    {
        // Art�k devre d��� b�rakma yok � kal�c� hale geldi
        if (hasBeenActivated)
            return;

        isActive = false;

        if (targetGate != null)
            targetGate.Deactivate();

        Debug.Log("Lever (Plate+Button) turned OFF");
    }
}
