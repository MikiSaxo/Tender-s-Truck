using UnityEngine;
using UnityEngine.Serialization;

public class MouseFollow : MonoBehaviour
{
    // Set the speed at which the object follows the mouse pointer.
    // [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(_target.position.x, _target.position.y, -Camera.main.transform.position.z));

        Debug.Log(transform.position);
    }
}