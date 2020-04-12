using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HordeOrganizer : MonoBehaviour
{
    List<FlockManager> hordeList;
    [CanBeNull] public Dropdown dropDownMenu;
    [SerializeField] [Min(1)] int maxHordes = 1;
    public GameObject flockManagerPrefab;
    [CanBeNull] public Text hordeHealth;

    private int activeHorde = 0;

    // Start is called before the first frame update
    void Start()
    {
        hordeList = new List<FlockManager>();
        hordeList.Add(transform.parent.GetComponentInChildren<FlockManager>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && hordeList.Count < maxHordes)
        {
            FlockManager temp = Instantiate(flockManagerPrefab).GetComponent<FlockManager>();

            if (hordeList[activeHorde].SplitHorde(temp))
            {
                hordeList.Add(temp);
                //activeHorde = hordeList.IndexOf(temp);
                dropDownMenu?.options.Add(new Dropdown.OptionData());
                dropDownMenu?.SetValueWithoutNotify(activeHorde);
                SwitchActive();
            }
            else
                Destroy(temp.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int hordeToAbsorb = IndexOfClosestHorde();
            if (hordeToAbsorb >= 0)
            {
                foreach (Flocker zombie in hordeList[hordeToAbsorb].getZombieList())
                {
                    zombie.GetComponent<MeshRenderer>().materials[1].SetFloat("_Outline", 0f);
                }

                hordeList[activeHorde].AbsorbHorde(hordeList[hordeToAbsorb]);
                hordeList.RemoveAt(hordeToAbsorb);

                if (hordeToAbsorb < activeHorde)
                    activeHorde--;

                dropDownMenu.options.RemoveAt(dropDownMenu.options.Count - 1);
                dropDownMenu.SetValueWithoutNotify(activeHorde);
            }
        }

        UpdateDropDown();
        UpdateHealth();
    }

    public int IndexOfClosestHorde()
    {
        float distance = 0;
        float tempDistance;
        int index = -1;

        for (int i = 0; i < hordeList.Count; i++)
        {
            if (index < 0 && activeHorde != i)
            {
                index = i;
                distance = (hordeList[i].transform.position - hordeList[activeHorde].transform.position).magnitude;
            }
            else
            {
                tempDistance = (hordeList[i].transform.position - hordeList[activeHorde].transform.position).magnitude;
                if (activeHorde != i && tempDistance < distance)
                {
                    index = i;
                    distance = tempDistance;
                }
            }
        }

        return index;
    }

    public void SwitchActive()
    {
        hordeList[activeHorde].active = false;
        activeHorde = dropDownMenu.value;
        hordeList[activeHorde].active = true;
    }

    private void UpdateDropDown()
    {
        if (dropDownMenu == null) return;
        for (int i = 0; i < hordeList.Count; i++)
        {
            dropDownMenu.options[i].text = "Horde: " + i + " Size: " + hordeList[i].HordeSize();
        }

        dropDownMenu.captionText.text = "Horde: " + activeHorde + " Size: " + hordeList[activeHorde].HordeSize();
    }

    public void UpdateHealth()
    {
        if (hordeHealth == null) return;
        hordeHealth.text = hordeList[activeHorde].HordeSize().ToString();
    }
}