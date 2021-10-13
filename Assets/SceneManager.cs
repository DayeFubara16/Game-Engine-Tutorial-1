using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Text ValueText;

    // Start is called before the first frame update
    private void Start()
    {
        ValueText.text = PersistanceManager.Instance.Value.ToString();
    }
}
