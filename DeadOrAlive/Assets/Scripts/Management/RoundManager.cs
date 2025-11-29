using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using kNN.CosineSimilarity;

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
    public TextMeshProUGUI updatedTimeText;
    public Collider2D spawnAreaCollider;
    public UserTypingInput userTypingInput;
    [SerializeField] private Bounds spawnArea;
    [SerializeField] private Vector2 spawnAreaCenter;

    [Header("Wanted Poster")]
    public Transform wantedPersonPoster;
    public GameObject attribute_UI;
    // public Transform clothesParentUI;
    // public Transform hairParentUI;
    // public Transform accessoriesParentUI;
    // public Transform facialHairParentUI;

    [Header("Round Info")]
    public float roundOneMaxTime;
    public float maxTimeToStart;
    [SerializeField] private int currentRoundNum;
    [SerializeField] private float roundTimeLeft;

    [Header("Conditions")]
    [SerializeField] private bool readyToStartRound;
    [SerializeField] private bool timerRunning;

    // Start is called before the first frame update
    void Start()
    {
        SetSpawnArea(spawnAreaCollider);
        spawnAreaCenter = spawnArea.center;

        // SpawnPersonInRandomPosition(spawnArea, person);
        // SpawnPeople(4, 8);

        // roundNumberText.text = currentRoundNum.ToString();
        // timeLeftText.text = roundTimeLeft.ToString();

        // AssignPersonWanted();
        StartNewRound(4, 8);

        AddWantedPersonToPoster();
        
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
    }

    public void SetSpawnArea(Collider2D collider2D)
    {
        spawnArea = collider2D.bounds;
    }

    // kNN Search Methods
    /////////////////////

    public void DisplayMostSimilarScores(List<int> vectorizedQuery)
    {
        CalculateSimilarities(vectorizedQuery);

        double highestScore = 0;
        GameObject personWithHighestScore = null;

        foreach (GameObject person in peopleList)
        {
            if (person != null)
            {
                PersonDocument personInstance = person.GetComponent<PersonDocument>();
                double personSimilarityScore = personInstance.cosineSimilarityScore;

                if (highestScore < personSimilarityScore)
                {
                    highestScore = personSimilarityScore;
                    personWithHighestScore = person;
                }
            }
        }

        if (personWithHighestScore != null)
        {
            PersonDocument personWithHighestScoreInstance = personWithHighestScore.GetComponent<PersonDocument>();
            personWithHighestScoreInstance.EnableTargetCircle();
        }
    }

    public void CalculateSimilarities(List<int> vectorizedQuery)
    {
        double personMagnitude;
        double queryMagnitude = CosineSimilarity.CalculateMagnitude(vectorizedQuery);
        int dotProduct;
        double cosineSimilarity;

        foreach (GameObject person in peopleList)
        {
            if (person != null)
            {
                PersonDocument personInstance = person.GetComponent<PersonDocument>();
                personMagnitude = CosineSimilarity.CalculateMagnitude(personInstance.vectorizedTokens);
                dotProduct = CosineSimilarity.CalculateDotProduct(personInstance.vectorizedTokens, vectorizedQuery);
                cosineSimilarity = CosineSimilarity.CalculateCosineSimilarity(dotProduct, queryMagnitude, personMagnitude);

                personInstance.cosineSimilarityScore = cosineSimilarity;
            }
        }
    }

    // Round Methods
    ////////////////
    public void StartNewRound(int minPeopleToGenerate, int maxPeopleToGenerate)
    {
        currentRoundNum++;
        SpawnPeople(minPeopleToGenerate, maxPeopleToGenerate);
        AssignPersonWanted();
        AddWantedPersonToPoster();
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
        int numPeopleToSpawn = Random.Range(minPeopleToGenerate, maxPeopleToGenerate + 1);
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

    public void DisableAllPeopleCircles()
    {
        foreach (GameObject person in peopleList)
        {
            PersonDocument personInstance = person.GetComponent<PersonDocument>();
            personInstance.DisableTargetCircle();
        }
    }

    public void PauseRound()
    {
        PauseTimer();
    }

    public void ResetRounds()
    {
        currentRoundNum = 1;
        UpdateRound(currentRoundNum);
    }

    // Wanted Poster Methods
    ///////////////////////
    public void AddWantedPersonToPoster()
    {
        PersonDocument wantedPerson = GetWantedPerson();
        List<PersonSprite> wantedPersonSprites = wantedPerson.GetPersonSprites();

        GameObject spriteLayer = attribute_UI;

        foreach (PersonSprite ps in wantedPersonSprites)
        {
            GameObject spriteLayerInstance = Instantiate(spriteLayer, wantedPersonPoster);
            Image image = spriteLayerInstance.GetComponent<Image>();

            image.sprite = ps.sprite;
            image.color = ps.color;
        }
    }

    public PersonDocument GetWantedPerson()
    {
        foreach (GameObject person in peopleList)
        {
            PersonDocument personInstance = person.GetComponent<PersonDocument>();
            if (personInstance.GetWantedStatus() == true)
            {
                return personInstance;
            }
        }

        Debug.Log("Wanted person not found");
        return null;
    }

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

    public void MakePeopleUnclickable()
    {
        foreach (GameObject person in peopleList)
        {
            PersonDocument personInstance = person.GetComponent<PersonDocument>();
            personInstance.canBeClicked = false;
        }
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

    public void ResetTimerToRoundOne()
    {
        roundTimeLeft = roundOneMaxTime;
    }
    public void RunTimer()
    {
        roundTimeLeft -= Time.deltaTime;
        int roundedTime = Mathf.FloorToInt(roundTimeLeft);

        if (roundedTime < 0)
        {
            roundedTime = 0;
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
        DisplayUpdatedTime("-" + time.ToString(), Color.red);
        Invoke(nameof(HideUpdatedTime), 0.75f);
    }

    public void AddToTimer(int time)
    {
        roundTimeLeft += time;
        DisplayUpdatedTime("+" + time.ToString(), Color.green);
        Invoke(nameof(HideUpdatedTime), 0.75f);
    }

    public void DisplayUpdatedTime(string time, Color color)
    {
        updatedTimeText.text = time;
        updatedTimeText.color = color;
    }

    public void HideUpdatedTime()
    {
        updatedTimeText.text = "";
    }

    public float GetRoundTimeLeft()
    {
        return roundTimeLeft;
    }
}
