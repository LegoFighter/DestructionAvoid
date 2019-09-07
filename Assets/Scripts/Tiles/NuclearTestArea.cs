using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearTestArea : MonoBehaviour
{


    public int bonusFactor;
    public float SuccessPropabiltiy;
    public GameEvent BombTestFailed;
    public GameEvent BombTestSuccesful;
    public GameEvent HideTileInfo;
    public GameEvent PlayNuclearSound;

    private Tile baseTile;
    public GameProperties GameProperties;
    private HashSet<GameObject> GameObjectsInRange;

    private Animator animator;


    void Start()
    {
        baseTile = GetComponent<Tile>();
        GameObjectsInRange = new HashSet<GameObject>();
        animator = GetComponent<Animator>();
    }

    public void TestBomb()
    {

        SuccessPropabiltiy = (baseTile.LocalRessources[4] / baseTile.AmountRessourcesMax[4]);

        float testResult = UnityEngine.Random.Range(SuccessPropabiltiy, 1f);

        if (testResult < 0.5f)
        {
            Explode();
        }
        else if (testResult >= 0.5f)
        {
            BombTestSuccesful.Raise();
        }
    }

    public void Explode()
    {
        animator.SetTrigger("Explosion");
        PlayNuclearSound.Raise();
        StartCoroutine(DeleteSelf());
    }


    IEnumerator DeleteSelf()
    {
        yield return new WaitForSecondsRealtime(2);
        GameProperties.TilesToDelete.Add(gameObject);
        GameProperties.TilesToDelete.AddRange(GameObjectsInRange);
        BombTestFailed.Raise();
        HideTileInfo.Raise();
    }




    private void OnTriggerEnter(Collider other)
    {
        Tile tile = other.gameObject.GetComponent<Tile>();
        if (tile != null && !tile.InDangerZone && tile.Type != 0)
        {
            GameObjectsInRange.Add(other.gameObject);
            tile.InDangerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Tile tile = other.gameObject.GetComponent<Tile>();
        if (tile != null && tile.InDangerZone && tile.Type != 0)
        {
            GameObjectsInRange.Remove(other.gameObject);
            tile.InDangerZone = false;
        }
    }



}
