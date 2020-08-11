using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlameParticleBehaviour : MonoBehaviour
{
    //Public Variables (Declaring the particle system) :
    public ParticleSystem flameParticleSystem;
    public List<ParticleCollisionEvent> collisionEvents;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    //OnParticleCollision:
    void OnParticleCollision( GameObject collision )
    {
        ParticlePhysicsExtensions.GetCollisionEvents(flameParticleSystem, collision, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            if (collisionEvents[i].colliderComponent.tag == "Player")
            {
                AudioManager.GetAudioManager().PlayTankHitSound();
                Healthbar.Health -= 1;
                if (Healthbar.Health <= 0)
                {
                    if (PlayerPrefs.GetFloat("Highscore") < Score.scoreValue)
                        PlayerPrefs.SetFloat("Highscore", Score.scoreValue);
                    Destroy(collisionEvents[i].colliderComponent.gameObject);
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                    PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
                    SceneManager.LoadScene("DeathScreen");
                    Healthbar.Health = 3;
                }
            }

            if (collisionEvents[i].colliderComponent.tag == "Bullet")
            {
                Destroy(collisionEvents[i].colliderComponent.gameObject);
            }
        }
    }
}

