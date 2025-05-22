using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
{
    GameObject rootObj = gameObject.transform.root.gameObject;
    DontDestroyOnLoad(rootObj);

    // Lanjutkan akses audio source
    audioSource = GetComponentInChildren<AudioSource>();

    if (audioSource == null)
    {
        Debug.LogError("AudioSource tidak ditemukan di child " + rootObj.name);
        audioSource = rootObj.AddComponent<AudioSource>();
    }

    if (!audioSource.isPlaying)
    {
        audioSource.Play();
    }
}

    void Start()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
}
