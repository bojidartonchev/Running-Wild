using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Assets.Scripts.UI
{
    public class DistanceCounter : MonoBehaviour
    {
        public Text textField;
        
        private Vector3 lastPosition;
        private int highestDistance;

        public float Distance { get; private set; }
       

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
            this.Distance += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            this.textField.text = String.Format("{0,10:D10}", (int)this.Distance);
        }

        void OnApplicationQuit()
        {
           this.SaveHighestScore();
        }

        public void SaveHighestScore()
        {
            if (this.Distance > this.highestDistance)
            {
                this.highestDistance = (int) this.Distance;
                PlayerPrefs.SetInt("highestScore", this.highestDistance);
                FacebookManager.Instance.SetScore(this.highestDistance);
            }
        }

        private int LoadHighestScore()
        {
            return PlayerPrefs.GetInt("highestScore");
        }
    }
}
