using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    int speed = 10;
    float maxLifeTime = 3;
    public Vector3 targetVector = Vector3.right;
    private float timer;

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(targetVector * speed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= maxLifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                Vector3 realDirection = transform.TransformDirection(targetVector).normalized;
                asteroid.Explode(realDirection);
            }
            else
            {
                Destroy(other.gameObject);
            }

            gameObject.SetActive(false);
            IncreaseScore();
        }
    }

    public void IncreaseScore()
    {
        Player.SCORE++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<TMP_Text>().text = "Puntos: " + Player.SCORE;
    }
}