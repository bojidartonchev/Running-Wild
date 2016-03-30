using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Assets.Scripts.UI
{
    public class DistanceCounter : MonoBehaviour
    {
        public Text textField;

        private float distance;
        private Vector3 lastPosition;
        private int highestDistance;

        // Use this for initialization
        void Start ()
        {
            this.lastPosition = transform.position;
            this.highestDistance = this.LoadHighestScore();
            Debug.Log("Highest distance: "+this.highestDistance);
        }
	
        // Update is called once per frame
        void Update ()
        {
            this.distance += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            this.textField.text = String.Format("{0,10:D10}", (int)this.distance);
        }

        void OnApplicationQuit()
        {
           this.SaveHighestScore();
        }

        public void SaveHighestScore()
        {
            if (this.distance > this.highestDistance)
            {
                this.highestDistance = (int) this.distance;
                PlayerPrefs.SetInt("highestScore", this.highestDistance);
            }
        }

        private int LoadHighestScore()
        {
            return PlayerPrefs.GetInt("highestScore");
        }
    }
}
