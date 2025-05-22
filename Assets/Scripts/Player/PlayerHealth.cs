using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  // Import SceneManager untuk restart game

public class PlayerHealth : MonoBehaviour {
    [Header("Audio Settings")]
    public AudioClip deathClip;
    private AudioSource audioSource;
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float health;
    private float lerpTimer;

    [Header("Health UI")]
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay Settings")]
    public Image overlay;        // UI Image untuk efek damage (contoh: layar merah)
    public float duration = 0.5f;  // Waktu sebelum mulai fade out
    public float fadeSpeed = 2f;   // Kecepatan fade out overlay
    private float durationTimer;

    [Header("Game Over Settings")]
    public GameObject gameOverPanel;  // Panel Game Over yang akan muncul
    private bool isGameOver = false;  // Status apakah game sudah selesai
    public AudioClip hitClip;
    void Start()
    {
        // Inisialisasi darah
        health = maxHealth;
        UpdateHealthUI();

        // Pastikan overlay mulai dalam keadaan transparan
        if (overlay != null)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        }

        // Pastikan game over panel tidak aktif saat awal
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update() {
        // Clamp darah agar tidak melebihi batas
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        // Proses fade out overlay damage jika ada
        if (overlay != null && overlay.color.a > 0) {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration) {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }

        // Cek jika darah sudah habis dan game belum berakhir
        if (health <= 0 && !isGameOver) {
            GameOver();
        }
    }

    public void UpdateHealthUI() {
        // Update health bar (dengan animasi chip effect)
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction) {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / 2f; // chipSpeed bisa diatur jika diinginkan
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction) {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / 2f;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }

        // Update teks kesehatan
        if (healthText != null) {
            healthText.text = Mathf.RoundToInt(health) + " / " + Mathf.RoundToInt(maxHealth);
        }
    }

    // Fungsi untuk mengurangi darah dan memunculkan efek overlay damage
    public void TakeDamage(float damage)
    {
        Debug.Log("Player terkena damage: " + damage);
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;

        if (overlay != null)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        }
        if (hitClip != null && audioSource != null) {
        audioSource.PlayOneShot(hitClip);
        }
    }

    // Fungsi untuk menambah darah
    public void RestoreHealth(float healAmount) {
        Debug.Log("Player mendapatkan heal: " + healAmount);
        health += healAmount;
        lerpTimer = 0f;
    }

    // Fungsi Game Over
    private void GameOver()
    {
        isGameOver = true;  // Tandai bahwa game telah selesai

        // Menampilkan panel Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        // Mainkan audio kematian
        if (deathClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathClip);
        }

        // Menghentikan waktu agar tidak ada yang berjalan
        //Time.timeScale = 0f;  // Menghentikan semua aktivitas di game
    }

    // Fungsi untuk restart game
    /*public void RestartGame() {
        Time.timeScale = 1f;  // Mengaktifkan kembali waktu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Memuat ulang scene yang aktif
   }*/

    // Fungsi untuk keluar dari game
    public void ExitGame() {
        Debug.Log("Exiting game...");
        Application.Quit();  // Keluar dari game (hanya berfungsi di build, bukan editor)
    }
}
