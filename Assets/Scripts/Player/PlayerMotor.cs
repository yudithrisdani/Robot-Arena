using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Dipanggil sekali di awal
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController tidak ditemukan! Tambahkan ke GameObject Player.");
            enabled = false;
        }
    }

    // Dipanggil setiap frame
    void Update()
    {
        
        // Cek apakah player menyentuh tanah
        isGrounded = controller.isGrounded;

        // Reset velocity Y saat grounded untuk menjaga player tetap menempel
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Tambahkan gravitasi
        playerVelocity.y += gravity * Time.deltaTime;

        // Jalankan movement berbasis vertical di sini jika tidak pakai input terpisah
        // controller.Move(playerVelocity * Time.deltaTime);  //--> Tidak lagi dipakai di sini
    }

    // Fungsi untuk menggerakkan player (dipanggil dari InputManager)
    public void ProcessMove(Vector2 input)
    {
        // Buat vector arah gerakan dari input
        Vector3 moveDirection = transform.TransformDirection(new Vector3(input.x, 0, input.y));

        // Gabungkan movement horizontal dan vertical
        Vector3 finalVelocity = moveDirection * speed + new Vector3(0, playerVelocity.y, 0);

        // Pindahkan player
        controller.Move(finalVelocity * Time.deltaTime);
    }

    // Fungsi untuk melompat (dipanggil dari InputManager atau Input System)
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
