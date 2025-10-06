using UnityEngine;

public class Lever_PlateAndButton : MonoBehaviour, IInteractable
{
    [Header("Connections")]
    public Gate targetGate;
    public PressurePlate activatorPlate;
    public Button activatorButton;
    public PlayerMovement player;

    [Header("Settings")]
    public bool isActive;   // Lever açýk mý?
    public bool isLightOn;  // Kullanýlabilir mi?
    public float interactRange = 1.5f;

    private void Start()
    {
        isActive = false;
        isLightOn = false;
    }

    private void Update()
    {
        // Her frame'de ön þartlarý kontrol et
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
                isActive = false; // biri kapanýrsa lever da kapanýr
            }
        }

        // Oyuncu menzildeyse ve lever aktif edilebilirse
        if (isLightOn && Vector3.Distance(player.transform.position, transform.position) <= interactRange)
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
        if (targetGate != null)
            targetGate.Activate();

        Debug.Log("Lever (Plate+Button) turned ON");
    }

    public void Deactivate()
    {
        if (targetGate != null)
            targetGate.Deactivate();

        Debug.Log("Lever (Plate+Button) turned OFF");
    }
}
