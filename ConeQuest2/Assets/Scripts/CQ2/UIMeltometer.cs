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
    public List<Image> tickMarks;

    Image currentTick;
    int currentTickIndex;

    RawImage meterBar;

    float meterBarRightSideOffset;

    private void Awake()
    {

        foreach (RawImage g in RawImage.FindObjectsOfType(typeof(RawImage)))
        {
            if (g.tag == "MeterBar")
            {
                meterBar = g;
            }
        }

        foreach (Image g in Image.FindObjectsOfType(typeof(Image)))
        {
            if (g.tag =="MeterTick")
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
        
    }

    public void MoveMeltBar(float inc)
    {
        float newX;

        print("enter");
        print(inc);

        if (inc < 0)
        {
            if (currentTick != tickMarks[tickMarks.Count - 1])
            {
                if ((tickMarks.Count - 1 - currentTickIndex) - Math.Abs(inc) < 0)
                {
                    print("Player died");
                    // kill player outright
                    print(currentTick.rectTransform.anchoredPosition.x);
                    currentTickIndex = tickMarks.Count - 1;
                    print("CTI       " + currentTickIndex);
                    currentTick = tickMarks[currentTickIndex];
                    print(currentTick.rectTransform.anchoredPosition.x);
                    print(meterBarRightSideOffset);
                    print(currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset);
                    newX = currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset;
                    meterBar.rectTransform.anchoredPosition = new Vector3(newX, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
                }
                else
                {
                    print("player hurt");
                    print(currentTick.rectTransform.anchoredPosition.x);
                    currentTickIndex = currentTickIndex - (int)inc;
                    print("CTI       " + currentTickIndex);
                    currentTick = tickMarks[currentTickIndex];
                    print(currentTick.rectTransform.anchoredPosition.x);
                    print(meterBarRightSideOffset);
                    print(currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset);
                    newX = currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset;
                    meterBar.rectTransform.anchoredPosition = new Vector3(newX, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
                }
                
            }
            
        }
        else if (inc > 0)
        {
            print("positive");
            if (currentTick != tickMarks[0])
            {
                print("not tick 0");
                if (currentTickIndex - Math.Abs(inc) < 0)
                {
                    print("max hp");
                    // player is at max health

                    print(currentTick.rectTransform.anchoredPosition.x);
                    currentTickIndex = 0;
                    currentTick = tickMarks[currentTickIndex];
                    print("CTI       " + currentTickIndex);
                    print(currentTick.rectTransform.anchoredPosition.x);
                    print(meterBarRightSideOffset);
                    print(currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset);
                    newX = currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset;
                    meterBar.rectTransform.anchoredPosition = new Vector3(newX, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
                }
                else
                {
                    print("palyer goes up by inc");

                    print(currentTick.rectTransform.anchoredPosition.x);
                    currentTickIndex = currentTickIndex - (int)inc;
                    currentTick = tickMarks[currentTickIndex];
                    print("CTI       " + currentTickIndex);
                    print(currentTick.rectTransform.anchoredPosition.x);
                    print(meterBarRightSideOffset);
                    print(currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset);
                    newX = currentTick.rectTransform.anchoredPosition.x - meterBarRightSideOffset;
                    meterBar.rectTransform.anchoredPosition = new Vector3(newX, meterBar.rectTransform.anchoredPosition.y, meterBar.rectTransform.localPosition.z);
                }

            }
        }
        
    }

}
