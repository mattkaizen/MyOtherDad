//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/MyOtherDad/Project Settings/InputSystem Data/GameControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""127641f5-bd6c-4905-aa9c-df8021cd3a95"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""4af2dd1b-68dd-4d70-a942-d00818d6f203"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GetUp"",
                    ""type"": ""Button"",
                    ""id"": ""c9eb9238-ceea-4724-b2c6-1ab0a1efd920"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""daca987d-0744-4ad5-8d97-c08985277463"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LookAt"",
                    ""type"": ""Value"",
                    ""id"": ""593cf607-0a55-47e5-be2d-40c6e729ee67"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""a2133947-0e34-4499-9505-2dc1479a267b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Paint"",
                    ""type"": ""Button"",
                    ""id"": ""de919de7-c39b-4cc5-add8-04d69371b89a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchNextItem"",
                    ""type"": ""Button"",
                    ""id"": ""3f7d1cfd-31de-4d95-906a-ba06241af180"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchPreviousItem"",
                    ""type"": ""Button"",
                    ""id"": ""ff71fc30-6861-4332-b839-8b1c7c10f7dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThrowItem"",
                    ""type"": ""Button"",
                    ""id"": ""7bce215b-c863-4502-84fd-13ee4a28f2b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a289053a-dd1b-4167-9ece-091ac24defb8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fad02dd-2c3d-4bb9-b6c8-a09b39da697b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b9fed447-3ad6-4d69-adba-21a5a818d8eb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""36a8f333-841b-4c97-b8cc-6f17ce2c34ac"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""78b462a6-ca2c-4427-93aa-8843e268b520"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""22d9ffb2-eedc-451f-9b3c-82fb656c45e6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3b8f4835-b1c6-4678-a9f6-a456b1d1d2b9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4bde2718-be3c-4e99-8b53-660433d89384"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookAt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30fbe7b2-a405-4f98-9f6b-61e26778f8a5"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43de3319-4ea3-45bb-a969-028067683cca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Paint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""933af108-4bce-4c82-97ee-485506583ebd"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchNextItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b29c9a5-8566-43a1-a051-3d9fec22f7a2"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchPreviousItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6440c4dd-04e0-4551-bf64-35e27186d1f4"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_GetUp = m_Player.FindAction("GetUp", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_LookAt = m_Player.FindAction("LookAt", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Paint = m_Player.FindAction("Paint", throwIfNotFound: true);
        m_Player_SwitchNextItem = m_Player.FindAction("SwitchNextItem", throwIfNotFound: true);
        m_Player_SwitchPreviousItem = m_Player.FindAction("SwitchPreviousItem", throwIfNotFound: true);
        m_Player_ThrowItem = m_Player.FindAction("ThrowItem", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_GetUp;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_LookAt;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Paint;
    private readonly InputAction m_Player_SwitchNextItem;
    private readonly InputAction m_Player_SwitchPreviousItem;
    private readonly InputAction m_Player_ThrowItem;
    public struct PlayerActions
    {
        private @GameControls m_Wrapper;
        public PlayerActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @GetUp => m_Wrapper.m_Player_GetUp;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @LookAt => m_Wrapper.m_Player_LookAt;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Paint => m_Wrapper.m_Player_Paint;
        public InputAction @SwitchNextItem => m_Wrapper.m_Player_SwitchNextItem;
        public InputAction @SwitchPreviousItem => m_Wrapper.m_Player_SwitchPreviousItem;
        public InputAction @ThrowItem => m_Wrapper.m_Player_ThrowItem;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @GetUp.started += instance.OnGetUp;
            @GetUp.performed += instance.OnGetUp;
            @GetUp.canceled += instance.OnGetUp;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @LookAt.started += instance.OnLookAt;
            @LookAt.performed += instance.OnLookAt;
            @LookAt.canceled += instance.OnLookAt;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Paint.started += instance.OnPaint;
            @Paint.performed += instance.OnPaint;
            @Paint.canceled += instance.OnPaint;
            @SwitchNextItem.started += instance.OnSwitchNextItem;
            @SwitchNextItem.performed += instance.OnSwitchNextItem;
            @SwitchNextItem.canceled += instance.OnSwitchNextItem;
            @SwitchPreviousItem.started += instance.OnSwitchPreviousItem;
            @SwitchPreviousItem.performed += instance.OnSwitchPreviousItem;
            @SwitchPreviousItem.canceled += instance.OnSwitchPreviousItem;
            @ThrowItem.started += instance.OnThrowItem;
            @ThrowItem.performed += instance.OnThrowItem;
            @ThrowItem.canceled += instance.OnThrowItem;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @GetUp.started -= instance.OnGetUp;
            @GetUp.performed -= instance.OnGetUp;
            @GetUp.canceled -= instance.OnGetUp;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @LookAt.started -= instance.OnLookAt;
            @LookAt.performed -= instance.OnLookAt;
            @LookAt.canceled -= instance.OnLookAt;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Paint.started -= instance.OnPaint;
            @Paint.performed -= instance.OnPaint;
            @Paint.canceled -= instance.OnPaint;
            @SwitchNextItem.started -= instance.OnSwitchNextItem;
            @SwitchNextItem.performed -= instance.OnSwitchNextItem;
            @SwitchNextItem.canceled -= instance.OnSwitchNextItem;
            @SwitchPreviousItem.started -= instance.OnSwitchPreviousItem;
            @SwitchPreviousItem.performed -= instance.OnSwitchPreviousItem;
            @SwitchPreviousItem.canceled -= instance.OnSwitchPreviousItem;
            @ThrowItem.started -= instance.OnThrowItem;
            @ThrowItem.performed -= instance.OnThrowItem;
            @ThrowItem.canceled -= instance.OnThrowItem;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnGetUp(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnLookAt(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnPaint(InputAction.CallbackContext context);
        void OnSwitchNextItem(InputAction.CallbackContext context);
        void OnSwitchPreviousItem(InputAction.CallbackContext context);
        void OnThrowItem(InputAction.CallbackContext context);
    }
}
