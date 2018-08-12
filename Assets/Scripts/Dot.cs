using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {
    public GameObject goal;
    private Vector2 acc = new Vector2(0f, 0f);
    private Vector2 vel = new Vector2(0f, 0f);
    private int brainsize = 400;
    public float fitness;
    public Brain brain;
    public bool dead = false;



    // Use this for initialization
    void Start() {
    }

    public void SetupDot()
    {

        Debug.Log("creating Brain");
        brain = new Brain(brainsize);
        Debug.Log("created Brain");

        Debug.Log("Setting Location");
        SpriteRenderer m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.black;
        Vector3 cPos = new Vector3(.5f, .8f, 10f);
        transform.position = Camera.main.ViewportToWorldPoint(cPos);
    }

    // Update is called once per frame
    void Update() {
        if (!dead)
        {
            Move();

            Vector3 cPos = Camera.main.WorldToViewportPoint(transform.position);

            if (cPos.x < 0.01
                || cPos.x > .99
                || cPos.y < 0.01
                || cPos.y > .99)
            {
                dead = true;
            }

            if (Vector2.Distance(transform.position, goal.transform.position) < .1)
            {
                dead = true;
            }

            if (brain.step == brainsize)
            {
                dead = true;
            }
        }
    }

    public void setupBrainRandom()
    {
        Debug.Log("Setting Brain Positions");
        brain.initializeRandom();
        Debug.Log("Brain of " + gameObject.name + " created randomly");
    }

    public void setupBrainZero()
    {
        Debug.Log("Setting Brain Positions");
        brain.initializeZero();
        Debug.Log("Brain of " + gameObject.name + " set to zero");
    }

    public void Inherit(Dot parent1, Dot parent2){
        Debug.Log("Parent1: " + parent1.name + " & Parent2: " + parent2.name);
        brain.inheritBrain(parent1.brain.getDirections(), parent2.brain.getDirections());
    }

    void Move()
    {
        if (brain.step < brainsize) {
            acc = brain.directions[brain.step];
            brain.step++;
        }
        vel += acc;
        if (vel.magnitude > .3)
        {
            vel = vel.normalized;
            vel.x = vel.x * .3f;
            vel.y = vel.y * .3f;
        }

        transform.position = new Vector2(transform.position.x + vel.x, transform.position.y + vel.y);
    }

    public void CalculateFitness()
    {
        float temp = Vector2.Distance(transform.position, goal.transform.position);
        float tempF = 1 / (temp * temp);
        if(tempF >= 999)
        {
            tempF = 999f;
        }
        if(temp == 0)
        {
            fitness = 1000;
        }
        else
        {
            fitness = tempF;
        }
    }
}
