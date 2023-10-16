using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    [SerializeField] private Transform _visualTarget;
    [SerializeField] private Vector3 _localAxis;
    [SerializeField] private float _minHeight;
    [SerializeField] private float _resetSpeed = 5; 
    [SerializeField] private float _followAngleTreshold = 45;

    private XRBaseInteractable _interactable;
    private bool _isFollowing = false;
    private bool _freeze = false;
    private Vector3 _offset;
    private Vector3 _startPos;
    private Transform _pokeAttachTransform;

    void Start()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        _interactable.hoverEntered.AddListener(Follow);
        _interactable.hoverExited.AddListener(ResetFollow);
        _interactable.selectEntered.AddListener(Freeze);

        _startPos = _visualTarget.localPosition;
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            
            _pokeAttachTransform = interactor.attachTransform;
            _offset = _visualTarget.position - _pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(_offset, _visualTarget.TransformDirection(_localAxis));

            if(pokeAngle < _followAngleTreshold)
            {
                _isFollowing = true;
                _freeze = false;
            }
        }
    }

    public void ResetFollow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            _isFollowing = false;
            _freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            _freeze = true;
        }
    }

    void Update()
    {
        if (_freeze)
            return;

        if (_isFollowing)
        {
            Vector3 localTargetPos = _visualTarget.InverseTransformPoint(_pokeAttachTransform.position + _offset);
            Vector3 constraintLocalTargetPos = Vector3.Project(localTargetPos, _localAxis);
            // var position = _visualTarget.localPosition;
            _visualTarget.position = _visualTarget.TransformPoint(constraintLocalTargetPos);
            
            // var getYPos = position.y;
            // getYPos = Mathf.Clamp(getYPos, _minHeight-_startPos.y, _startPos.y);
            // // print(getYpos);
            // position = new Vector3(_startPos.x, getYPos, _startPos.z);
            // _visualTarget.localPosition = position;
        }
        else
        {
            _visualTarget.localPosition = Vector3.Lerp(_visualTarget.localPosition, _startPos, Time.deltaTime * _resetSpeed);

            if(_visualTarget.localPosition.y > _startPos.y)
            {
                _visualTarget.localPosition = _startPos;
            }
            
        }
        
    }
}
