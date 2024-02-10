using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomInput
{
    [CreateAssetMenu(fileName = "InputControl", menuName = "Input/Action Control", order = 0)]
    public class InputActionControlData : ScriptableObject
    {
        public InputAction Input
        {
            get => _input;
            set => _input = value;
        }

        public VoidEventChannelData InputEnabled => inputEnabled;

        public VoidEventChannelData InputDisabled => inputDisabled;

        [SerializeField] private VoidEventChannelData inputEnabled;
        [SerializeField] private VoidEventChannelData inputDisabled;

        private InputAction _input;

        public void EnableInput()
        {
            _input?.Enable();
            
            if (inputEnabled != null)
                inputEnabled.RaiseEvent();
            else
                Debug.LogWarning($"Empty {typeof(VoidEventChannelData)} in {this.name}");
            
            Debug.Log($"Input: {_input.name} , state: {_input.enabled}");
        }

        public void DisableInput()
        {
            _input?.Disable();

            if (inputDisabled != null)
                inputDisabled.RaiseEvent();
            else
                Debug.LogWarning($"Empty {typeof(VoidEventChannelData)} in {this.name}");
            
            Debug.Log($"Input: {_input.name} , state: {_input.enabled}");

        }
    }
}