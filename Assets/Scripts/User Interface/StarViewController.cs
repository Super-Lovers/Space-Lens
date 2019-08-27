using UnityEngine;

public class StarViewController : MonoBehaviour
{
    [SerializeField]
    private Transform _starView = null;

    // Parameters of the speed at which the
    // star view rotates in the background
    // to simulate Earth's rotation.
    [Header("======> Lower is faster!")]
    [Space(10)]
    [Range(0, 5)]
    [SerializeField]
    private float _rotationDelay = 0;
    private bool _rotationCooldown = false;

    [Header("======> Recently selected objects")]
    [Space(10)]
    public StarController PreviousStarController;
    public StarController CurrentStarController;

    private void Update()
    {
        if (_rotationCooldown == false)
        {
            _starView.Rotate(new Vector3(0, 0, 0.2f));
            Invoke("EnableRotation", _rotationDelay);
            _rotationCooldown = true;
        }
    }

    private void EnableRotation()
    {
        _rotationCooldown = false;
    }

    public float GetRotationDelay()
    {
        return _rotationDelay;
    }
}
