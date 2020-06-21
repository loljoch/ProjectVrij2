using Extensions.Generics.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualController : GenericSingleton<VirtualController, VirtualController>
{
    [SerializeField] private InputAction movementAction;
    [SerializeField] private InputAction attackAction;
    [SerializeField] private InputAction interactPressAction;
    private InputAction interactHoldAction; //Doesn't need to be serialized
    [SerializeField] private InputAction inventoryAction;
    [SerializeField] private InputAction cancelItemOptionsAction;

    public static Pointer pointer = new Pointer();

    //Subscribable events
    public System.Action<Vector2> MovementActionPerformed;
    public System.Action AttackActionPerformed;
    public System.Action InteractPressActionPerformed;
    public System.Action InteractReleaseActionPerformed;
    public System.Action InteractHoldActionPerformed;
    public System.Action InteractHoldActionCanceled;
    public System.Action InventoryActionPerformed;
    public System.Action CancelItemOptionsPerformed;

    public bool ListenForMovement
    {
        set
        {
            if (value)
            {
                movementAction.Enable();
            } else
            {
                movementAction.Disable();
                MovementActionPerformed?.Invoke(Vector2.zero);
            }
        }
    }

    protected override void Awake()
    {
        movementAction.performed += MovementAction_performed;
        attackAction.performed += AttackAction_performed;
        interactPressAction.performed += InteractPressReleaseAction_performed;
        inventoryAction.performed += InventoryAction_performed;
        cancelItemOptionsAction.performed += CancelItemOptionsAction_performed;

        InteractPressActionPerformed += movementAction.Disable;
        InteractPressActionPerformed += () => MovementActionPerformed?.Invoke(Vector2.zero);
        InteractReleaseActionPerformed += () => ListenForMovement = true;

        Player p = FindObjectOfType<Player>();
        p.OnFindInteractable += SetInteractHoldTimer;
        p.OnLostInteractable += ResetInteractHoldTimer;
    }

    private void OnEnable()
    {
        movementAction.Enable();
        attackAction.Enable();
        interactPressAction.Enable();
        inventoryAction.Enable();
        cancelItemOptionsAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
        attackAction.Disable();
        interactPressAction.Disable();
        inventoryAction.Disable();
        cancelItemOptionsAction.Disable();
    }

    private void OnDestroy()
    {
        movementAction.performed -= MovementAction_performed;
        interactPressAction.performed -= InteractPressReleaseAction_performed;
        inventoryAction.performed -= InventoryAction_performed;
    }

    private void SetInteractHoldTimer(IInteractable obj)
    {
        float time = obj.HoldTime;

        if (time == 0) return;

        InputAction holdAction = new InputAction(type: InputActionType.Button, interactions: $"hold(duration={time})");
        holdAction.AddBinding(interactPressAction.controls[0]);

        if (interactHoldAction == holdAction) return;

        interactHoldAction = holdAction;

        interactHoldAction.performed += InteractHoldAction_performed;
        interactHoldAction.canceled += InteractHoldAction_canceled;

        holdAction.Enable();
    }

    private void ResetInteractHoldTimer()
    {
        if (interactHoldAction == null) return;

        interactHoldAction.performed -= InteractHoldAction_performed;
        interactHoldAction.canceled -= InteractHoldAction_canceled;
    }

    private void MovementAction_performed(InputAction.CallbackContext obj)
    {
        Vector2 input = obj.ReadValue<Vector2>();
        //Debug.Log(input);
        MovementActionPerformed?.Invoke(input);
    }

    private void AttackAction_performed(InputAction.CallbackContext obj)
    {
        AttackActionPerformed?.Invoke();
    }

    private void InteractPressReleaseAction_performed(InputAction.CallbackContext obj)
    {
        bool isPressed = obj.ReadValueAsButton();
        if (isPressed)
        {
            InteractPressActionPerformed?.Invoke();
        } else if (UIManager.State == UIState.None)
        {
            InteractReleaseActionPerformed?.Invoke();
        }
    }

    private void InteractHoldAction_performed(InputAction.CallbackContext obj)
    {
        InteractHoldActionPerformed?.Invoke();
    }

    private void InteractHoldAction_canceled(InputAction.CallbackContext obj)
    {
        InteractHoldActionCanceled?.Invoke();
        ResetInteractHoldTimer();
    }

    private void InventoryAction_performed(InputAction.CallbackContext obj)
    {
        InventoryActionPerformed?.Invoke();
    }

    private void CancelItemOptionsAction_performed(InputAction.CallbackContext obj)
    {
        CancelItemOptionsPerformed?.Invoke();
    }
}
