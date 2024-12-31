using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] FollowingPlayer cameraFollow;
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    [SerializeField] Transform thisRoom;
    [SerializeField] GameObject player;

    [SerializeField] Animator animator;
    
   
     
    public GameObject loadingPanel;
    public GameObject loadingScreenTransition;
    private SpriteRenderer spriteRenderer;
   
     bool playerIsInvincible = false;
     
     Player playerScript;

         public UnityEvent OnFadeStart;
    public UnityEvent OnFadeComplete;

    public void Start()
    {
   
        playerScript = GameObject.FindObjectOfType<Player>();
     }
    


    

    void Awake()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        if(loadingPanel != null)
        {
            GameObject panel = Instantiate(loadingPanel,Vector3.zero, 
            Quaternion.identity ) as GameObject;
            Destroy(panel,1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AudioManager.Instance.musicSource.Pause();
            StartCoroutine(FadeCouroutine());
           
        }

    }

    public IEnumerator FadeCouroutine()
    {
         OnFadeStart?.Invoke();

       animator.enabled = false;
        player.GetComponent<Player>().canMove = false;
       

        loadingPanel.SetActive(true);

         

        if (spriteRenderer != null)
        {
            Color tempColor = spriteRenderer.color;
            tempColor.a = 0; // Set alpha to 0
            spriteRenderer.color = tempColor;
           playerIsInvincible = true ;
         }
            
         yield return new WaitForSeconds(1f);
         loadingPanel.SetActive(false);
         OnFadeComplete?.Invoke();
         loadingScreenTransition.SetActive(true);

        player.transform.position = thisRoom.transform.position;
        yield return new WaitForSeconds(0.1f);

        if(playerSpriteRenderer.flipX == false)
        {
            if(cameraFollow.transform.position.x < 0) {
            cameraFollow.transform.position = new Vector3 (player.transform.position.x +8f, 
            player.transform.position.y + 4f, -10);
            } else {
                cameraFollow.transform.position = new Vector3 (player.transform.position.x +8f, 
            player.transform.position.y + 4f, -10);
            }
        } 

        if(playerSpriteRenderer.flipX == true)
        {
            if(cameraFollow.transform.position.x  < 0)
            {
                cameraFollow.transform.position = new Vector3 (player.transform.position.x -8f, 
            player.transform.position.y + 4f, -10);
            } else {
                cameraFollow.transform.position = new Vector3 (player.transform.position.x -8f, 
            player.transform.position.y + 4f, -10);
            }
        }

        yield return new WaitForSeconds(1f);
        loadingScreenTransition.SetActive(false);
        player.GetComponent<Player>().canMove = true;
        animator.enabled = true;
        
                 if (playerIsInvincible)
        {
            // Gradually make the player visible
            yield return StartCoroutine(FadeInPlayer(0.4f)); // Call the fade in coroutine
            playerIsInvincible = false;
        }

        AudioManager.Instance.musicSource.UnPause();
        yield return new WaitForSeconds(1f); 

        
        
     

    }

     private IEnumerator FadeInPlayer(float duration)
    {
        animator.enabled = false;
        playerScript.canMove = false;
        float elapsedTime = 0f;
        Color tempColor = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Clamp01(elapsedTime / duration); // Calculate new alpha
            spriteRenderer.color = tempColor;
            yield return null; // Wait for the next frame
        }

        // Ensure alpha is set to 1 at the end
        tempColor.a = 1;
        spriteRenderer.color = tempColor;
        animator.enabled = true;
         playerScript.canMove = true;
         
    }
}

