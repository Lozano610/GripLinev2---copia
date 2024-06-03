using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //Usar StreamWriter y StreamReader
using Newtonsoft.Json.Linq; //Para poder usar Json.net y estructuras de datos
using System.Security.Cryptography; //Liber�a para encriptaci�n y desencriptaci�n de informaci�n
using Unity.VisualScripting;
using UnityEngine.SceneManagement;



public class ManagerSavingObjects : MonoBehaviour
{
    public Coins coins;
    public Survivol_Level survivorlevel;
    //public MenuManager manager;
    
    public string save_timertext;
    public int save_coins;
    public int save_indexcar;
    public List<int> save_cars_buy;

    public static ManagerSavingObjects Singleton
    {
        //El get nos sirve para obtener la informaci�n del Singleton
        get
        {
            //Comprobamos primero que la instancia est� vac�a
            if (instance == null)
            {
                //Rellenamos la referencia del Singleton
                instance = FindAnyObjectByType(typeof(ManagerSavingObjects)) as ManagerSavingObjects;
            }
            //Nos devuelve la informaci�n de la instancia
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static ManagerSavingObjects instance;
    public class SaveData
    {
        //Variables para serializar
        public int money;
        public string timertext;
        public int car_index;
        public List<int> cars_buy;
        //Constructor de la clase
        public SaveData(int _coins, string _timer, int _index, List<int> _carsbuy)
        {
            //Rellenamos las variables con las que le pasamos por par�metro
            money = _coins;
            timertext = _timer;
            car_index = _index;
            cars_buy = _carsbuy;
        }
    }
    private void Start()
    {
        //if(SceneManager.GetActiveScene().name == "Brittish_Survivor" || SceneManager.GetActiveScene().name == "Nascar_Survivor" || SceneManager.GetActiveScene().name == "Senach_Survivor")
        //{
        //    string text;
        //    save_timertext = text = survivorlevel.timerText.text; ;
        //    save_coins = Coins.TotalCoins;
        //}
    }
 
    public void Guardar()
    {
        //Ruta de donde queremos leer la informaci�n
        string saveFilePath = Application.persistentDataPath + "/GripLine.sav";
        //Muestra la ruta del archivo por consola
        Debug.Log("Loading from: " + saveFilePath);

        //Creamos un StreamReader que nos permita leer la informaci�n del archivo de guardado
        StreamReader sr = new StreamReader(saveFilePath);
        //Creamos un string donde guardar la informaci�n que leemos
        string jsonStringc = sr.ReadToEnd();
        //Al acabar la lectura de datos cerramos el StreamReader
        sr.Close();
        //Instanciamos la clase anidada para cargar las variables de esta
        //La informaci�n recibida del archivo de guardado sobreescribir� los campos oportunos del jsonString
        //SaveData sdc = JsonUtility.FromJson<SaveData>(jsonStringc);
        //Realmente cargamos la informaci�n del archivo de guardado en las variables de Unity
        //save_coins = sdc.money;
        Debug.Log(save_coins);
        if (SceneManager.GetActiveScene().name == "PrincipalMenus")
        {
            save_coins = Coins.TotalCoins;
            save_indexcar = MenuManager.Singleton.Selectedcar_index;
            save_cars_buy = new List<int>(MenuManager.Singleton.CarsComprados);
        }
        else if (SceneManager.GetActiveScene().name == "Brittish_Survivor" || SceneManager.GetActiveScene().name == "Nascar_Survivor" || SceneManager.GetActiveScene().name == "Senach_Survivor")
        {
            save_timertext = survivorlevel.timerText.text;
            save_coins = Coins.TotalCoins;
        }
        else
            save_coins += Coins.TotalCoins;
        JObject jSaveGame = new JObject();
        
            SaveData sd = new SaveData(save_coins, save_timertext, save_indexcar, save_cars_buy);
            string jsonString = JsonUtility.ToJson(sd);
            //Ruta donde queremos guardar la informaci�n
            //string saveFilePath = Application.persistentDataPath + "/GripLine.sav";
            //Creamos un StreamWriter para guardar la informaci�n en la ruta dada
            StreamWriter sw = new StreamWriter(saveFilePath);
            //Muestra la ruta del archivo por consola
            //Debug.Log("Saving to: " + saveFilePath);
            //Escribimos la informaci�n que queremos en el archivo de guardado
            sw.WriteLine(jsonString);
            //Al acabar cerramos el StreamWriter
            sw.Close();
        //Creamos un array de bytes para guardar el array que nos devuelve el m�todo Encrypt para que pueda ser usado
        //byte[] encryptSavegame = Encrypt(jSaveGame.ToString());
        ////Escribimos esta informaci�n en el archivo de guardado, ya encriptada la informaci�n en su ruta 
        //File.WriteAllBytes(saveFilePath, encryptSavegame);
        ////Muestra la ruta del archivo por consola
        //Debug.Log("Saving to: " + saveFilePath);
        Debug.Log(save_coins);
        
    }

    public void Cargar()
    {
        
            //Ruta de donde queremos leer la informaci�n
            string saveFilePath = Application.persistentDataPath + "/GripLine.sav";
            //Muestra la ruta del archivo por consola
            Debug.Log("Loading from: " + saveFilePath);

            //Creamos un StreamReader que nos permita leer la informaci�n del archivo de guardado
            StreamReader sr = new StreamReader(saveFilePath);
            //Creamos un string donde guardar la informaci�n que leemos
            string jsonString = sr.ReadToEnd();
            //Al acabar la lectura de datos cerramos el StreamReader
            sr.Close();
            //Instanciamos la clase anidada para cargar las variables de esta
            //La informaci�n recibida del archivo de guardado sobreescribir� los campos oportunos del jsonString
        //    SaveData sd = JsonUtility.FromJson<SaveData>(jsonString);
        //    //Realmente cargamos la informaci�n del archivo de guardado en las variables de Unity
        //    save_coins = sd.money;
        //    save_timertext = sd.timertext;
        //    save_indexcar = sd.car_index;
        //save_cars_buy = sd.cars_buy;

        //if (SceneManager.GetActiveScene().name == "Brittish_Survivor" || SceneManager.GetActiveScene().name == "Nascar_Survivor" || SceneManager.GetActiveScene().name == "Senach_Survivor")
        //{ 
        //    survivorlevel.timerText.text = save_timertext.ToString();
        //}
        //else
        //    Coins.TotalCoins = save_coins;
        //    coins.CoinsNumber.text = save_coins.ToString();
        //if(MenuManager.Singleton!=null)
        //{
        //    MenuManager.Singleton.Selectedcar_index = save_indexcar;
        //    MenuManager.Singleton.CarsComprados = new List<int>(save_cars_buy);
        //}
        ////Creamos un array con la informaci�n encriptada recibida
        //byte[] decryptedSavegame = File.ReadAllBytes(saveFilePath);
        ////Creamos un array donde guardar la informaci�n desencriptada recibida
        //string JsonCript = Decrypt(decryptedSavegame);

    }
    ///*PARA ENCRIPTAR Y DESENCRIPTAR LA INFORMACI�N DEL ARCHIVO DE GUARDADO
    //*/

    ////Clave generada para la encriptaci�n en formato bytes, 16 posiciones
    //byte[] _key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
    ////Vector de inicializaci�n para la clave
    //byte[] _initializationVector = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

    ////Encriptamos los datos del archivo de guardado que le pasaremos en un string
    //byte[] Encrypt(string message)
    //{
    //    //Usamos esta librer�a que nos permitir� a trav�s de una referencia crear un encriptador de la informaci�n
    //    AesManaged aes = new AesManaged();
    //    //Para usar este encriptador le pasamos tanto la clave como el vector de inicializaci�n que hemos creado nosotros arriba
    //    ICryptoTransform encryptor = aes.CreateEncryptor(_key, _initializationVector);
    //    //Lugar en memoria donde guardamos la informaci�n encriptada
    //    MemoryStream memoryStream = new MemoryStream();
    //    //Con esta referencia podremos escribir en el MemoryStream de arriba la informaci�n ya encriptada usando el encriptador con sus claves que ya hab�amos creado
    //    CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
    //    //Con el StreamWriter podemos escribir en el archivo la informaci�n encriptada, que se habr� guardado en el MemoryStream
    //    StreamWriter streamWriter = new StreamWriter(cryptoStream);

    //    //Usando todo lo anterior, guardamos en el archivo de guardado el json que le pasamos por par�metro, haciendo el siguiente proceso: recibimos el string, lo encriptamos, queda guardado en la memoria reservada para la encriptaci�n
    //    streamWriter.WriteLine(message);

    //    //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupci�n del archivo o de la propia encriptaci�n
    //    streamWriter.Close();
    //    cryptoStream.Close();
    //    memoryStream.Close();

    //    //Por �ltimo el m�todo devolver� esta informaci�n que reside en el hueco de memoria con la informaci�n encriptada, convertida esta informaci�n en array de bytes
    //    return memoryStream.ToArray();
    //}

    ////Generamos un m�todo que nos devuelva la informaci�n del archivo de guardado desencriptada
    //string Decrypt(byte[] message)
    //{
    //    //Usamos esta librer�a que nos permitir� a trav�s de una referencia crear un desencriptador de la informaci�n
    //    AesManaged aes = new AesManaged();
    //    //Para usar este desencriptador le pasamos tanto la clave como el vector de inicializaci�n que hemos creado nosotros arriba
    //    ICryptoTransform decrypter = aes.CreateDecryptor(_key, _initializationVector);
    //    //Lugar en memoria donde guardamos la informaci�n desencriptada
    //    MemoryStream memoryStream = new MemoryStream(message);
    //    //Con esta referencia podremos escribir en el MemoryStream de arriba la informaci�n ya desencriptada usando el desencriptador con sus claves que ya hab�amos creado
    //    CryptoStream cryptoStream = new CryptoStream(memoryStream, decrypter, CryptoStreamMode.Read);
    //    //Con el StreamReader podemos leer del archivo la informaci�n desencriptada, que se habr� guardado en el MemoryStream
    //    StreamReader streamReader = new StreamReader(cryptoStream);

    //    //Usando todo lo anterior, cargamos del archivo de guardado el json que le pasamos por par�metro, haciendo el siguiente proceso: recibimos el string, lo desencriptamos, queda guardado en la memoria reservada para la desencriptaci�n
    //    string decryptedMessage = streamReader.ReadToEnd();

    //    //Una vez hemos usado estas referencias las cerramos para evitar problemas de guardado o corrupci�n del archivo o de la propia encriptaci�n
    //    streamReader.Close();
    //    cryptoStream.Close();
    //    memoryStream.Close();

    //    //Por �ltimo el m�todo devolver� esta informaci�n que reside en el hueco de memoria con la informaci�n desencriptada, convertida esta en un string
    //    return decryptedMessage;
    //}
}
