using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class: RayTrace Bullet Behaviour, controls the behaviour of bullets in the game.
///Author: Lewis Nicoll
public class RayTraceBulletBehaviour : MonoBehaviour
{
    //Public Variables:
   
    public static bool canAffectTimeScale;


    //Private Variables:

    private int currentSceneIndex;

    ///Move speed variable:
    private float   moveSpeed;

        ///Variables for the bullet's path and reflections:
        private Ray bulletPath;
        private RaycastHit objectHit;
        private Vector3 reflectDirection;
        private float newRotation;

        ///Bounce variables:
        private int currentBounces;
        private int maxBounces;


    /// SlowMo Variables:
    private Ray slowMoDetectorRay;
    private RaycastHit objectHitSlowMo;

    private bool isASlowMoBullet;
    private float slowMoTime;
    private float currentSlowMoTimer;

    private Vector3 predictedHitPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        //Initialising variables:
        moveSpeed           = 5;

        bulletPath          = new Ray(transform.position, transform.forward);
        slowMoDetectorRay   = new Ray(transform.position, transform.forward);

        currentBounces      = 0;
        maxBounces          = 1;

        canAffectTimeScale = true;

        isASlowMoBullet = false;

        slowMoTime = 5f;
        currentSlowMoTimer = 0f;

        predictedHitPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        UpdateRays();
        CheckForSlowMo();
        Collisions();
        TimeSlowMo();
        Debug.DrawRay(bulletPath.origin, bulletPath.direction* moveSpeed * (Time.deltaTime + .5f));
    }

    //Bullet Movement:
    void Movement()
    {
       transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void UpdateRays()
    {
        //updating the ray:
        bulletPath.origin = transform.position;
        bulletPath.direction = transform.forward;

        slowMoDetectorRay.origin = transform.position;
        slowMoDetectorRay.direction = transform.forward;

    }

    //CheckForSlowMo fires a ray out in front of the bullet to detect an enemy tank and upon doing so attempts to set slow motion into action
    //It also sets isASlowMoBullet to true so the script knows its under the influence of slowmotion for later and sets the public static variable canAffectTimeScale
    //to false allowing no other bullets to overide the slow-mo effect.
    void CheckForSlowMo()
    {
        if (canAffectTimeScale)
        {
            if (Physics.Raycast(slowMoDetectorRay.origin, slowMoDetectorRay.direction, out objectHitSlowMo, moveSpeed))
            {

                if (objectHitSlowMo.collider.gameObject != gameObject)
                {
                    if (objectHitSlowMo.transform.tag == "Tank")
                    {
                        isASlowMoBullet = true;
                        canAffectTimeScale = false;
                        predictedHitPosition = objectHitSlowMo.transform.position;
                    }
                    else
                    {
                        isASlowMoBullet = false;
                    }
                  
                }
            }
            Debug.DrawRay(slowMoDetectorRay.origin, slowMoDetectorRay.direction * moveSpeed);
        }

        if(isASlowMoBullet)
        {
            //GameManager.GetGameManager().ActivateSlowMoWithFollowCam(transform.position);
            GameManager.GetGameManager().ActivateSlowMoWithRelativePositions(transform.position, predictedHitPosition, moveSpeed);
        }
    }

    
    //The reflections functions, dictates what the bullet should do when it's path is about to collide with another object
    //including reflecting if has enough bounces remaining:
    void Collisions()
    {
        //Reading what the ray has collided with:
        if ( Physics.Raycast(bulletPath,out objectHit, moveSpeed * Time.deltaTime + .5f))
        {
            if (objectHit.collider.gameObject != gameObject)
            {
                
                //Destroying the bullet and the object hit if it is a tank or another bullet:
                if (objectHit.transform.tag == "Bullet")
                {
                    objectHit.collider.gameObject.GetComponent <RayTraceBulletBehaviour> ().StopSlowMo();
                    Destroy(objectHit.collider.gameObject);
                    Destroy(gameObject);
                }
                else if (objectHit.transform.tag == "Tank")
                {
                    VFXManager.GetVFXManager().InstantiateBigExplosion(objectHit.transform);
                    AudioManager.GetAudioManager().PlayTankHitSound();
                    Score.scoreValue += 100;
                    Destroy(objectHit.collider.transform.parent.gameObject );
                    Destroy(gameObject);
                    GameManager.GetGameManager().KillEnemy();
                    
                }
                else if (objectHit.transform.tag == "Player")
                {
                    AudioManager.GetAudioManager().PlayTankHitSound();
                    StopSlowMo();
                    Destroy(gameObject);
                    Healthbar.Health -= 1;
                    if (Healthbar.Health <= 0)
                    {
                        if (PlayerPrefs.GetFloat("Highscore") < Score.scoreValue)
                        PlayerPrefs.SetFloat("Highscore", Score.scoreValue);
                        PlayerPrefs.GetFloat("Score");
                        Destroy(objectHit.collider.transform.parent.gameObject);
                        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
                        SceneManager.LoadScene("DeathScreen");
                        Healthbar.Health = 3;
                    }
                }
                else if (objectHit.transform.tag == "Fire")
                {
                    Destroy(gameObject);
                }

                //If the bullet hit's something else, it will reflect, assuming it has enough bounces remaining:
                else if (currentBounces < maxBounces)
                {
                    //Calculating the new reflected direction using the normal of the surface the bullet's ray hit:
                    reflectDirection = Vector3.Reflect(bulletPath.direction, objectHit.normal);
                    newRotation = 90f - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
                    transform.position = objectHit.point;
                    transform.eulerAngles = new Vector3(0, newRotation, 0);
                    currentBounces++;
                }

                //Destroying the bullet if it has no remaining bounces:
                else if (currentBounces >= maxBounces)
                {
                    Destroy(gameObject);
                }

                if (isASlowMoBullet)
                {
                    StopSlowMo();
                }
                
                
            }
        }
    }

    void TimeSlowMo()
    {
        if (isASlowMoBullet)
        {
            currentSlowMoTimer += Time.unscaledDeltaTime;

            if (currentSlowMoTimer > slowMoTime)
            {
                StopSlowMo();
            }
        }
    }

    public void StopSlowMo()
    {
        GameManager.GetGameManager().DeactivateSlowMo();
        currentSlowMoTimer = 0f;
        isASlowMoBullet = false;
    }
}
