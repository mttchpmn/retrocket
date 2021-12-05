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
    private bool _isTransitioning = false;

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
        if (_isTransitioning) return;

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
        InvokeTransitionSequence(winSound, nameof(LoadNextLevel));
    }

    private void InvokeTransitionSequence(AudioClip audioClip, string actionName)
    {
        _isTransitioning = true;
        _audioSource.Stop();
        StopPlayerInteraction();
        PlaySound(audioClip);
        Invoke(actionName, loadDelay);
    }

    private void StartCrashSequence()
    {
        InvokeTransitionSequence(loseSound, nameof(ReloadLevel));
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