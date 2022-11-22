using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputManager : MonoBehaviour
{
    [Serializable]
    public struct VRInput
    {
        [SerializeField] private InputActionReference inputRef;

        public bool GetButton { get => inputRef.action.IsPressed(); }
        public bool GetButtonDown { get => inputRef.action.WasPressedThisFrame(); }
        public bool GetButtonUp { get => inputRef.action.WasReleasedThisFrame(); }
    }

    private static VRInputManager instance;
    public static VRInputManager Instance { get { return instance; } }

    public VRInput LeftTrigger;
    public VRInput RightTrigger;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }
}
