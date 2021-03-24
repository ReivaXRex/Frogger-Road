using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private int score;
    private bool onLog;

    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] GameObject model;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isHopping = false;
    [SerializeField] private TerrainGenerator terrainGenerator;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isHopping)
        {
            // Lazy initialization 
            float zDifference = 0;

            /// Readjust player to be back on the grid for movement.
            if (transform.position.z % 1 != 0)
            {
                zDifference = Mathf.Round(transform.position.z) - transform.position.z;
            }

            // Move & Rotate the player forward and update the player's score.
            MoveCharacter(new Vector3(2, 0, zDifference));
            transform.rotation = Quaternion.Euler(0, 0, 0);
            UpdateScore();
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
        {
            // Move & Rotate the player left & trigger movement animation.
            MoveCharacter(new Vector3(0, 0, 1));
            if (!onLog)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
          
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
        {
            // Move & Rotate the player right & trigger movement animation.
            MoveCharacter(new Vector3(0, 0, -1));
            if (!onLog)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            
        }
    }

    /// <summary>
    /// Trigger Movement animation and move the player.
    /// </summary>
    /// <param name="difference">
    /// Axis to move along.
    /// </param>
    private void MoveCharacter(Vector3 difference)
    {
        anim.SetTrigger("Hop");
        AudioManager.Instance.PlaySFX("Hop");
        isHopping = true;

        transform.position +=  difference;

        terrainGenerator.SpawnTerrain(false, transform.position);
    }

    public void FinishHop()
    {
        isHopping = false;
    }

    private void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;

    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.collider.GetComponent<MovingObject>();

        if (obj != null)
        {
            if (obj.isLog)
            {
                transform.parent = collision.collider.transform;
                onLog = true;
            }
        }
        else
        {
            transform.parent = null;
            onLog = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.SetDeathBool();
            Death();
            Destroy(gameObject, 1);
        }
    }

    public void Death()
    {
        model.SetActive(false);
        deathVFX.Play();
        AudioManager.Instance.PlaySFX("Death");
    }


}
