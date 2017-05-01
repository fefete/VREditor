using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class JsonData
{
    public int objectID;

    //old transform position information, for deleting
    //the object when importing
    public float old_x;
    public float old_y;
    public float old_z;

    //current position
    public float t_x;
    public float t_y;
    public float t_z;

    //Current rotation
    public float r_x;
    public float r_y;
    public float r_z;

    //current scale
    public float s_x;
    public float s_y;
    public float s_z;

    //material name and object name
    public string mat_name;
    public string obj_name;
}
