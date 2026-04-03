using UnityEngine;
using UnityEngine.Timeline;

public class Boss : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float health = 1000;
    public Vector3[] safePoints;
    
    [Header("Set Dynamically")]
    public elemType type = elemType.water;
    public elemDef def;
    public float attackID;
    public int numberOfAttacks;

    private int i = 0;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("ElemSelect", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        GameObject coll = other.gameObject;

        switch (coll.tag)
        {
            case "WizardProjectile":
                Projectile p = coll.GetComponent<Projectile>();
                health -= p.damage;
                Destroy(coll);
                break;

            case "Wizard":
                Wizard.S.DamageTaken(2);
                break;
            default:
                break;
        }       
    }

    void ElemSelect()
    {
        switch (2)//Random.Range(1,4))
        {
            case 1:
                def = Main.GetElemDef(elemType.water);
                break;
            case 2:
                def = Main.GetElemDef(elemType.fire);
                transform.position = safePoints[Random.Range(0,9)];
                attackID = Random.Range(1,1);
                numberOfAttacks = Random.Range(5,10);
                Invoke("FireAttack", .5f);
                break;
            case 3:
                def = Main.GetElemDef(elemType.grass);
                break;
            default:
                break;
        }
    }

    void FireAttack()
    {
        switch (attackID)
        {
            case 1:
                float deltaX = transform.position.x - Wizard.S.transform.position.x;
                float deltaY = transform.position.y - Wizard.S.transform.position.y;
                float rotY = 180f / 3.14159f * Mathf.Atan2(deltaY, deltaX);

                print(rotY.ToString());
                transform.rotation = Quaternion.Euler(0,0,rotY);
                
                Projectile p = MakeProjectile();
                p.transform.rotation = Quaternion.Euler(-rotY, 45, 45 + rotY);
                p.rigid.velocity =  Quaternion.Euler(0,0,rotY) * Vector3.left * def.velocity * 0.5f;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.Euler(-rotY + 15, 45, 45 + rotY - 15);
                p.rigid.velocity =  Quaternion.Euler(0,0,rotY - 15) * Vector3.left * def.velocity * 0.5f;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.Euler(-rotY - 15, 45, 45 + rotY + 15);
                p.rigid.velocity =  Quaternion.Euler(0,0,rotY + 15) * Vector3.left * def.velocity * 0.5f;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.Euler(-rotY + 30, 45, 45 + rotY - 30);
                p.rigid.velocity =  Quaternion.Euler(0,0,rotY - 30) * Vector3.left * def.velocity * 0.5f;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.Euler(-rotY - 30, 45, 45 + rotY + 30);
                p.rigid.velocity =  Quaternion.Euler(0,0,rotY + 30) * Vector3.left * def.velocity * 0.5f;
                

                break;
            default:
                break;
        }

        if (i >= numberOfAttacks)
        {
            i = 0;
            Invoke("ElemSelect", 2);
        } else
        {
            i++;
            Invoke("FireAttack", 0.5f);
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
            go.tag = "EnemyProjectile";
            go.layer = LayerMask.NameToLayer("EnemyProjectile");
        }

        go.transform.position = transform.position;
        go.transform.localScale = new Vector3(.4f,.4f,.8f);
        Projectile p = go.GetComponent<Projectile>();
        p.damage = def.damage;
        p.render.material.color = def.projectileColor;

        return p;
    }
}
