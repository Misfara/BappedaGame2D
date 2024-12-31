using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class IndoorScene : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject loadingScreenTransition;
    private SpriteRenderer spriteRenderer;

     bool playerIsInvincible = false;

    [SerializeField] Player player;
    public float fadeWait = 1f;
    private bool playerInTrigger = false;
    private bool playerCanClick = false;

    [SerializeField] Transform thisIndoorRoom;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    FollowingPlayer cameraFollow;
    [SerializeField] GameObject triggerQue;
    [SerializeField] public Image button;
    [SerializeField] bool spriteFlipped = false;

    void Awake()
    {
        if (loadingScreenTransition != null)
        {
            GameObject panel = Instantiate(loadingScreenTransition, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }

   
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        cameraFollow = FindAnyObjectByType<FollowingPlayer>();
    }
    

    // This method will be called on button click
    public void OnButtonClicked()
    {
        if( playerInTrigger && playerCanClick == true){
            StartCoroutine(FadeCouroutine());
             AudioManager.Instance.musicSource.Pause();
        } 
        
    }

    // Triggers the scene load
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            playerInTrigger = true;
            playerCanClick = true;
            Color tempColor = button.color;
            tempColor.a = 1f; // Set alpha to 0
            button.color = tempColor;
           triggerQue.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            playerInTrigger = true;
            playerCanClick = true;
            Color tempColor = button.color;
            tempColor.a = 1f; // Set alpha to 0
            button.color = tempColor;
           triggerQue.SetActive(true);

        } else {
             playerInTrigger = false;
            playerCanClick = false;
            Color tempColor = button.color;
            tempColor.a = 0.3f; // Set alpha to 0
            button.color = tempColor;
            triggerQue.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            playerInTrigger = false;
            playerCanClick = false;
            Color tempColor = button.color;
            tempColor.a = 0.3f; // Set alpha to 0
            button.color = tempColor;
            triggerQue.SetActive(false);
        }
    }

     public IEnumerator FadeCouroutine()
    {
        AudioManager.Instance.PlaySFX("MoveToIndoor");
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
         
         loadingScreenTransition.SetActive(true);

        player.transform.position = thisIndoorRoom.transform.position;
        yield return new WaitForSeconds(0.1f);

     float xOffset = spriteFlipped ? -8f : 8f; // Determine offset based on sprite direction
    cameraFollow.transform.position = new Vector3(
    player.transform.position.x + xOffset, 
    player.transform.position.y + 4f, 
    -10);
        
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
    }
}
