using UnityEngine.SceneManagement;
using UnityEngine;

public class car_select : MonoBehaviour
{
    public GameObject car1;
    public string Tag = "Menu";

    private int ID = 0;
    private readonly string Select = "Select";

    public void Next()
    {
        ID++;
        if (ID > car1.transform.childCount - 1)
        {
            ID = 0;
            car1.transform.GetChild(car1.transform.childCount - 1).gameObject.SetActive(false);
        }
        else
            car1.transform.GetChild(ID - 1).gameObject.SetActive(false);
        car1.transform.GetChild(ID).gameObject.SetActive(true);
        PlayerPrefs.SetInt(Select, ID);

    }
    public void Prev()
    {
        ID--;
        if (ID < 0)
        {
            ID = car1.transform.childCount - 1;
            car1.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
            car1.transform.GetChild(ID + 1).gameObject.SetActive(false);
        car1.transform.GetChild(ID).gameObject.SetActive(true);
        PlayerPrefs.SetInt(Select, ID);
    }
}
