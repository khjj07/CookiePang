using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;

public class TeleportationBlock : Block
{
    public TeleportationBlock destination;
    private bool _ballEntered = false;
    public Sprite[] sprite;
    public Vector2 savedVelocity;
    protected override void Start()
    {
        base.Start();
        foreach (var block in GameManager.instance.blocks)
        {
            if (block != this && block as TeleportationBlock && hp == block.hp)
            {
                destination = (TeleportationBlock)block;
                block.GetComponent<SpriteRenderer>().sprite = sprite[hp - 1];
                break;
            }
        }
        if (!SceneManager.GetActiveScene().name.Equals("SandBox"))
            _textMeshPro.gameObject.SetActive(false);

        this.UpdateAsObservable()
            .Where(_ => GameManager.instance.ball.isFloor)
            .Subscribe(_ => { _ballEntered = false; });
    }
    public override void Hit(int damage)
    {
        if (!_ballEntered)
        {
            if (destination)
            {
                _ballEntered = true;
                destination._ballEntered = true;

               
                EffectManager.instance.PlayEffect(3, transform.gameObject, 2f);

                var ballBody = GameManager.instance.ball.GetComponent<Rigidbody2D>();
                savedVelocity = ballBody.velocity;
                ballBody.velocity = Vector2.zero;
                SoundManager.instance.PlaySound(1, "TeleportSound");
                GameManager.instance.ball.gameObject.SetActive(false);
                StartCoroutine("Teleport");
                    
            }
        }
    }

    public IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.ball.gameObject.SetActive(true);
        GameManager.instance.ball.GetComponent<Rigidbody2D>().velocity = savedVelocity;
        GameManager.instance.ball.transform.position = destination.transform.position;
        EffectManager.instance.PlayEffect(3, destination.gameObject, 2f);
        SoundManager.instance.PlaySound(1, "TeleportSound");
        yield return null;
    }

    public override void Shock(int damage)
    {

    }

    public override BlockData ToData(int row, int column)
    {
        Vector2Int index = GameManager.instance.GetBlockIndex(destination);
        TeleportationBlockData data = new TeleportationBlockData(hp, row, column, index.x, index.y);
        //data.position = transform.position;
        return data;
    }
    public override void GetData(BlockData data)
    {
        hp = data.hp;
        TeleportationBlockData tel = data as TeleportationBlockData;
        if (tel != null && GameManager.instance.blocks[tel.destinationRow, tel.destinationCol])
        {
            var telInstance2 = GameManager.instance.blocks[tel.destinationRow, tel.destinationCol] as TeleportationBlock;
            destination = telInstance2;
            telInstance2.destination = this;

        }
    }
}