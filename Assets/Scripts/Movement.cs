using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrustFactor = 100f;
    [SerializeField] private float rotationThrustFactor = 50f;
    [SerializeField] AudioClip mainEngineSound;

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
            _audioSource.Stop();
        
        if (!Input.GetKey(KeyCode.UpArrow))
            return;

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngineSound);
        }

        ApplyThrust(Vector3.up, mainThrustFactor);
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyRotationThrust(Vector3.forward);
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyRotationThrust(Vector3.back);
            return;
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