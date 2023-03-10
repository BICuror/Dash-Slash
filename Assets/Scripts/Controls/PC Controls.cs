//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/Scripts/Controls/PC Controls.inputactions
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

public partial class @PCControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PCControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PC Controls"",
    ""maps"": [
        {
            ""name"": ""PC"",
            ""id"": ""626636e5-2894-46ad-9435-f799e9340227"",
            ""actions"": [
                {
                    ""name"": ""PointerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""4c19739d-86ef-470b-8b93-ad85c850cf60"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ActivateAbility"",
                    ""type"": ""Button"",
                    ""id"": ""46690839-96e9-4bbc-8827-cf8d21559487"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""075bce8f-27a4-4e6c-82a5-b1ff429d1777"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""67ebf225-08c9-4729-a306-3e63daa86549"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PointerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a897add-f217-43bf-9991-f083feb2fa72"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""398625bf-8aa8-477f-8a2c-b7323dd24cf3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_PointerPosition = m_PC.FindAction("PointerPosition", throwIfNotFound: true);
        m_PC_ActivateAbility = m_PC.FindAction("ActivateAbility", throwIfNotFound: true);
        m_PC_PauseGame = m_PC.FindAction("PauseGame", throwIfNotFound: true);
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

    // PC
    private readonly InputActionMap m_PC;
    private IPCActions m_PCActionsCallbackInterface;
    private readonly InputAction m_PC_PointerPosition;
    private readonly InputAction m_PC_ActivateAbility;
    private readonly InputAction m_PC_PauseGame;
    public struct PCActions
    {
        private @PCControls m_Wrapper;
        public PCActions(@PCControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PointerPosition => m_Wrapper.m_PC_PointerPosition;
        public InputAction @ActivateAbility => m_Wrapper.m_PC_ActivateAbility;
        public InputAction @PauseGame => m_Wrapper.m_PC_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void SetCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterface != null)
            {
                @PointerPosition.started -= m_Wrapper.m_PCActionsCallbackInterface.OnPointerPosition;
                @PointerPosition.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnPointerPosition;
                @PointerPosition.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnPointerPosition;
                @ActivateAbility.started -= m_Wrapper.m_PCActionsCallbackInterface.OnActivateAbility;
                @ActivateAbility.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnActivateAbility;
                @ActivateAbility.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnActivateAbility;
                @PauseGame.started -= m_Wrapper.m_PCActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PCActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PCActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_PCActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PointerPosition.started += instance.OnPointerPosition;
                @PointerPosition.performed += instance.OnPointerPosition;
                @PointerPosition.canceled += instance.OnPointerPosition;
                @ActivateAbility.started += instance.OnActivateAbility;
                @ActivateAbility.performed += instance.OnActivateAbility;
                @ActivateAbility.canceled += instance.OnActivateAbility;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public PCActions @PC => new PCActions(this);
    public interface IPCActions
    {
        void OnPointerPosition(InputAction.CallbackContext context);
        void OnActivateAbility(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
