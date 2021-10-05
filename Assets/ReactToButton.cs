using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactToButton : MonoBehaviour
{
    public Material color1;
    public Material color2;
    int material_index = 1;

    // Start is called before the first frame update
    void Start()
    {
        //1 gives it the default value if its never been set before
        int material_index = PlayerPrefs.GetInt("cube.material", 1);
        this.UpdateMaterial();

        //EX find components in the scene to change (rather than grouping them under an empty object)
        //Find the button by its name (there are alternative means to find the button object)
        var button_object = GameObject.Find("Change Color Button");
        //grab the button component
        var button = button_object.GetComponent<Button>();
        //add the function you want to call to the onClick listener
        button.onClick.AddListener(this.ButtonWasClicked);
    }

    public void Update()
    {
        //EX using quaternion to rotate cube
        // var transform = this.GetComponent<Transform>();
        // var local_rotation = transform.localRotation;
        // local_rotation *= Quaternion.Euler(1.0f, 0.0f, 0.0f);
        // transform.localRotation = local_rotation;

        //QUATERNION USAGE EXAMPLE
        // var quat1 = Quaternion.FromEuler(90,0,0);
        // var quat2 = Quaternion.FromEuler(0,90,0);
        // var quat3 = quat1 * quat2; //this gives you the first quaternion rotated by the second
    }

    private void UpdateMaterial() {
        var renderer = this.GetComponent<MeshRenderer>();
        if (this.material_index == 1)
            renderer.material = this.color1;
        else
            renderer.material = this.color2;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     var renderer = this.GetComponent<MeshRenderer>();
    //     var color = renderer.material.color;
    //     color.r += Time.deltaTime;
    //     renderer.material.color = color;
    // }

    public void ButtonWasClicked()
    {
        this.material_index += 1;
        if (this.material_index > 2)
            this.material_index = 1;
        this.UpdateMaterial();
        this.Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("cube.material", this.material_index);
    }

    public void SliderMoved(float v)
    {
        Debug.Log("slider moved! Now it's set to " + v);
    }
}
