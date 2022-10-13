//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Scripts/InputSystem/PlayerContrl.inputactions
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

public partial class @PlayerContrl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerContrl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerContrl"",
    ""maps"": [
        {
            ""name"": ""PlayerControl"",
            ""id"": ""602bfad6-0811-400a-8b1f-d9851bfced40"",
            ""actions"": [
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""c2b0db80-6970-445c-998a-7fd62ba34274"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""f24cea4e-9345-408a-89fe-3c283964a411"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""af8884e2-e8a7-418f-906f-169e10786a34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""3f247725-41b1-46aa-a1f1-5faa6cd84e9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""65e53b3b-c9d2-4bfb-9677-5211808e02ce"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00fa53ab-d928-44c6-bdc3-dcf1e0494b85"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34bd1cad-eb88-4aff-8da1-82faed581f8c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acaed18f-cd85-4d34-abdb-987b54b89450"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControl
        m_PlayerControl = asset.FindActionMap("PlayerControl", throwIfNotFound: true);
        m_PlayerControl_Confirm = m_PlayerControl.FindAction("Confirm", throwIfNotFound: true);
        m_PlayerControl_Cancel = m_PlayerControl.FindAction("Cancel", throwIfNotFound: true);
        m_PlayerControl_RotateLeft = m_PlayerControl.FindAction("RotateLeft", throwIfNotFound: true);
        m_PlayerControl_RotateRight = m_PlayerControl.FindAction("RotateRight", throwIfNotFound: true);
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

    // PlayerControl
    private readonly InputActionMap m_PlayerControl;
    private IPlayerControlActions m_PlayerControlActionsCallbackInterface;
    private readonly InputAction m_PlayerControl_Confirm;
    private readonly InputAction m_PlayerControl_Cancel;
    private readonly InputAction m_PlayerControl_RotateLeft;
    private readonly InputAction m_PlayerControl_RotateRight;
    public struct PlayerControlActions
    {
        private @PlayerContrl m_Wrapper;
        public PlayerControlActions(@PlayerContrl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm => m_Wrapper.m_PlayerControl_Confirm;
        public InputAction @Cancel => m_Wrapper.m_PlayerControl_Cancel;
        public InputAction @RotateLeft => m_Wrapper.m_PlayerControl_RotateLeft;
        public InputAction @RotateRight => m_Wrapper.m_PlayerControl_RotateRight;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlActions instance)
        {
            if (m_Wrapper.m_PlayerControlActionsCallbackInterface != null)
            {
                @Confirm.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnConfirm;
                @Cancel.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnCancel;
                @RotateLeft.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnRotateLeft;
                @RotateLeft.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnRotateLeft;
                @RotateRight.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnRotateRight;
                @RotateRight.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnRotateRight;
                @RotateRight.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnRotateRight;
            }
            m_Wrapper.m_PlayerControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @RotateLeft.started += instance.OnRotateLeft;
                @RotateLeft.performed += instance.OnRotateLeft;
                @RotateLeft.canceled += instance.OnRotateLeft;
                @RotateRight.started += instance.OnRotateRight;
                @RotateRight.performed += instance.OnRotateRight;
                @RotateRight.canceled += instance.OnRotateRight;
            }
        }
    }
    public PlayerControlActions @PlayerControl => new PlayerControlActions(this);
    public interface IPlayerControlActions
    {
        void OnConfirm(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnRotateLeft(InputAction.CallbackContext context);
        void OnRotateRight(InputAction.CallbackContext context);
    }
}
