using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SmartDLL;

public class FileDialogManager : MonoBehaviour
{
    //public Text eText;
    public InputField einput;
    public Button openExplorerButton;
    //public Image eimage;

    public SmartFileExplorer fileExplorer = new SmartFileExplorer();

    private bool readText = false;

    void OnEnable()
    {
        openExplorerButton.onClick.AddListener(delegate { ShowExplorer(); });
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fileExplorer.resultOK && readText)
        {
            //ReadText(fileExplorer.fileName);
            einput.text = fileExplorer.fileName;
            readText = false;
        }
    }
    /*
    void ShowExplorer()
    {
        string initialDir = @"C:\";
        bool restoreDir = true;
        string title = "Open a Text File";
        string defExt = "txt";
        string filter = "txt files (*.txt)|*.txt";

        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);
        readText = true;
    }
    */
    /*
    void ShowExplorer()
    {
        string initialDir = @"C:\";
        bool restoreDir = true;
        string title = "Open a png File";
        string defExt = "png";
        string filter = "png files (*.png)|*.png";

        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);
        readText = true;
    }*/

    void ShowExplorer()
    {
        string initialDir = @"C:\";
        bool restoreDir = true;
        string title = "Open a png File";
        string defExt = "png";
        string filter = "png files (*.png)|*.png|jpg files(*.jpg)|*.jpg";

        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);
        readText = true;
    }

    /*
    void ReadText(string path)
    {
        eText.text = File.ReadAllText(path);

    }*/
}