using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickClip;
    [SerializeField] AudioClip boomClip;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    public void PlayClick()
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

    public void PlayBoom()
    {
        audioSource.clip = boomClip;
        audioSource.Play();
    }
}
