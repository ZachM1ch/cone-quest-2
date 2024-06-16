using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleton tactic allows for one script to be accessed by others easily

    public static GameManager GM;

    public List<GameObject> checkpoints;

    public GameObject terry;

    //public List<GameObject> spawners;
    //public List<GameObject> spawnNums;

    //public GameObject chick;

    //public GameObject counter;


    private void Awake()
    {
        GM = this;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            checkpoints.Add(g);
        }

        checkpoints.Sort((x, y) => x.GetComponent<Checkpointer>().GetCPID().CompareTo(y.GetComponent<Checkpointer>().GetCPID()));

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            terry = g;
        }

        /*

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            spawners.Add(g);
        }

        spawners.Sort((x, y) => x.name.CompareTo(y.name));

        determineSpawns();

        spawnCoop();

        counter.transform.localScale = (new Vector3(9f, 9f, 9f));
        */
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Removes a given checkpoint from the checkpoint list
    /// </summary>
    /// <param name="index"></param>
    public void RemoveCheckpointFromList(int index)
    {
        checkpoints.RemoveAt(index);
        
        if (checkpoints.Count > 0)
        {
            for (int i = 0; i < checkpoints.Count; i++)
            {
                checkpoints[i].GetComponent<Checkpointer>().SetCPID(index);
                checkpoints[i].GetComponent<Checkpointer>().ClearActive();
            }

            checkpoints[index - 1].GetComponent<Checkpointer>().MakeActive();

            terry.GetComponent<Meltometer>().SetLastCheckpoint(checkpoints[index - 1]);

        }
    }

    /*
    public void determineSpawns()
    {

        for (int index = 0; index < 9; index++)
        {
            int rand = Random.Range(0, spawners.Count);

            if (!spawnNums.Contains(spawners[rand]))
            {
                spawnNums.Add(spawners[rand]);
            }
            else
            {
                index--;
            }
        }
    }

    public void spawnChix(GameObject spooner)
    {
        GameObject indo = Instantiate(chick, spooner.transform.position, Quaternion.identity) as GameObject;
        indo.GetComponent<SphereCollider>().enabled = true;
    }

    public void spawnCoop()
    {
        foreach (GameObject g in spawnNums)
        {
            spawnChix(g);
        }
    }
    */


}
