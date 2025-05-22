using System.Collections;
using UnityEngine;

public class Keypad : Interactable 
{
    [SerializeField]
    private GameObject ChestV1;

    [SerializeField]
    private GameObject MissionComplete;

    [SerializeField]
    private AudioClip chestOpenClip; // 🎵 Suara buka chest

    private bool chestOpen;
    private AudioSource audioSource;

    void Start() 
    {
        // Coba ambil AudioSource dari GameObject ini
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Kalau tidak ada, tambahkan
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    protected override void Interact()
    {
        chestOpen = !chestOpen;

        // 🔓 Animasi membuka / menutup chest
        ChestV1.GetComponent<Animator>().SetBool("IsOpen", chestOpen);

        // ✅ Aktifkan MissionComplete jika terbuka
        if (chestOpen && MissionComplete != null)
        {
            MissionComplete.SetActive(true);
        }

        // 🔊 Mainkan suara hanya saat chest dibuka
        if (chestOpen && chestOpenClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(chestOpenClip);
        }
    }
}
