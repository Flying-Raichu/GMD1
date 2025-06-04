using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }
    private PlayerInput input;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            input = new PlayerInput();
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    // TODO: if we add other controller schemes, will have to add support for stuff other than "Default"
    
    public Vector2 GetMovement() // gets joystick position
    {
        return input.Default.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetMousePosition() // gets mouse position
    {
        return Mouse.current.position.ReadValue();
    }

    public bool APressed() // A button || left mouse button
    {
        return input.Default.A.IsPressed();
    }
    
    public bool AUp()
    {
        return input.Default.A.WasReleasedThisFrame();
    }

    public bool PausePressed() // start button || backspace
    {
        return input.Default.Start.WasPressedThisFrame();
    }

    public bool RTriggerPressed() // B button || Space
    {
        return input.Default.RightTrigger.IsPressed();
    }
}
