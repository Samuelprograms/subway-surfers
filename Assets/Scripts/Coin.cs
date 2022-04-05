using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
        {
            return;
        }
        GameManager.instance.IncrementScore();
        Destroy(gameObject);
    }

}
