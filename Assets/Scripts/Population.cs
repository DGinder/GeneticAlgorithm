using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {
    public Dot prefab;
    public GameObject goal;
    public int GenerationCount = 1;
    Dot[] dots;
    float fitnessSum = 0;
    bool inWork = false;
    int dotname = 0;

    // Use this for initialization
    void Start() {
        dots = new Dot[100];
        Debug.Log("setting up population");
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i] = Instantiate(prefab);
            dots[i].SetupDot();
            dots[i].name = "Dot" + dotname;
            dots[i].setupBrainRandom();
            dotname++;
            dots[i].transform.parent = gameObject.transform;
            dots[i].goal = goal;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (inWork == false) {
            if (AllDotsDead() == true)
            {
                Debug.Log("Generation Dead Creating Next Generation");
                NextGeneration();
            }
        }
    }

    bool AllDotsDead()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (dots[i].dead == false)
            {
                return false;
            }

        }
        return true;
    }


    void NextGeneration()
    {
        inWork = true;
        Dot[] newDots = new Dot[dots.Length];
        CalculateChildFitness();
        Debug.Log("Fitness of Generation Calculated");
        CalculateFitnessSum();
        Debug.Log("Total Fitness of Generation Calculated");
        for (int i = 0; i < newDots.Length; i++)
        {
            //Get Parents
            Dot parent1 = SelectParent();
            Debug.Log(parent1.name);
            Dot parent2 = SelectParent();
            Debug.Log(parent1.name);

            //create Child
            newDots[i] = CreateChild(parent1, parent2);
        }

        for (int i = 0; i < dots.Length; i++)
        {
            Debug.Log("trying to destroy");
            Destroy(dots[i].gameObject);
        }

        dots = newDots;
        GenerationCount++;

        inWork = false;
    }

    void CalculateChildFitness()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].CalculateFitness();

        }
    }

    void CalculateFitnessSum()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            fitnessSum += dots[i].fitness;

        }
    }

    Dot SelectParent()
    {
        float rand = Random.Range(0, fitnessSum);

        Debug.Log(rand);

        float runFitnessSum = 0;
        for (int i = 0; i < dots.Length; i++)
        {
            runFitnessSum += dots[i].fitness;
            if (runFitnessSum >= rand)
            {
                return dots[i];
            }
        }
        return null;
    }

    Dot CreateChild(Dot parent1, Dot parent2)
    {
        Dot Child = Instantiate(prefab);
        Child.name = "Dot" + dotname;
        Child.SetupDot();
        Child.setupBrainZero();
        dotname++;
        Child.transform.parent = gameObject.transform;
        Child.goal = goal;
        for (int i = 0; i < parent1.brain.directions.Length; i++)
        {
            Child.Inherit(parent1, parent2);
        }
        return Child;
    }
}
