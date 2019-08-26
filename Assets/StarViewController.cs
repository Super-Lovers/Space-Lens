using UnityEngine;

public class StarViewController : MonoBehaviour
{
    [SerializeField]
    private Transform _starView;

    // Parameters of the speed at which the
    // star view rotates in the background
    // to simulate Earth's rotation.
    [Header("======> Lower is faster!")]
    [Space(10)]
    [Range(0, 5)]
    [SerializeField]
    private float _rotationDelay;
    private bool _rotationCooldown = false;

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
}
