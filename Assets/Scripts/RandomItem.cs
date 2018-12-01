using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomItem : MonoBehaviour {

    private GameObject player;
    private GameObject enemy;
    private RandomItem gmScript;
    private Image itemImage;
    private Sprite pufferfish;
    private Sprite squidInk;
    private Sprite seagulls;
    private Sprite treasureChest;
    private Sprite imgPlaceholder;
    private int itemNumber;
    private int wait;
    private Vector3 puffOffsetE;
    private Vector3 inkOffsetE;

    // Use this for initialization
    void Start() {

        itemImage = GameObject.Find("ItemImage").GetComponent<Image>();
        gmScript = GameObject.Find("GameManager").GetComponent<RandomItem>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        pufferfish = Resources.Load<Sprite>("Sprites/Items/Pufferfish");
        squidInk = Resources.Load<Sprite>("Sprites/Items/SquidInk");
        seagulls = Resources.Load<Sprite>("Sprites/Items/Seagulls");
        treasureChest = Resources.Load<Sprite>("Sprites/Items/TreasureChest");
        imgPlaceholder = Resources.Load<Sprite>("Sprites/Items/ItemPlaceholder");
        wait = Random.Range(1, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gmScript.RandomizeItem();
        }
        if (other.tag == "Enemy")
        {
            gmScript.EnemyUseItem();
        }
        Destroy(gameObject);
    }

    public void RandomizeItem()
    {
        itemNumber = Random.Range(0, 3);

        switch (itemNumber)
        {
            case 0:
                itemImage.sprite = pufferfish;
                break;
            case 1:
                itemImage.sprite = squidInk;
                break;
            case 2:
                itemImage.sprite = seagulls;
                break;
            case 3:
                itemImage.sprite = treasureChest;
                break;
            default:
                Debug.Log("None");
                break;
        }
    }

    public void UseItem()
    {
        Vector3 puffOffset = new Vector3(player.transform.position.x, player.transform.position.y - 2f, player.transform.position.z);
        Vector3 inkOffset = new Vector3(player.transform.position.x, player.transform.position.y - 2f, player.transform.position.z);
        switch (itemNumber)
        {
            case 0:
                Instantiate(Resources.Load("Prefabs/Pufferfish"), puffOffset, player.transform.rotation);
                break;
            case 1:
                Instantiate(Resources.Load("Prefabs/InkSlick"), inkOffset, player.transform.rotation);
                break;
            case 2:
                GameObject seaObj = Instantiate(Resources.Load("Prefabs/SeagullAttack"), enemy.transform.position, Quaternion.identity, enemy.transform) as GameObject;
                Vector3 seaObjAdj = new Vector3(-.5f, 1.5f, 0);
                Vector3 seaObjScale = new Vector3(1.2f, 1.2f, 1);
                seaObj.transform.position += seaObjAdj;
                seaObj.transform.localScale = seaObjScale;
                break;
            case 3:
                Debug.Log("treasure!!");
                break;
            default:
                Debug.Log("None");
                break;
        }

        itemImage.sprite = imgPlaceholder;

    }

    public void EnemyUseItem()
    {
        int enemyItem = Random.Range(0, 2);
        switch (enemyItem)
        {
            case 0:
                StartCoroutine(EnemyWaitPuff());
                break;
            case 1:
                StartCoroutine(EnemyWaitInk());
                break;
            case 2:
                StartCoroutine(EnemyWaitGulls());
                break;
            default:
                Debug.Log("None");
                break;
        }
    }

    IEnumerator EnemyWaitPuff()
    {
        yield return new WaitForSeconds(wait);
        puffOffsetE = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 2f, enemy.transform.position.z);
        Instantiate(Resources.Load("Prefabs/Pufferfish"), puffOffsetE, Quaternion.identity);
    }

    IEnumerator EnemyWaitInk()
    {
        yield return new WaitForSeconds(wait);
        inkOffsetE = new Vector3(enemy.transform.position.x, enemy.transform.position.y - 2f, enemy.transform.position.z);
        Instantiate(Resources.Load("Prefabs/InkSlick"), inkOffsetE, enemy.transform.rotation.normalized);
    }

    IEnumerator EnemyWaitGulls()
    {
        yield return new WaitForSeconds(wait);
        Instantiate(Resources.Load("Prefabs/SeagullAttack"), player.transform.position, Quaternion.identity, player.transform);
    }
}
