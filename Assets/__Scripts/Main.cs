using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum elemType
{
    water,
    grass,
    fire,
    waterUpgrade,
    grassUpgrade,
    fireUpgrade
}

[System.Serializable]
public struct elemDef
{
    public elemType type;
    public Color color;
    public GameObject projectilePrefab;
    public Color projectileColor;
    public float fireRate;
    public float damage;
    public float velocity;
    
}

public class Main : MonoBehaviour
{
    static public Main S;
    static Dictionary<elemType, elemDef> ELEM_DICT;

    [Header("Set In Inspector")]
    public elemDef[] elemDefs;
    

    void Awake()
    {
        S = this;

        ELEM_DICT = new Dictionary<elemType, elemDef>();
        foreach(elemDef def in elemDefs)
        {
            ELEM_DICT[def.type] = def;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public elemDef GetElemDef(elemType type)
    {
        return  ELEM_DICT[type];
    }
}
