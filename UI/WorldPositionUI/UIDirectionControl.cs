using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool _UseRelativeRotation = true;       // Use relative rotation should be used for this gameobject?


    private Quaternion _RelativeRotation;          // The local rotatation at the start of the scene.


    private void Start()
    {
        _RelativeRotation = transform.parent.localRotation;
    }


    private void Update()
    {
        if (_UseRelativeRotation)
            transform.rotation = _RelativeRotation;
    }
}
