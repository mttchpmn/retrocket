using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrustFactor = 100f;
    [SerializeField] private float rotationThrustFactor = 50f;
    [SerializeField] AudioClip mainEngineSound;

    [SerializeField] private ParticleSystem mainBoosterParticles;
    [SerializeField] private ParticleSystem leftBoosterParticles;
    [SerializeField] private ParticleSystem rightBoosterParticles;


    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _audioSource.Stop();
            mainBoosterParticles.Stop();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayMainBoosterSound();

            if (!mainBoosterParticles.isPlaying)
                mainBoosterParticles.Play();

            ApplyThrust(Vector3.up, mainThrustFactor);
        }
    }

    private void PlayMainBoosterSound()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngineSound);
        }
    }

    private void ProcessRotation()
    {
        HandleRotate(KeyCode.LeftArrow, rightBoosterParticles, Vector3.forward);
        HandleRotate(KeyCode.RightArrow, leftBoosterParticles, Vector3.back);
    }

    private void HandleRotate(KeyCode keyCode, ParticleSystem particles, Vector3 vector)
    {
        if (Input.GetKeyUp(keyCode))
        {
            particles.Stop();
        }

        if (Input.GetKey(keyCode))
        {
            if (!particles.isPlaying)
                particles.Play();

            ApplyRotationThrust(vector);
        }
    }

    private void ApplyRotationThrust(Vector3 vector)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(vector * rotationThrustFactor * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }

    private void ApplyThrust(Vector3 vector, float thrustFactor)
    {
        var thrustForce = vector * thrustFactor;
        _rigidbody.AddRelativeForce(thrustForce * Time.deltaTime);
    }
}