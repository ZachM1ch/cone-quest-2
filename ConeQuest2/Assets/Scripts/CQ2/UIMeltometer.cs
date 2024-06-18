using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMeltometer : MonoBehaviour
{
    public List<RawImage> tickMarks;

    public RawImage currentTick;
    public int currentTickIndex;

    RawImage meterBar;

    float meterBarRightSideOffset;

    bool isTakingGradualDamage = false;

    float initX;

    private void Awake()
    {

        foreach (RawImage g in RawImage.FindObjectsOfType(typeof(RawImage)))
        {
            if (g.tag == "MeterBar")
            {
                meterBar = g;
            }
            if (g.tag == "MeterTick")
            {
                tickMarks.Add(g);
            }
        }

        tickMarks.Sort((x, y) => x.name.CompareTo(y.name));

        currentTick = tickMarks[0];
        currentTickIndex = 0;

    }

    // Start is called before the first frame update
    void Start()
    {
        meterBarRightSideOffset = meterBar.rectTransform.sizeDelta.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Screw it I'm hardcoding it
        //if (isTakingGradualDamage)
        //{
            //GradualMoveMeter(0.05f);
        //}
    }

    public void MoveMeltBar(float inc)
    {
        float newX;

        print("enter");
        print(inc);

        if (inc > -1 && inc < 0)
        {
            print(currentTick);
            print(tickMarks[tickMarks.Count - 1]);
            if (currentTick != tickMarks[tickMarks.Count - 1])
            {
                print("pass1");
                if (true)
                {
                    print("Player gradual");

                    GradualMoveMeter(inc);
                }
            }
        }
        else if (inc < 0)
        {
            if (currentTick != tickMarks[tickMarks.Count - 1])
            {
                if ((tickMarks.Count - 1 - currentTickIndex) - Math.Abs(inc) < 0)
                {
                    // kill player
                    currentTickIndex = tickMarks.Count - 1;
                }
                else
                {
                    currentTickIndex = currentTickIndex - (int)inc;
                    
                }
                currentTick = tickMarks[currentTickIndex];
                newX = currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset;
                meterBar.rectTransform.anchoredPosition = new Vector3(newX, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
            }
        }
        else if (inc > 0)
        {
            if (currentTick != tickMarks[0])
            {
                if (currentTickIndex - Math.Abs(inc) < 0)
                {
                    // full health player
                    currentTickIndex = 0;
                }
                else
                {
                    currentTickIndex = currentTickIndex - (int)inc;
                }
                currentTick = tickMarks[currentTickIndex];
                newX = currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset;
                meterBar.rectTransform.anchoredPosition = new Vector3(newX, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
            }
        }
        
    }

    public void GradualMoveMeter(float rate)
    {
        float diffX = (float) Math.Abs(initX - currentTick.rectTransform.anchoredPosition.x);

        print("in");
        print(meterBar.rectTransform.anchoredPosition.x + " > " + initX + " - " + diffX);
        if (meterBar.rectTransform.anchoredPosition.x > initX - diffX && isTakingGradualDamage)
        {
            print("pass1");
            float temp = meterBar.rectTransform.anchoredPosition.x;
            print("(" + temp + " - " + rate +" * "+ Time.deltaTime + ") < " + initX + " - " + diffX);
            print((temp - rate * 5 * Time.deltaTime) + " < " + (initX - diffX));
            if ((temp - rate * 5 * Time.deltaTime) < initX - diffX)
            {
                print("pass2");
                // cling to next closest tickmark
                meterBar.rectTransform.anchoredPosition = new Vector3(currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
                isTakingGradualDamage = false;
            }
            else
            {
                print("fail2");
                float newerX = meterBar.rectTransform.anchoredPosition.x + 1000 * rate * Time.deltaTime;
                print(newerX);
                meterBar.rectTransform.anchoredPosition = new Vector3(newerX , meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
            }
        }
        else
        {
            print("fail1");
            isTakingGradualDamage = false;
        }
    }

    public void setupForGradual()
    {
        isTakingGradualDamage = true;
        initX = meterBar.rectTransform.anchoredPosition.x;

        if (true)
        {
            currentTickIndex = currentTickIndex + 1;
        }
        print("cty " + currentTickIndex);
        currentTick = tickMarks[currentTickIndex];
    }

}
