using System.Collections.Generic;
using UnityEngine;

public class RobotGenerator : MonoBehaviour
{
    private float timeBetweenGeneration = 5f;
    private float currentTime = 0f;
    [SerializeField] private GameObject robot;

    private List<float> xCoordinates;
    private float yCoordinate = -2f;
    private List<float> zCoordinates;

    private float xBorder = 16f;
    private float zBorder = 8f;

    private void Awake()
    {
        xCoordinates = new List<float>();
        for(int i = 0; i < 5; i++)
        {
            xCoordinates.Add(-20f + i * 10f);
        }

        zCoordinates = new List<float>();
        for(int i = 0; i < 2; i++)
        {
            zCoordinates.Add(-5f + i * 10f);
        }
    }

    private void FixedUpdate()
    {
        if(currentTime >= timeBetweenGeneration)
        {
            GameObject newRobot = Instantiate(robot);
            GeneratePosition(newRobot);

            currentTime = 0f;
            if (timeBetweenGeneration > 1f)
            {
                //timeBetweenGeneration--;
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void GeneratePosition(GameObject robot)
    {
        int side = Random.Range(0, 2);

        float xCoord;
        float zCoord;
        float rotation = 0f;

        if(side == 0) //upper or lower side
        {
            xCoord = xCoordinates[Random.Range(0, xCoordinates.Count)];
            side = Random.Range(0, 2);
            if(side == 0) //upper side
            {
                zCoord = zBorder;
            }
            else //lower side
            {
                zCoord = -zBorder;
                rotation = 180f;
            }
        }
        else //left or right side
        {
            zCoord = zCoordinates[Random.Range(0, zCoordinates.Count)];
            side = Random.Range(0, 2);
            if (side == 0) //right side
            {
                xCoord = xBorder;
                rotation = 90f;
            }
            else //left side
            {
                xCoord = -xBorder;
                rotation = -90f;
            }
        }

        robot.transform.position = new Vector3(xCoord, yCoordinate, zCoord);
        robot.transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
