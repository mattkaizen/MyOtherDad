using System.Collections.Generic;
using UnityEngine;

namespace CustomInput
{
    [CreateAssetMenu(fileName = "InputControlManager", menuName = "Input/Action Control Manager", order = 0)]
    public class InputActionControlManagerData : ScriptableObject
    {
        public InputActionControlData MoveActionControl
        {
            get => moveActionControl;
            set => moveActionControl = value;
        }

        public InputActionControlData RunActionControl
        {
            get => runActionControl;
            set => runActionControl = value;
        }

        public InputActionControlData LookAtActionControl
        {
            get => lookAtActionControl;
            set => lookAtActionControl = value;
        }

        public InputActionControlData LookAtAssetActionControl
        {
            get => lookAtAssetActionControl;
            set => lookAtAssetActionControl = value;
        }

        public InputActionControlData InteractActionControl
        {
            get => interactActionControl;
            set => interactActionControl = value;
        }

        public InputActionControlData GetUpActionControl
        {
            get => getUpActionControl;
            set => getUpActionControl = value;
        }

        public InputActionControlData ThrowItemActionControl
        {
            get => throwItemActionControl;
            set => throwItemActionControl = value;
        }

        public InputActionControlData SwitchNextItemActionControl
        {
            get => switchNextItemActionControl;
            set => switchNextItemActionControl = value;
        }

        public InputActionControlData SwitchPreviousItemActionControl
        {
            get => switchPreviousItemActionControl;
            set => switchPreviousItemActionControl = value;
        }

        public InputActionControlData PaintActionControl
        {
            get => paintActionControl;
            set => paintActionControl = value;
        }
        
        [SerializeField] private InputActionControlData moveActionControl;
        [SerializeField] private InputActionControlData runActionControl;
        [SerializeField] private InputActionControlData lookAtActionControl;
        [SerializeField] private InputActionControlData lookAtAssetActionControl;
        [SerializeField] private InputActionControlData interactActionControl;
        [SerializeField] private InputActionControlData getUpActionControl;
        [SerializeField] private InputActionControlData throwItemActionControl;
        [SerializeField] private InputActionControlData switchNextItemActionControl;
        [SerializeField] private InputActionControlData switchPreviousItemActionControl;
        [SerializeField] private InputActionControlData paintActionControl;
        
        private List<InputActionControlData> _inputControlsData = new List<InputActionControlData>();

        private void OnEnable()
        {
            _inputControlsData.Clear();
            
            _inputControlsData.Add(moveActionControl);
            _inputControlsData.Add(runActionControl);
            _inputControlsData.Add(lookAtActionControl);
            _inputControlsData.Add(lookAtAssetActionControl);
            _inputControlsData.Add(interactActionControl);
            _inputControlsData.Add(getUpActionControl);
            _inputControlsData.Add(throwItemActionControl);
            _inputControlsData.Add(switchNextItemActionControl);
            _inputControlsData.Add(switchPreviousItemActionControl);
            _inputControlsData.Add(paintActionControl);
        }

        private void OnDisable()
        {
            _inputControlsData.Clear();
        }

        public void EnableAllInputs()
        {
            foreach (var inputControl in _inputControlsData)
            {
                inputControl.EnableInput();
            }
        }
        
        public void DisableAllInputs()
        {
            foreach (var inputControl in _inputControlsData)
            {
                inputControl.DisableInput();
            }
        }

    }
}