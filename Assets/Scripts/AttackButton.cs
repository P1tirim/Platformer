using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Button btn = null;

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(delegate { ParameterOnClick(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ParameterOnClick()
    {
        
    }
}
