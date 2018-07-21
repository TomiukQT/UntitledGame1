﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clocks : MonoBehaviour {

    private UIManager ui;

    [SerializeField]
    private Transform sun;

    private float timer = 0.0f;

    [Range(1, 2000)]
    public int timerReduction;



    [HideInInspector]
    public int hours = 0;
    [HideInInspector]
    public int minutes = 0;

    private string[] daysName = { "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
    private int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private int dayInWeek = 0;

    [HideInInspector]
    public int day = 1;
    [HideInInspector]
    public int month = 1;
    [HideInInspector]
    public int year = 2018;


    private string clockText;
    private string dateText;


    private int timeSpeed = 1;

    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UIManager>();
        
    }


    void Update()
    {
        UpdateTime();
        
        UpdateTextUI();
    }


    public void PauseTime()
    {

    }

    public void PlayTime(int speed)
    {
        timeSpeed = speed;
    }

    private void UpdateTime()
    {
        timer += Time.deltaTime * timeSpeed * timerReduction;
        
        if(timer >= 1)
        {
            timer = 0;
            Tick();
        }
    }

    private void Tick()
    {
        minutes++;
        UpdateLight();
        if (minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
        if(hours >= 24)
        {
            hours = 0;
            dayInWeek++;
            day++;
        }
        if(dayInWeek >= 7)
        {
            dayInWeek = 0;
        }
        if (day > daysInMonth[month-1])
        {
            day = 1;
            month++;
        }
        if(month >= 13)
        {
            month = 1;
            year++;
        }
    }

    private void UpdateLight()
    {

        sun.Rotate(0,0.25f,0);

    }

    private void UpdateTextUI()
    {
        
        string h = FormatTimeText(hours.ToString());
        string m = FormatTimeText(minutes.ToString());

        clockText = string.Format("{0}:{1}", h, m);
        ui.ClockText.text = clockText;

        string d = FormatTimeText(day.ToString());
        string mo = FormatTimeText(month.ToString());
        string y = year.ToString();

        dateText = string.Format("{0} {1}/{2} {3}", daysName[dayInWeek], d, mo, y);
        ui.DateText.text = dateText;

    }

    private string FormatTimeText(string s)
    {
        string n = s;
        if (s.Length == 1)
        {
            n = string.Format("0{0}", s);          
        }
        
        return n;
    }
    


}