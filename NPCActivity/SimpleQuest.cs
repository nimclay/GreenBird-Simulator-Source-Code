using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleQuest : MonoBehaviour
{
    
    [System.Serializable]
    public class questID{
        public string questName;
        public string[] itemsRequired;
        public string completionFunction;

    }
    public questID[] quests;
    float amountofQuests;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
