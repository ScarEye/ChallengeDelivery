using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float days = 0,money=0;
    public Text daysTxt,moneyTxt;

    public Animator nextDayAnim;

    private void Start()
    {
        daysTxt.text = days.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // NextDay Animation and counter
    public IEnumerator NextDay()
    {
        nextDayAnim.SetTrigger("NextDay");
        yield return new WaitForSeconds(2.5f);
        days++;
        daysTxt.text = days.ToString();
    }

    // add money
    public void AddMoney(float m)
    {
        money += m;
        moneyTxt.text = money.ToString();
    }
}


