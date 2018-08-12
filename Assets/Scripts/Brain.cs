using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {
    public Vector2[] directions;
    public int step = 0;
    public Brain(int size)
    {
        directions = new Vector2[size];
    }

    private void randomize()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            float randomAngle = Random.Range(0, 2 * Mathf.PI);
            directions[i] = new Vector2((float)Mathf.Cos(randomAngle),-(float)Mathf.Sin(randomAngle));
        }
    }

    private void zero()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = new Vector2(0, 0);
        }
    }

    public void initializeRandom()
    {
        randomize();
    }

    public void initializeZero()
    {
        zero();
    }

    public void inheritBrain(Vector2[] directions1, Vector2[] directions2)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            int parentgene = Random.Range(0, 101);
            if (parentgene > 50)
            {
                Debug.Log(directions[i].x + " " + directions[i].y);
                directions[i] = directions[i];
            }
            else
            {
                Debug.Log(directions[i].x + " " + directions[i].y);
                directions[i] = directions[i];
            }
        }
    }

    public Vector2[] getDirections()
    {
        return directions;
    }
}
