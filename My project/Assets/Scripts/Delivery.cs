using UnityEngine;
using TMPro;

public class Delivery : MonoBehaviour
{
    bool hasPackage = false;
    ParticleSystem packageParticles;
    [Header("UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text packageText;
    [SerializeField] TMP_Text winText;
    [SerializeField] int score = 0;

    void Start()
    {
        packageParticles = GetComponent<ParticleSystem>();
        scoreText.gameObject.SetActive(true);
        UpdateScoreUI();
        packageText.gameObject.SetActive(true);
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
        if (score == 400)
        {
            winText.text = $"You Win!";
            winText.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(hasPackage)
        {
            packageText.text = $"Deliver this Pizza";
        }

        else
        {
            packageText.text = $"Find a Pizza Box";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package")&&!hasPackage)
        {
            hasPackage = true;
            packageParticles.Play();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Customer") && hasPackage)
        {
            Debug.Log("Delivered package!");
            hasPackage = false;
            packageParticles.Stop();
            score += 100;
            UpdateScoreUI();
            Destroy(collision.gameObject);
        }
    }
}