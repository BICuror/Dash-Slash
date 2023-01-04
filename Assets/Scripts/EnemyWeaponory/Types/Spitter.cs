using UnityEngine;

public class Spitter : EnemyTaskManager
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private float bulletSpeed;

    [SerializeField] private int _bulletsAmount;

    [SerializeField] private float _spread;

    [SerializeField] private float _sizeSpread;

    [SerializeField] private ParticleSystem partSystem;

    [SerializeField] private Animator anim;

    private ShootingModule shootingModule;

    private MoveAgent agent;



    private void Awake()
    {
        agent = GetComponent<MoveAgent>();

        shootingModule = GetComponent<ShootingModule>();
    }

    protected override void PreperateTask() 
    {
        anim.Play("EnemyPrepeare");
    }

    protected override void DoTask()
    {
        partSystem.Play();

        anim.Play("EnemyLineTask");

        agent.KnockBack(transform.right, 8f);

        for (int i = 0; i < _bulletsAmount; i++)
        {
            GameObject currentBullet = Instantiate(bullet, transform.position, transform.rotation);

            currentBullet.transform.Rotate(0f, 0f, Random.Range(-_spread, _spread));

            float scale = Random.Range(-_sizeSpread, _sizeSpread);

            currentBullet.transform.localScale = new Vector3(1f + scale, 1f + scale, 1f);

            currentBullet.GetComponent<Rigidbody2D>().AddForce(currentBullet.transform.right * (bulletSpeed + Random.Range(-2f, 2f)), ForceMode2D.Impulse);
        }
    }  
}
