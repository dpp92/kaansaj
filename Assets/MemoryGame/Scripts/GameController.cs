using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    [SerializeField]
    private Sprite bgImage;

    public List<Button> btns = new List<Button>();

    public Sprite[] imgs;
    List<Sprite> game_imgs = new List<Sprite>();

    public bool first, second;
    private int countg, count_correct, gameg;
    private int first_index, second_index;
    private string firstg, secondg;

    void Awake()
    {
        imgs = Resources.LoadAll<Sprite>("Sprites/Candy");
    }

    // Use this for initialization
    void Start()
    {
        getButtons();
        AddLIsteners();
        AddImages();
        Shuffle(game_imgs);
        gameg = game_imgs.Count / 2;
    }

    void AddImages()
    {
        int count = btns.Count;
        int index = 0;

        for(int i = 0; i < count; i++)
        {
            if (index == count / 2) { index = 0; }
            game_imgs.Add(imgs[index]);
            index++;

        }

    }

    void getButtons()
    {
		Debug.Log ("function getButtons");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameButton");

		for(int i = 0; i < objs.Length; i++)
        {
            btns.Add(objs[i].GetComponent<Button>());
			Debug.Log (objs[i].GetInstanceID());
            btns[i].image.sprite = bgImage;
        }

    }

    public void AddLIsteners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => ClickButton());
        }

    }

    public void ClickButton()
    {
        Debug.Log("Clicked! " + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        if (!first)
        {
            first = true;
            first_index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            btns[first_index].image.sprite = game_imgs[first_index];
            firstg = game_imgs[first_index].name;

        }
        else if (!second)
        {

            second = true;
            second_index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            btns[second_index].image.sprite = game_imgs[second_index];
            secondg = game_imgs[second_index].name;
            countg++;
            StartCoroutine(checkIfMatch());

        }
    }

    IEnumerator checkIfMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstg == secondg) {
            yield return new WaitForSeconds(0.5f);
            btns[first_index].interactable = false;
            btns[second_index].interactable = false;
            //btns[first_index].image.color = new Color(0, 0, 0, 0);
            //btns[second_index].image.color = new Color(0, 0, 0, 0);
            checkIfFinished();
        }
        else
        {
            btns[first_index].image.sprite = bgImage;
            btns[second_index].image.sprite = bgImage;

        }
        yield return new WaitForSeconds(0.5f);
        first = second = false;
        Text txt = GameObject.FindGameObjectWithTag("Score").GetComponent <Text>() ;
        txt.text = "Intentos: " + countg + " - Correcto: "+count_correct;

    }

    void checkIfFinished()
    {
        count_correct ++;
        if (gameg == count_correct) {
            Debug.Log("Game Finished");
            Debug.Log("Tardaste " + countg + " Intentos");
        }
    }

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int random = Random.Range(0, list.Count);
            list[i] = list[random];
            list[random] = temp;

        }

    }
}
