using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class SauceProjection : MonoBehaviour
{
    [SerializeField] VelocityEstimator _velocityEstimator;
    
    [Space(10)]

    [SerializeField] float _minRate;
    [SerializeField] float _maxRate;
    [SerializeField] float _rateMultiplicator;

    [Space(10)]

    [SerializeField] float _minGravity;
    [SerializeField] float _maxGravity;
    [SerializeField] float _gravityMultiplicator;

    [Space(10)]

    [SerializeField] float _minLifetime;
    [SerializeField] float _maxLifetine;
    [SerializeField] float _lifetimeMultiplicator;

    [Space(10)]

    [SerializeField] float _minOrientation;
    [SerializeField] float _maxOrientation;
    [SerializeField] float _orientationMultiplicator;


    VisualEffect sauceEffect;

    private void Awake()
    {
        sauceEffect = GetComponent<VisualEffect>();

    }

    private void Update()
    {
        Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
        float magnitude = velocity.magnitude;
        
        //Set rate w/ velocity
        float crntRate = magnitude * _rateMultiplicator;

        if (crntRate > _maxRate)
            crntRate = _maxRate;
        if (crntRate <= _minRate)
            crntRate = _minRate;

        sauceEffect.SetFloat("Rate", crntRate);


        //Set projection rotation w/ direction
        float crntRotation = magnitude * _orientationMultiplicator;

        if (crntRotation > _maxOrientation)
            crntRotation = _maxOrientation;

        if (crntRotation <= _minOrientation)
        {
            sauceEffect.SetFloat("MinRotationX", 0.2f);
            sauceEffect.SetFloat("MaxRotationX", 0.2f);
            sauceEffect.SetFloat("MinRotationZ", 0.2f);
            sauceEffect.SetFloat("MaxRotationZ", 0.2f);
        }
        else
        {
            if (velocity.x > 0)
            {
                sauceEffect.SetFloat("MinRotationX", _minOrientation * -1);
                sauceEffect.SetFloat("MaxRotationX", _maxOrientation * -1);
            }
            else
            {
                sauceEffect.SetFloat("MinRotationX", _minOrientation);
                sauceEffect.SetFloat("MaxRotationX", _maxOrientation);
            }
            if (velocity.z > 0)
            {
                sauceEffect.SetFloat("MinRotationZ", _minOrientation * -1);
                sauceEffect.SetFloat("MaxRotationZ", _maxOrientation * -1);
            }
            else
            {
                sauceEffect.SetFloat("MinRotationZ", _minOrientation);
                sauceEffect.SetFloat("MaxRotationZ", _maxOrientation);
            }
        }

        //Set gravity w/ velocity
        float crntGravity = Mathf.Abs(magnitude * _gravityMultiplicator) * -1;

        if (crntGravity > _minGravity)
            crntGravity = _minGravity;
        if (crntGravity <= _maxGravity)
            crntGravity = _maxGravity;

        sauceEffect.SetFloat("GravityForce", crntGravity);


        //Set gravity w/ velocity
        float crntLifetime = Mathf.Abs(magnitude * _lifetimeMultiplicator);

        if (crntLifetime > _maxGravity)
            crntLifetime = _maxGravity;
        if (crntLifetime <= _minGravity)
            crntLifetime = _minGravity;

        sauceEffect.SetFloat("Lifetime", crntLifetime);

    }





}
