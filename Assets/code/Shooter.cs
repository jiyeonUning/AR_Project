using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject ballLeftPrefab;
    [SerializeField] GameObject ballRightPrefab;
    [SerializeField] float shootSpeed;

    public void ShootLeft()
    {
        GameObject ball = Instantiate(ballLeftPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        rigidbody.velocity = shootSpeed * Camera.main.transform.forward;
    }

    public void ShootRight()
    {
        GameObject ball = Instantiate(ballRightPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
        Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
        rigidbody.velocity = shootSpeed * Camera.main.transform.forward;
    }
}
