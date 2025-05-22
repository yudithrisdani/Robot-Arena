using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private bool hasHit = false; // mencegah multiple damage

    private void OnCollisionEnter(Collision collision) {
        if (hasHit) return; // jika sudah pernah kena, abaikan

        Transform hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player")) {
            Debug.Log("Hit player");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(15);
        }

        hasHit = true; // tandai sudah kena
        Destroy(gameObject); // hancurkan peluru
    }
}
