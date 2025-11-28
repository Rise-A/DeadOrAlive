using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    [Header("People")]
    public int minPeopleToGenerate;
    public int maxPeopleToGenerate;
    public GameObject person;
    [SerializeField] private List<GameObject> peopleList;

    [Header("References")]
    public TextMeshProUGUI roundNumberText;
    public TextMeshProUGUI timeLeftText;
    public Collider2D spawnAreaCollider;
    [SerializeField] private Bounds spawnArea;
    [SerializeField] private Vector2 spawnAreaCenter;
    public Transform wantedPoster;

    [Header("Round Info")]
    public float maxTimeToStart;
    [SerializeField] private int currentRoundNum;
    [SerializeField] private float roundTimeLeft;

    [Header("Conditions")]
    [SerializeField] private bool readyToStartRound;
    [SerializeField] private bool timerRunning;
    [SerializeField] private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        SetSpawnArea(spawnAreaCollider);
        spawnAreaCenter = spawnArea.center;

        // SpawnPersonInRandomPosition(spawnArea, person);
        // SpawnPeople(4, 8);

        // roundNumberText.text = currentRoundNum.ToString();
        // timeLeftText.text = roundTimeLeft.ToString();

        // AssignPersonWanted();
        StartNewRound(4, 8);
        
        currentRoundNum = 1;
        UpdateRound(currentRoundNum);

        timerRunning = true;
        roundTimeLeft = maxTimeToStart;
    }

    void Update()
    {
        if (timerRunning)
        {
            RunTimer();
        }

        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     SubtractFromTimer(2);
        // }
    }

    public void SetSpawnArea(Collider2D collider2D)
    {
        spawnArea = collider2D.bounds;
    }

    // Round Methods
    ////////////////
    public void StartNewRound(int minPeopleToGenerate, int maxPeopleToGenerate)
    {
        currentRoundNum++;
        SpawnPeople(minPeopleToGenerate, maxPeopleToGenerate);
        AssignPersonWanted();
    }

    public void SpawnPersonInRandomPosition(Bounds bounds, GameObject person)
    {
        float spawnXPos = Random.Range(bounds.min.x, bounds.max.x);
        float spawnYPos = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 randomSpawnPosition = new Vector2(spawnXPos, spawnYPos);

        GameObject personInstance = Instantiate(person, randomSpawnPosition, quaternion.identity);
        peopleList.Add(personInstance);
    }

    public void SpawnPeople(int minPeopleToGenerate, int maxPeopleToGenerate)
    {
        int numPeopleToSpawn = Random.Range(minPeopleToGenerate, maxPeopleToGenerate);
        int peopleSpawned = 0;

        while (peopleSpawned < numPeopleToSpawn)
        {
            SpawnPersonInRandomPosition(spawnArea, person);
            peopleSpawned++;
        }
    }

    public void AssignPersonWanted()
    {
        int wantedIndex = Random.Range(0, peopleList.Count);
        PersonDocument personInstance = peopleList[wantedIndex].GetComponent<PersonDocument>();
        personInstance.SetWantedStatus(true);
    }

    // Wanted Poster Methods
    ///////////////////////

    public void UpdateRound(int round)
    {
        roundNumberText.text = round.ToString();
    }

    /// <summary>
    /// Clears the list of people, and deletes all of them.
    /// </summary>
    public void ClearPeople()
    {
        foreach (GameObject person in peopleList)
        {
            Destroy(person);
        }
        peopleList.Clear();
    }

    // Timer Methods
    ////////////////
    public void StartTimer()
    {
        timerRunning = true;
    }

    public void PauseTimer()
    {
        timerRunning = false;
    }
    public void RunTimer()
    {
        roundTimeLeft -= Time.deltaTime;
        int roundedTime = Mathf.FloorToInt(roundTimeLeft);

        if (roundedTime < 0)
        {
            roundedTime = 0;
            isGameOver = true;
        }
        
        UpdateTime(roundedTime);
    }
    public void UpdateTime(int time)
    {
        timeLeftText.text = time.ToString();
    }

    public void SubtractFromTimer(int time)
    {
        roundTimeLeft -= time;
    }
}
