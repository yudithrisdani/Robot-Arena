using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    public PlayerInput.OnFootActions OnFoot => onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
 

    // Dipanggil saat objek dibuat
    void Awake() 
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Dipanggil setiap frame physics
    void FixedUpdate() {
        // Beri input movement ke PlayerMotor
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate() {
        // Beri input movement ke PlayerMotor
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    // Aktifkan input saat script aktif
    private void OnEnable() {
        onFoot.Enable();
    }

    // Nonaktifkan input saat script nonaktif
    private void OnDisable() {
        onFoot.Disable();
    }
}
