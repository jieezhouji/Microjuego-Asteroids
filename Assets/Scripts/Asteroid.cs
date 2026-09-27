using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed = 2f;
    float maxLifeTime = 8f;
    public bool isFragment = false;
    public GameObject fragmentPrefab;

    private Vector3 moveDirection = Vector3.down;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Explode(Vector3 impactDirection)
    {
        if (!isFragment && fragmentPrefab != null)
        {
            Vector3 perpendicular = new Vector3(-impactDirection.y, impactDirection.x, 0).normalized;

            SpawnFragment(perpendicular);
            SpawnFragment(-perpendicular);
        }
        Destroy(gameObject);
    }

    private void SpawnFragment(Vector3 direction)
    {
        Vector3 spawnPos = transform.position + direction.normalized * 0.3f;
        GameObject frag = Instantiate(fragmentPrefab, spawnPos, Quaternion.identity);
        Asteroid fragScript = frag.GetComponent<Asteroid>();
        if (fragScript != null)
        {
            fragScript.isFragment = true;
            fragScript.moveDirection = direction.normalized;
        }
    }
}