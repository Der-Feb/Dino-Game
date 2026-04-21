using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle_Limiter"))
        {
            Destroy(gameObject);
        }
    }
}
