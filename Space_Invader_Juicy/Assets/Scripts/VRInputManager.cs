using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRInputManager : MonoBehaviour
{
    [Serializable] public struct VRInput
    {
        [SerializeField] private InputActionReference inputRef;

        public bool GetButton { get => inputRef.action.IsPressed(); }
        public bool GetButtonDown { get => inputRef.action.WasPressedThisFrame(); }
        public bool GetButtonUp { get => inputRef.action.WasReleasedThisFrame(); }
    }
    [Serializable] public struct VRAxis
    {
        [SerializeField] private InputActionReference inputRef;

        public Vector2 Direction { get => inputRef.action.ReadValue<Vector2>(); }
        public float Horizontal { get => Direction.x; }
        public float Vertical { get => Direction.y; }
    }

    private static VRInputManager instance;
    public static VRInputManager Instance { get { return instance; } }

    public VRInput LeftTrigger;
    public VRInput RightTrigger;
    public VRInput LefttGrip;
    public VRInput RightGrip;

    public VRAxis LeftStick;
    public VRAxis RightStick;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }
}
