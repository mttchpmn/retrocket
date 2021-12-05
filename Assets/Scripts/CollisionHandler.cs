using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int loadDelay = 1;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    
    private AudioSource _audioSource;
    private int _activeLevelIndex;
    private int _nextLevelIndex;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _activeLevelIndex = SceneManager.GetActiveScene().buildIndex;
        _nextLevelIndex = _activeLevelIndex.Equals(SceneManager.sceneCountInBuildSettings - 1)
            ? 0
            : _activeLevelIndex + 1;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                return;
        }
    }

    private void StartSuccessSequence()
    {
        StopPlayerInteraction();
        PlaySound(winSound);
        Invoke(nameof(LoadNextLevel), loadDelay);
    }
    
    private void StartCrashSequence()
    {
        StopPlayerInteraction();
        PlaySound(loseSound);
        Invoke(nameof(ReloadLevel), loadDelay);
    }

    private void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    private void StopPlayerInteraction()
    {
        GetComponent<Movement>().enabled = false;
    }
    
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevelIndex);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(_activeLevelIndex);
    }
}