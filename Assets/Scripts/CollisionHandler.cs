using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int loadDelay = 1;
    [SerializeField] float forceThreshold = 5;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;

    [SerializeField] ParticleSystem winParticles;
    [SerializeField] ParticleSystem loseParticles;

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
                StartSuccessSequence(collision.impulse.y);
                break;
            default:
                StartCrashSequence();
                return;
        }
    }

    private void StartSuccessSequence(float verticalImpactForce)
    {
        Debug.Log($"Impact: {verticalImpactForce}");
        Debug.Log($"Threshold: {forceThreshold}");

        if (verticalImpactForce > forceThreshold)
        {
            StartCrashSequence();
            return;
        }

        StartNextLevelSequence();
    }

    private void StartNextLevelSequence()
    {
        InvokeTransitionSequence(winSound, winParticles, nameof(LoadNextLevel));
    }

    private void InvokeTransitionSequence(AudioClip audioClip, ParticleSystem particles, string actionName)
    {
        _isTransitioning = true;
        particles.Play();
        _audioSource.Stop();
        StopPlayerInteraction();
        PlaySound(audioClip);
        Invoke(actionName, loadDelay);
    }

    private void StartCrashSequence()
    {
        InvokeTransitionSequence(loseSound, loseParticles, nameof(ReloadLevel));
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