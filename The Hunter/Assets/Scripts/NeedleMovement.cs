using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class NeedleMovement : MonoBehaviour
{
    public Image needle;
    public Text text;
    public int i = 0, frames = 0, targetFrames = 60;
    public List<float> HeartRate = new List<float>();
    public float value = 0, heart_rate, eulerRotateZ, current_pos;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = targetFrames;
        ReadCSV();
        needle = GetComponent<Image>();
        //needle.transform.Rotate(0, 0, 0);
        needle.transform.eulerAngles = new Vector3(0, 0, 130 - HeartRate[i]);
    }

    // Update is called once per frame
    void Update()
    {
        //60 * Time.deltaTime;
        // take input

        if (i < HeartRate.Count)
        {
            if (frames % 60 == 0)
            {
                //value = 90 - needle.transform.eulerAngles.z - /*90 -*/ HeartRate[i];
                // eulerRotateZ is between 0-90 and 270-360
                eulerRotateZ = needle.transform.eulerAngles.z;
                //now eulerRotateZ has to be adjusted for the formula
                //if its between 0-90 -> 40-130, 270-360 -> 130-220
                //formula
                if (eulerRotateZ <= 90 || eulerRotateZ >= 0)
                {
                    eulerRotateZ = 130 - eulerRotateZ;
                }
                else if (eulerRotateZ <= 360 || eulerRotateZ >= 270)
                {
                    eulerRotateZ = 490 - eulerRotateZ;
                }


                // heart_rate should be between 40-220
                heart_rate = /*130 - */HeartRate[i];
                //now heart_rate has to be adjusted
                //or does it? i dont know
                //lets try not to change it


                //counting delta between two values to know which side move the needle to
                value = eulerRotateZ - heart_rate;
                //if new is bigger - the value is negative therefore moving to the right
                //if new is smaller - the value is positive therefore moving to the left
                
                //we have the value, but how do we know if its in the right place?

                //value = needle.transform.eulerAngles.z - HeartRate[i];
                //value = eulerRotateZ - HeartRate[i];
                text.text = HeartRate[i].ToString() + "   i =  " + i + "\n";
                                
                i++;
            }
            else
            {
                needle.transform.Rotate(0, 0, value * Time.deltaTime);
            }
        }
        frames++;

        //needle.transform.Rotate(0, 0, 0.05f);
        /*if (i < HeartRate.Count)
        {
            //works
            
            //needle.transform.eulerAngles = new Vector3(0, 0, 90 - HeartRate[i]);

           
            if (needle.transform.eulerAngles.z < 90 - HeartRate[i])
            {
                while(needle.transform.eulerAngles.z < 90-HeartRate[i])
                {
                    needle.transform.Rotate(0, 0, 0.05f*Time.deltaTime);
                    
                }
            }
            else
            {
                while(needle.transform.eulerAngles.z > 90- HeartRate[i])
                {
                    needle.transform.Rotate(0, 0, -0.05f * Time.deltaTime);
                    
                }
            }

            rotateZ = needle.transform.rotation.z;
            eulerRotateZ = needle.transform.eulerAngles.z;
            /*value = HeartRate[i] - needle.transform.rotation.z;
            if (i != 0)
            {
                while()
                value = HeartRate[i] - HeartRate[i - 1];
                // nie jesty zle needle.transform.Rotate(0, 0, -value);//HeartRate[i]);
                needle.transform.eulerAngles = new Vector3(0, 0, 90 - HeartRate[i]);
                text.text = HeartRate[i].ToString() + "   i =  " + i;
            }*/
        //needle.transform.Rotate(0, 0, value);//HeartRate[i]);
        //text.text = HeartRate[i].ToString() + "   i =  " + i;
        //}
        //System.Threading.Thread.Sleep(1000);


    }

    // Reads from csv file
    void ReadCSV()
    {
        
        StreamReader data = new StreamReader("E:\\UnityProjects\\the-hunter\\The Hunter\\Assets\\Data\\short_file.csv");
        bool endOfFile = false;
        while (!endOfFile)
        {
            
            string data_string = data.ReadLine();
            if (data_string == null)
            {
                endOfFile = true;
                break;
            }
            var data_values = data_string.Split(new char[] { ',' });
            if (data_values[6][0] < 57)
            {
                HeartRate.Add((float.Parse(data_values[6], CultureInfo.InvariantCulture.NumberFormat)));
            }
            
        }
        data.Close();
    }
}
