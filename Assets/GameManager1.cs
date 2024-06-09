using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    [SerializeField] int enemiesAmount;
    [SerializeField] List<GameObject> enemiesObjects;
    [SerializeField] GameObject enemyObject;
    [SerializeField] Transform enemyPosAppear;
    [SerializeField] float timeToAppearAgain;
    [SerializeField] int limitEnemiesDead;
    [SerializeField] int addLimitenemiesDead;
    [SerializeField] public int enemiesKilled;
    [SerializeField] int killBoss;
    public int enemynum;
    float rot;
    bool ableSpawnEnemies;
    [SerializeField] GameObject boss;
    [SerializeField] Text textEnemiesKilled;
    // Start is called before the first frame update
    void Start()
    {

        ableSpawnEnemies = true;
        for (int i = 0; i < enemiesAmount; i++)
        {
            GameObject enemy = Instantiate(enemyObject, Vector3.zero, Quaternion.identity);
            enemy.SetActive(false);
            enemiesObjects.Add(enemy);
            Enemy enemyS = enemy.GetComponent<Enemy>();
            enemyS.numEnemy = i;
        }
        Invoke("EnemiesAppear", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        textEnemiesKilled.text = "Enemies killed: " + enemiesKilled;
        //if (enemiesKilled > limitEnemiesDead)
        //{
        //    ChangeRotGame();

        //}
        if (enemiesKilled > killBoss)
        {
            ableSpawnEnemies = false;
            for (int i = 0; i < enemiesAmount; i++)
            {
                enemiesObjects[i].SetActive(false);
            }
                boss.SetActive(true);
        }
    }

    //void ChangeRotGame()
    //{
    //    rot += 90;
    //    limitEnemiesDead += addLimitenemiesDead;

    //    MovimientoCamara movimientoCamara = GameObject.Find("Camera").GetComponent<MovimientoCamara>();
    //    movimientoCamara.ChangeRot(rot);
    //}

    void EnemiesAppear()
    {
        if(ableSpawnEnemies)
        {
         
            for (int i = 0; i < enemiesObjects.Count; i++)
        {
                enemyPosAppear.transform.position = new Vector3(Random.Range(8, -8), 0, -9.19f);
                enemiesObjects[i].transform.position = enemyPosAppear.position;
                enemiesObjects[i].SetActive(true);
                StartCoroutine(randomTimeSpawn());
                
        }
        }
        
    }

    public void SetAbleEnemy(int enemyToActivate)
    {
        StartCoroutine(AppearAgain(enemyToActivate));
    }

    IEnumerator AppearAgain(int enemyToActivate)
    {
        if (ableSpawnEnemies)
        {
            yield return new WaitForSeconds(Random.Range(0, timeToAppearAgain));
            enemyPosAppear.transform.position = new Vector3(Random.Range(8, -8), 0, -9.19f);
            enemiesObjects[enemyToActivate].transform.position = enemyPosAppear.position;
            enemiesObjects[enemyToActivate].SetActive(true);
        }
           
    }
    IEnumerator randomTimeSpawn()
    {
        yield return new WaitForSeconds(Random.Range(0, timeToAppearAgain));
        
    }
}
