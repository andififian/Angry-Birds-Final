using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    public SlingShooter SlingShooter;
    public TrailController trailController;
    private bool _isGameEnded = false;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Birds.Count; i++){
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }
        for(int i = 0; i < Enemies.Count; i++){
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }
        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }
    public void ChangeBird(){
        TapCollider.enabled = false;
        if (_isGameEnded){
            return;
        }
        Birds.RemoveAt(0);
        if(Birds.Count > 0){
            SlingShooter.InitiateBird(Birds[0]);
            _shotBird = Birds[0];
        }
        if (Birds.Count == 0 && !gameOver) StartCoroutine(Lose());
    }
    public void CheckGameEnd(GameObject destroyedEnemy){
        for(int i = 0; i < Enemies.Count; i++){
            if(Enemies[i].gameObject == destroyedEnemy){
                Enemies.RemoveAt(i);
                break;
            }
        }
        if(Enemies.Count == 0 && !gameOver){
           Win();
        }
    }
    public void AssignTrail(Bird bird){
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    void OnMouseUp(){
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    private IEnumerator Lose(){
        yield return new WaitForSeconds(4);
        if (!gameOver)
        {
            panel.SetActive(true);
            panel.transform.GetChild(2).gameObject.SetActive(true);
            panel.transform.GetChild(0).GetComponent<Text>().text = "YOU LOSE";
        }

    }
    private void Win(){
        gameOver = true;
        panel.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(true);
        panel.transform.GetChild(0).GetComponent<Text>().text = "YOU WIN";
    }
    public void ClickNextStage(){
        SceneManager.LoadScene(1);
    }
    public void ClickRetry(){
        SceneManager.LoadScene(0);
    }
    public void ClickExit(){
        Application.Quit();
    }


}
