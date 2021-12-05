using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrustFactor = 100f;
    [SerializeField] private float rotationThrustFactor = 50f;
    [SerializeField] private AudioClip mainEngineSound;

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
        if (!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.UpArrow))
        {
            _audioSource.Stop();
            return;
        }

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngineSound);
        }

        Debug.Log("Space pressed");
        ApplyThrust(Vector3.up, mainThrustFactor);
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("Left pressed");
            ApplyRotationThrust(Vector3.forward);
            return;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("Right pressed");
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