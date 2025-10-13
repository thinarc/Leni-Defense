using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        private PlayerInput _input;
        
        private List<BindedAction> _actions;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            _input = GetComponent<PlayerInput>();
            _actions = new List<BindedAction>();
        }

        public void GetInput(string actionName, Action<InputAction.CallbackContext> callback, bool performed = true)
        {
            var action = _input.actions[actionName];
            if (action != null)
            {
                if (performed) action.performed += callback;
                else action.canceled += callback;
                
                _actions.Add(new BindedAction
                {
                    Action = action,
                    Callback = callback,
                    Performed = performed
                });
            }
            else Debug.LogWarning($"Action '{actionName}' not found.");
        }
        
        public void GetAllInput(string actionName, Action<InputAction.CallbackContext> callback)
        {
            GetInput(actionName, callback);
            GetInput(actionName, callback, false);
        }
        
        private void OnEnable()
        {
            _input.enabled = true;
        }
        
        private void OnDisable()
        {
            _input.enabled = false;
            foreach (var bindedAction in _actions)
            {
                if (bindedAction.Performed) bindedAction.Action.performed -= bindedAction.Callback;
                else bindedAction.Action.canceled -= bindedAction.Callback;
            }
            _actions.Clear();
        }
    }
    
    public class BindedAction
    {
        public InputAction Action;
        public Action<InputAction.CallbackContext> Callback;
        public bool Performed;
    }
}
