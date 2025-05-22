using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnowPos;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }

    public Path path;
    public GameObject debugsphere;
    
    [Header("Sight Values")]
    public float sightDistance = 200f;
    public float fieldOfView = 200f;
    public float eyeHeight;

    [Header("Weapone Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate = 2f;

    // Tambahan audio
    [Header("Audio")]
    public AudioClip gunShotClip;
    private AudioSource audioSource;
    private float nextFireTime = 0f;

    [SerializeField]
    private string currentState;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

        // Inisialisasi AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set volume audio ke 0.3 (lebih kecil)
        audioSource.volume = 0.3f;

        // Pastikan fireRate minimal sama durasi clip agar suara tidak overlap
        if (gunShotClip != null && fireRate < gunShotClip.length)
        {
            fireRate = gunShotClip.length;
        }
        nextFireTime = 0f;
    }

    void Update()
    {
        // Percepat cooldown tembak dengan mengurangi 2 detik tapi jangan kurang dari 0.1f
        float adjustedFireRate = Mathf.Max(fireRate, 0.1f);

        if (CanSeePlayer() && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + adjustedFireRate;
        }

        currentState = stateMachine.activeState.ToString();
        debugsphere.transform.position = lastKnowPos;
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleTopPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleTopPlayer >= -fieldOfView && angleTopPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void Shoot()
    {
        if (gunShotClip != null && audioSource != null)
        {
            // Cek jika audio sedang tidak diputar, baru mainkan audio tembakan
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(gunShotClip);
            }
        }
        Debug.Log("Enemy menembak!");
    }

    // Method untuk reset cooldown tembak kapan saja, misal dipanggil saat exit atau reload scene
    public void ResetFireTime()
    {
        nextFireTime = 0f;
    }
}
