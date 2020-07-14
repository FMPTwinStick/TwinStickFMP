using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomingMissilieBehaviour : MonoBehaviour
{
    //Public Variables:
    public Transform player;

    //Private Variables:
    private float       m_speed;
    private float       m_rotationSpeed;

    private Ray         m_bulletPath;
    private RaycastHit  m_objectHit;

    // Start is called before the first frame update
    void Start()
    {
        m_speed = 5f;
        m_rotationSpeed = 200f;

        player = GameObject.FindWithTag("Player").transform;

        m_bulletPath = new Ray(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Collisions();
        Debug.DrawRay(m_bulletPath.origin, m_bulletPath.direction * m_speed * (Time.deltaTime + .5f));
    }

    void Movement()
    {
        //Moving forward:
            transform.Translate(Vector3.forward * m_speed * Time.deltaTime);

        //Rotating to face the player:

        //Getting the normalized direction towards the target:
        Vector3 targetDirection = (player.position - transform.position);
        targetDirection.Normalize();
        //Calculating the ratio of rotation required using the cross product:
        float rotationRequired = Vector3.Cross(targetDirection, transform.forward).y;
        //Rotating the missile accordingly:
        transform.Rotate(0f, -rotationRequired, 0f);

    }

    void Collisions()
    {
        //updating the ray:
        m_bulletPath.origin = transform.position;
        m_bulletPath.direction = transform.forward;

        //Reading what the ray has collided with:
        if (Physics.Raycast(m_bulletPath, out m_objectHit, m_speed * Time.deltaTime + .5f))
        {
            if (m_objectHit.collider.gameObject != gameObject)
            {

                //Destroying the bullet and the object hit if it is a tank or another bullet:
                if (m_objectHit.transform.tag == "Bullet")
                {
                    m_objectHit.collider.gameObject.GetComponent<RayTraceBulletBehaviour>().StopSlowMo();
                    Destroy(m_objectHit.collider.gameObject);
                    Destroy(gameObject);
                }
                else if (m_objectHit.transform.tag == "Tank")
                {
                    VFXManager.GetVFXManager().InstantiateBigExplosion(m_objectHit.transform);
                    AudioManager.GetAudioManager().PlayTankHitSound();
                    Score.scoreValue += 100;
                    Destroy(m_objectHit.collider.transform.parent.gameObject);
                    Destroy(gameObject);
                    GameManager.GetGameManager().KillEnemy();

                }
                else if (m_objectHit.transform.tag == "Player")
                {
                    AudioManager.GetAudioManager().PlayTankHitSound();
                    GameManager.GetGameManager().DeactivateSlowMo();
                    Destroy(gameObject);
                    Healthbar.Health -= 1;
                    if (Healthbar.Health <= 0)
                    {
                        if (PlayerPrefs.GetFloat("Highscore") < Score.scoreValue)
                            PlayerPrefs.SetFloat("Highscore", Score.scoreValue);
                        Destroy(m_objectHit.collider.transform.parent.gameObject);
                        SceneManager.LoadScene("DeathScreen");
                        Healthbar.Health = 3;
                    }
                }
                else
                {
                    Destroy(gameObject);
                    VFXManager.GetVFXManager().InstantiateBigExplosion(m_objectHit.transform);
                    AudioManager.GetAudioManager().PlayTankHitSound();
                }
            }
        }
    }
}
