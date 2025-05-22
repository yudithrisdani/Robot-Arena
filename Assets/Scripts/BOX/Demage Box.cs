using UnityEngine;

public class DamageBox : MonoBehaviour {
    public float damageAmount = 20f; // Jumlah damage yang diberikan

    void OnTriggerEnter(Collider other) {
        // Pastikan objek yang menyentuh box adalah pemain
        if (other.CompareTag("Player")) {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null) {
                ph.TakeDamage(damageAmount);
            }
        }
    }
}
