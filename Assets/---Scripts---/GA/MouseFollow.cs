using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    // Set the speed at which the object follows the mouse pointer.
    public float followSpeed = 5f;

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        Debug.Log(transform.position);
    }
}