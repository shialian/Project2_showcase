using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task[] tasks;
    
    public GameObject buttonToEnd;

    private bool taskFinish;

    private void Awake()
    {
        taskFinish = true;
        buttonToEnd.SetActive(false);
    }

    private void Update()
    {
        taskFinish = true;
        foreach(Task task in tasks)
        {
            if(task.taskComplete == false)
            {
                taskFinish = false;
            }
        }
        if (taskFinish)
        {
            buttonToEnd.SetActive(true);
        }
    }
}
