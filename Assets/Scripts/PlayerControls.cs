//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""MatchControlls"",
            ""id"": ""81eb967c-611f-4f00-95a2-da1118dbaf63"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b0d458c8-8814-4bd5-9b8b-d83cb100d59a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""51793048-80fd-418c-9929-4d1750ef5c1d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""57b9ab1e-0e8b-448c-929a-1b1dd7c7a82a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""4b9bc590-b26a-4e32-9084-810336c9501b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""b8e3caa0-ac5a-46a2-bd7e-fe7bcf7f7ba1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""89e747dc-ef03-4eee-8105-df6b12bef833"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b41a8225-caff-4a20-8b19-2c06125541f9"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/hat/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6b8e1c26-42d8-4a56-a90b-07d7fab4843b"",
                    ""path"": ""<HID::ZEROPLUS P4 Wired Gamepad>/hat/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bcf9a250-3c9b-4cb8-95d7-282df04de076"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MatchControlls
        m_MatchControlls = asset.FindActionMap("MatchControlls", throwIfNotFound: true);
        m_MatchControlls_Movement = m_MatchControlls.FindAction("Movement", throwIfNotFound: true);
        m_MatchControlls_Attack = m_MatchControlls.FindAction("Attack", throwIfNotFound: true);
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

    // MatchControlls
    private readonly InputActionMap m_MatchControlls;
    private IMatchControllsActions m_MatchControllsActionsCallbackInterface;
    private readonly InputAction m_MatchControlls_Movement;
    private readonly InputAction m_MatchControlls_Attack;
    public struct MatchControllsActions
    {
        private @PlayerControls m_Wrapper;
        public MatchControllsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MatchControlls_Movement;
        public InputAction @Attack => m_Wrapper.m_MatchControlls_Attack;
        public InputActionMap Get() { return m_Wrapper.m_MatchControlls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MatchControllsActions set) { return set.Get(); }
        public void SetCallbacks(IMatchControllsActions instance)
        {
            if (m_Wrapper.m_MatchControllsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MatchControllsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MatchControllsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MatchControllsActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_MatchControllsActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_MatchControllsActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_MatchControllsActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_MatchControllsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public MatchControllsActions @MatchControlls => new MatchControllsActions(this);
    public interface IMatchControllsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}