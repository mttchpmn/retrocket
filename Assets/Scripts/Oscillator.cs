using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;

    private Vector3 startingPosition;

    private float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
            return;
        
        var cycles = Time.time / period; // Continually growing over time
        const float tau = Mathf.PI * 2;
        var rawSineWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        movementFactor = (rawSineWave + 1f) / 2; // Recalculated to be from 0 to 1 so its cleaner

        var offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}