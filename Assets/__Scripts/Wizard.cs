using Unity.VisualScripting;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    static public Wizard S;

    [Header("Set In Inspector")]
    public float speed = 10f;
    public float speedFocus = 5f;

    [Header("Set Dynamically")] [SerializeField]
    private elemType type = elemType.water;
    public elemDef def;
    public float lastShotTime;


    void Awake()
    {
        if (S == null)
        {
            S = this;
        } else
        {
            Debug.LogError("Wizard.Awake Attempted to assign second Wizard.S");
        }
    }

    void Start()
    {
        def = Main.GetElemDef(type);
        Invoke("Shoot", def.fireRate);
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        float slow = Input.GetAxis("Fire1") + 1;

        Vector3 pos = transform.position;
        pos.x += speed * xAxis * Time.deltaTime / slow;
        pos.y += speed * yAxis * Time.deltaTime / slow;
        transform.position = pos;
    }

    void Shoot()
    {
        Projectile p;

        print("shoot" + def.damage.ToString());

        switch (type)
        {
            case elemType.water:
                p = MakeProjectile();
                Invoke("Shoot", def.fireRate);
                break;
            case elemType.fire:

                break;
            case elemType.grass:

                break;
            default:

                break;
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(def.projectilePrefab);
        if (transform.gameObject.tag == "Wizard")
        {
            go.tag = "WizardProjectile";
            go.layer = LayerMask.NameToLayer("WizardProjectile");
        } else
        {
            go.tag = "WizardProjectile";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }

        go.transform.position = transform.position + new Vector3(0.6f,0,0);
        Projectile p = go.GetComponent<Projectile>();
        p.damage = def.damage;

        Vector3 vel = Vector3.right * def.velocity;
        p.rigid.velocity = vel;

        return p;
    }
}
