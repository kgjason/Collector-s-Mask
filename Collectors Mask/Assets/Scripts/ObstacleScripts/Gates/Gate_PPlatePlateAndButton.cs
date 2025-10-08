using UnityEngine;

public class Gate_PPlatePlateAndButton : MonoBehaviour
{
    [Header("Required Inputs")]
    [SerializeField] private Button_ForCombo button;
    [SerializeField] private PressurePlate_ForCombo pressurePlate1;
    [SerializeField] private PressurePlate_ForCombo pressurePlate2;

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
        if (button == null || pressurePlate1 == null || pressurePlate2 == null || gateComponent == null)
        {
            Debug.LogWarning("One or more required components are missing!");
            return;
        }

        bool buttonOk = button.isActive;
        bool plate1Ok = pressurePlate1.isActive;
        bool plate2Ok = pressurePlate2.isActive;

        // Debug log’larla durumu kontrol et

        bool allActive = buttonOk && plate1Ok && plate2Ok;

        if (allActive && !gateIsOpen)
        {
            Debug.Log("All active! Gate opening!");
            gateComponent.Activate();
            gateIsOpen = true;
        }
        else if (!allActive && gateIsOpen)
        {
            Debug.Log("Condition lost! Gate closing!");
            gateComponent.Deactivate();
            gateIsOpen = false;
        }
    }
}