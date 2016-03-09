using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignStage : MonoBehaviour {
    public string stageDescription;

    // List that stores all child campaign stages
    private List<StageWave> childWaves;
    private int numOfWaves;
    private int currentWaveIndex;
    private int numOfWavesCompleted;
    // Private StageWave currentWave;
    private CampaignGroup parentGroup;
    private MessageBoard messageBoard;
    // time for the description of the state to display on the message board
    public float displayTime = 1;
    private float currTime;
    private bool displayDescription = false;
    //Timer waveTimer;
    bool beginNextWave;

    void Awake()
    {
        childWaves = new List<StageWave>();
        IEnumerable waves = transform.GetComponentsInChildren<StageWave>();
        foreach (StageWave w in waves)
        {
            childWaves.Add(w);
            w.Initialize(this);
        }
        numOfWaves = childWaves.Count;
        currentWaveIndex = 0;

        if (numOfWaves <= 0)
            Debug.LogError("num of waves is empty! CampaignStage" + name);
        beginNextWave = false;
        messageBoard = MessageBoard.Instance();
        numOfWavesCompleted = 0;
    }

    void Update()
    {
        if (displayDescription)
        {
            currTime += Time.deltaTime;
            if (currTime > displayTime)
            {
                displayDescription = false;
                messageBoard.pressedBack();
            }
        }
    }

    /// <summary>
    /// Initialize this CampaignStage
    /// </summary>
    /// <param name="parent"></param>
    public void Initialize(CampaignGroup parent)
    {
        parentGroup = parent;
    }
    
    /// <summary>
    ///  Called by the parent CampaignGroup to Start the Stage
    /// </summary>
    public void BeginCurrStage()
    {
        if (stageDescription != "")
        {
            Debug.Log("displaying stage description: " + stageDescription);
            messageBoard.setTitle(gameObject.name);
            messageBoard.setBody(stageDescription);
            messageBoard.activateBoard(true);
            displayDescription = true;
            currTime = 0;
        }
        if (!parentGroup)
            Debug.LogError("No Parent Group!");
        currentWaveIndex = 0;
        numOfWavesCompleted = 0;
        StartCoroutine(childWaves[currentWaveIndex].BeginCurrWave());
        //childWaves[currentWaveIndex].BeginCurrWave();
        Debug.Log("BEGINNING STAGE " + name);
    }
    public void LoadNextWave(StageWave completedStage)
    {
        beginNextWave = true;
        currentWaveIndex++;
        if (currentWaveIndex >= numOfWaves)
            return;
        StartCoroutine(childWaves[currentWaveIndex].BeginCurrWave());
    }

    public void NotifyWaveCompleted(StageWave completedStage)
    {
        numOfWavesCompleted++;
        Debug.Log("PLAYER BEAT WAVE: " + completedStage.name + " numOfWavesCompleted: " + numOfWavesCompleted + "/" + numOfWaves);
        if (numOfWavesCompleted >= numOfWaves)
        {
            parentGroup.NotifyStageCompleted(this);
            Debug.Log("PLAYER BEAT STAGE " + name);
            return;
        }
    }
    /*
    /// <summary>
    ///  Called by the parent CampaignGroup to Start the Stage
    /// </summary>
    public void NotifyWaveCompleted(StageWave completedStage)
    {
        beginNextWave = true;
        numOfWavesCompleted++;
        currentWaveIndex++;

        Debug.Log("PLAYER BEAT WAVE: " + completedStage.name + " numOfWavesCompleted: " + numOfWavesCompleted + "/" + numOfWaves);
        if (numOfWavesCompleted >= numOfWaves)
        {
            parentGroup.NotifyStageCompleted(this);
            Debug.Log("PLAYER BEAT STAGE " + name);
            return;
        }
        //childWaves[currentWaveIndex].BeginCurrWave();
        StartCoroutine(childWaves[currentWaveIndex].BeginCurrWave());
    }
     * */
}
