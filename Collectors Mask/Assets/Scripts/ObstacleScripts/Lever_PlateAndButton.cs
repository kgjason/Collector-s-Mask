using UnityEngine;

public class Lever_PlateAndButton : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public Gate targetGate;
    public PressurePlate activatorPlate;
    public Button_PlateAndLever activatorButton;
    public PlayerMovement player;

    [Header("Settings")]
    public bool isActive;   // Lever açýk mý?
    public bool isLightOn;  // Kullanýlabilir mi?
    public float interactRange = 1.5f;

    private bool hasBeenActivated = false; // Lever kalýcý olarak açýldý mý?

    private void Start()
    {
        isActive = false;
        isLightOn = false;
    }

    private void Update()
    {
        // Eðer zaten kalýcý olarak aktifse artýk hiçbir þey kontrol etme
        if (hasBeenActivated)
            return;

        // Plate ve buton kontrolü
        if (activatorPlate != null && activatorButton != null)
        {
            // Her iki ön þart da aktif olmalý
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
                hasBeenActivated = true; // Artýk hep aktif kalacak
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
        // Artýk devre dýþý býrakma yok — kalýcý hale geldi
        if (hasBeenActivated)
            return;

        isActive = false;

        if (targetGate != null)
            targetGate.Deactivate();

        Debug.Log("Lever (Plate+Button) turned OFF");
    }
}
