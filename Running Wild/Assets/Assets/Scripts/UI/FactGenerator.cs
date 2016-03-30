using UnityEngine;
using UnityEngine.UI;

public class FactGenerator : MonoBehaviour {

    public int FactsCount;
	// Use this for initialization
	void Start () {
        Text currentText = GetComponent<Text>();
        string randomFact;
        int randomIndex = UnityEngine.Random.Range(1, FactsCount);
        switch (randomIndex)
        {
            case 1:
                randomFact = "Mushrooms are comprised of 85 - 95 % water.";
                break;
            case 2:
                randomFact = "Mushrooms have their own immune system.";
                break;
            case 3:
                randomFact = "Mushrooms are more closely related in DNA to humans than to plants.";
                break;
            case 4:
                randomFact = "Fungi use antibiotics to fend off other microorganisms that compete with them for food.";
                break;
            case 5:
                randomFact = "The antibiotic penicillin was derived from the fungal species Penicillium.";
                break;
            case 6:
                randomFact = "Psathyrella aquatica is a gilled mushroom that lives completely under water.";
                break;
            case 7:
                randomFact = "There are more amino acids in mushrooms than in corn, peanuts, or soybeans.";
                break;
            default:
                randomFact = "Error.";
                break;
        }
        currentText.text = randomFact;
	}
	

}
