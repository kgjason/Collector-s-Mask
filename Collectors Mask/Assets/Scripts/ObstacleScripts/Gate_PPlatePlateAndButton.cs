using UnityEngine;

public class Gate_PPlatePlateAndButton : MonoBehaviour
{
    [Header("Required Inputs")]
    public Button_ForCombo button;
    public PressurePlate_ForCombo pressurePlate;
    public PlayerPressurePlate_ForCombo playerPressure;

    private Gate gateComponent;
    private bool gateIsOpen = false;

    private void Start()
    {
        gateComponent = GetComponent<Gate>();
        if (gateComponent == null)
            Debug.LogError("Gate component not found on " + gameObject.name);
    }

    private void Update()
    {
        if (button == null || pressurePlate == null || playerPressure == null || gateComponent == null)
            return;

        bool buttonOk = button.isActive;
        bool plateOk = pressurePlate.isActive;
        bool playerOk = playerPressure.objectCount > 0;

        // Debug log’larla durumu kontrol et
        Debug.Log($"[GateCheck] Button:{buttonOk} | Plate:{plateOk} | Player:{playerOk}");

        bool allActive = buttonOk && plateOk && playerOk;

        if (allActive && !gateIsOpen)
        {
            Debug.Log("All active  Gate opening!");
            gateComponent.Activate();
            gateIsOpen = true;
        }
        else if (!allActive && gateIsOpen)
        {
            Debug.Log("Condition lost  Gate closing!");
            gateComponent.Deactivate();
            gateIsOpen = false;
        }
    }
}
