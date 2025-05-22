using System.Collections;
using UnityEngine;

public class HealthBox : MonoBehaviour {
    public float healAmount = 20f;         // Jumlah darah yang ditambah
    public float respawnTime = 3f;         // Waktu muncul kembali
    private bool isUsed = false;

    private Renderer rend;
    private Collider col;

    void Start() {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other) {
        if (!isUsed && other.CompareTag("Player")) {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null) {
                ph.RestoreHealth(healAmount);
                StartCoroutine(RespawnHealthBox());
            }
        }
    }

    IEnumerator RespawnHealthBox() {
        isUsed = true;

        // Sembunyikan visual dan nonaktifkan collider
        if (rend != null) rend.enabled = false;
        if (col != null) col.enabled = false;

        // Tunggu beberapa detik
        yield return new WaitForSeconds(respawnTime);

        // Munculkan kembali
        if (rend != null) rend.enabled = true;
        if (col != null) col.enabled = true;
        isUsed = false;
    }
}
