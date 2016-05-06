using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Notifier : MonoBehaviour {

    private static Notifier instance;
    private GameObject notifierWindow;
    private float visibilityTime = 2f;
    private Queue<string> notifications = new Queue<string>();
    private Vector2 boxPossition = new Vector2(600, 200);
    private bool showingNotification;
    private string messageToShow;
    

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

    public void Notify(string message)
    {
        notifications.Enqueue(message);
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
            this.messageToShow = notifications.Dequeue();
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
        // come in
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            boxPossition.y = Mathf.Lerp(-200, 100, t);
            yield return null;
        }

        yield return new WaitForSeconds(this.visibilityTime);

        // go out
        while (t > 0)
        {
            t -= Time.deltaTime;
            boxPossition.y = Mathf.Lerp(-200, 100, t);
            yield return null;
        }
    }
    void OnGUI()
    {
        if (showingNotification)
        {
            this.NotifierWindow.transform.position = new Vector2(this.NotifierWindow.transform.position.x, boxPossition.y);
            this.NotifierWindow.transform.Find("Title").GetComponent<Text>().text = this.messageToShow;
        }
    }
}
