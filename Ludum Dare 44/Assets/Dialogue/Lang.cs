using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conv
{
    public string consays;
    public bool canMove = false;
    //Add next property
}

public class sent
{
    public string consays;
    public string whosays;
    public string whatsays;

    public void addsent(string con, string who, string what)
    {
        consays = con;
        whosays = who;
        whatsays = what;

    }
}

//Return class consisting of say list items and properties for conversation
public class SentConv : List<sent>
{
    public bool canMove = false;
}

public static class Lang
{
    //ivate string[10] mySays;
    private static List<sent> sayList = new List<sent>();
    private static List<conv> conlist = new List<conv>();
    private static bool started;

    //Called from outside the class to get a conversation
    public static SentConv GetConv(string ConvKey)
    {
        if(!started)
        {
            Start();
        }
        //List<sent> bbb = new List<sent>();
        SentConv bbb = new SentConv();
        foreach(sent s in sayList)
        {
            if(s.consays == ConvKey)
            {
                bbb.Add(s);
            }
        }

        foreach(conv c in conlist)
        {
            if (c.consays == ConvKey)
            {
                bbb.canMove = c.canMove;
                //Add next property
                break;
            }
        }

        return bbb;
    }

    private static void say(string con, string who, string what)
    {
        sent b = new sent(); b.addsent(con, who, what); sayList.Add(b);
    }

    private static void beginconv(string con, bool canMove )
    {
        conv c = new conv();
        c.canMove = canMove;
        c.consays = con;
        //Add next property
        conlist.Add(c);
    }

    static void Start()
    {
        beginconv("TollManIntro", true);
        say("TollManIntro", "The Toll Man", "Hey, kiddos. Looks like you wanna get past here.");
        say("TollManIntro", "Player1", "Uhhh, <i>yeah.");
        say("TollManIntro", "The Toll Man", "Well, hand over the money and I'll open up for ya.");
        say("TollManIntro", "Player2", "M-m-money?");
        say("TollManIntro", "Player1", "Yeah, we ain't got any.");
        say("TollManIntro", "The Toll Man", "Well, in that case...");
        say("TollManIntro", "The Toll Man", "<size=130%>YOUR SOUL WILL DO JUST FINE!");
        say("TollManIntro", "The Toll Man", "<size=130%>WELL, WHO'S IT GONNA BE?");
        say("TollManIntro", "Player1", "Not me! Hey, you do it.");

        started = true;
    }

}
