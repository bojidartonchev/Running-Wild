using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Assets.Scripts.Notifier;
using UnityEngine.UI;

public class Notifier : MonoBehaviour {

    private static Notifier instance;
    private GameObject notifierWindow;
    private float visibilityTime = 3f;
    private Queue<Notification> notifications = new Queue<Notification>();
    private Vector2 boxPossition = new Vector2(600, 200);
    private bool showingNotification;

    public static Notifier Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject fbm = new GameObject("Notifier");
                fbm.AddComponent<Notifier>();
            }
            return instance;
        }
    }
    private GameObject NotifierWindow
    {
        get
        {
            if (notifierWindow == null)
            {
                notifierWindow = GameObject.Find("NotifierWindow");
            }
            return notifierWindow;
        }
    }

    public void Notify(string message, string description, NotificationType type)
    {
        notifications.Enqueue(new Notification(message, description,type));
        
        if (!showingNotification)
        {
            StartCoroutine(ShowNotificationBox());
        }
    }

    IEnumerator ShowNotificationBox()
    {
        this.showingNotification = true;

        while (showingNotification)
        {
            var messageToShow = notifications.Dequeue();
            this.FillBox(messageToShow);
            // show box
            yield return StartCoroutine(AnimateBox());
            if (notifications.Count == 0)
            {
                showingNotification = false;
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    IEnumerator AnimateBox()
    {
        //plays sound if there is any attached
        if (this.NotifierWindow.GetComponent<AudioSource>() != null)
        {
            this.NotifierWindow.GetComponent<AudioSource>().Play();
        }

        // come in
        this.NotifierWindow.GetComponent<Animator>().SetTrigger("Show");

        yield return new WaitForSeconds(this.visibilityTime);

        // go out
        this.NotifierWindow.GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(1.5f);
    }

    private void FillBox(Notification notification)
    {
        this.NotifierWindow.transform.Find("FormTitle").GetComponent<Text>().text = ToDescriptionString(notification.Type);
        this.NotifierWindow.transform.Find("Title").GetComponent<Text>().text = notification.Title;
    }

    private static string ToDescriptionString(NotificationType val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}

public enum NotificationType
{
    [Description("Achievement Completed")]
    Achievement,
    [Description("Error")]
    Error
}
