using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;//Librería añadida para trabajar con la DB
using System.IO;//Librería para poder abrir archivos
using Mono.Data.Sqlite;//Librería para trabajar con SQLite
using UnityEngine.UI; //Para usar el Canvas de Unity
using System;
//using static UnityEditor.Progress;
using static UnityEngine.Rendering.DebugUI;

public class SQLITE_conxx : MonoBehaviour
{
    //Variable donde guardar la dirección de la Base de Datos
    string rutaDB;
    string strConexion;
    //Nombre de la base de datos con la que vamos a trabajar
    string DBFileName = "RankingBaseDatos.db";
    //Referencia que necesitamos para poder crear una conexión 
    IDbConnection dbConnection;
    //Referencia que necesitamos para poder ejecutar comandos
    IDbCommand dbCommand;
    //Referencia que necesitamos para leer datos
    IDataReader reader;
    // Start is called before the first frame update
    void Start()
    {
        ////Llamamos al método que abre las conexiones
        //AbrirDB("RankingBaseDatos.db", "");
        //CerrarDB();
        //UPDATE();
    }

    void AbrirDB(string nombreBD)
    {
        // Crear y abrir la conexión
        //Compuebo en que plataforma estamos
        //Si estamos en PC mantenemos la ruta
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            rutaDB = Application.dataPath + "/StreamingAssets/" + nombreBD;
        }
        //Si estamos en Android
        else if (Application.platform == RuntimePlatform.Android)
        {
            rutaDB = Application.persistentDataPath + "/" + nombreBD;
            //Comprobar si el archivo se encuentra almacenado en persistant data
            if (!File.Exists(rutaDB))
            {
                //Almaceno el archivo en load db
                //Copio el archivo a persistant data
                WWW loadDB = new WWW("jar;file://" + Application.dataPath + nombreBD);
                while (!loadDB.isDone)
                {

                }
                File.WriteAllBytes(rutaDB, loadDB.bytes);
            }
        }

        strConexion = "URI=file:" + rutaDB;
        dbConnection = new SqliteConnection(strConexion);
        dbConnection.Open();
    }

    //void AbrirDB(string nombreBD, string tabla)
    //{
    //    if (Application.platform == RuntimePlatform.WindowsEditor)
    //    {
    //        //Ruta dentro del pc para buscar la base de datos
    //        rutaDB = Application.dataPath + "/StreamingAssets/" + nombreBD;
    //    }
    //    //Si estamos en Android
    //    else if (Application.platform == RuntimePlatform.Android)
    //    {
    //        //Ruta dentro del dispositivo Android para buscar la base de datos
    //        rutaDB = Application.persistentDataPath + "/" + nombreBD;
    //        //Comprobar si el archivo se encuentra almacenado en persistant data
    //        if (!File.Exists(rutaDB))
    //        {
    //            //Almaceno el archivo en load db
    //            WWW loadDB = new WWW("jar;file://" + Application.dataPath + nombreBD);
    //            while (!loadDB.isDone)
    //            {

    //            }
    //            //Copio el archivo a persistant data
    //            File.WriteAllBytes(rutaDB, loadDB.bytes);
    //        }
    //    }
    //    //Para leer esta misma base de datos desde un dispositivo móvil
    //    strConexion = "URI=file:" + rutaDB;
    //    //Creamos una nueva conexión usando esa ruta
    //    dbConnection = new SqliteConnection(strConexion);
    //    //Abrimos esa conexión
    //    dbConnection.Open();

    //    //Crear la consulta
    //    //Generamos un comando para la conexión que hemos abierto
    //    dbCommand = dbConnection.CreateCommand();
    //    //El query
    //    string sqlQuery = "SELECT * FROM " + tabla;
    //    //Le pasamos el query al comando que vamos a ejecutar
    //    dbCommand.CommandText = sqlQuery;
    //    //Leer la base de datos
    //    //Ejecutamos el comando que hemos creado en formato lectura de datos
    //    reader = dbCommand.ExecuteReader();
    //    //Haremos que lea datos hasta que no queden más por leer de ese query
    //    while (reader.Read())
    //    {
    //        int pos = reader.GetInt32(0);
    //        string nombre = reader.GetString(1);
    //        string tiempo = reader.GetString(2);
    //        Debug.Log (pos + " - " + nombre + " - " + tiempo + " - ");
    //    }
    //    //Cerrar las conexiones
    //    //Cerramos el lector de datos
    //    reader.Close();
    //    //Vaciamos por si acaso el lector de datos
    //    reader = null;
    //    //Dejamos de disponer del comando que habíamos creado
    //    dbCommand.Dispose();
    //    //Vaciamos por si acaso el comando que habíamos creado
    //    dbCommand = null;
    //    //Cerramos la conexión que hemos abierto
    //    dbConnection.Close();
    //    //Vaciamos por si acaso la conexión que hemos usado
    //    dbConnection = null;
    //}

    void ComandoSelect(string item, string tabla)
    {
        // Crear la consulta
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = "SELECT " + item + " FROM " + tabla;
        ////string sqlQuery = "SELECT * FROM RankingNascar";
        dbCommand.CommandText = sqlQuery;

        // Leer la base de datos
        reader = dbCommand.ExecuteReader();
        while (reader.Read())
        {
            try
            {
                Debug.Log(reader.GetInt32(0) + " - " + reader.GetString(1) + " - " + reader.GetString(2));
            }
            catch (FormatException fe)
            {
                Debug.Log(fe.Message);
                continue;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
    }

    //Cerrar la conexión
    void CerrarDB()
    {
        // Cerrar las conexiones
        reader.Close();
        reader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
    void INSERT(string tabla, float dato3)
    {
        // Crear la consulta
        dbCommand = dbConnection.CreateCommand();
        string sqlQuery = String.Format("INSERT INTO \"{0}\"(Tiempo) values(\"{1}\")", tabla, dato3); //String.Format podemos pasarle datos a una cadena de caracteres para que tome el argumento
        //string sqlQuery = String.Format("INSERT INTO RankingNascar(Posicion, Nombre, Tiempo) values(\"{0}\",\"{1}\", \"{2}\")", dato1, dato2, dato3); //String.Format podemos pasarle datos a una cadena de caracteres para que tome el argumento
        //string sqlQuery = "INSERT INTO " + tabla + "(Posicion, Nombre, Tiempo) values(" + dato1 + "," + dato2 + "," + dato3 + ")";
        //string sqlQuery = "INSERT INTO RankingNascar(Posicion, Nombre, Tiempo) values(" + dato1 + "," + dato2 + "," + dato3 + ")";
        dbCommand.CommandText = sqlQuery;
        //Para poder contar datos de las filas tenemos que poner
        dbCommand.ExecuteScalar();
        //ExecuteScalar me devuelve un objeto 
        //Cerramos la DB
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }
    //void UPDATE()
    //{
    //    // Crear la consulta
    //    dbCommand = dbConnection.CreateCommand();
    //    string sqlQuery = "UPDATE media_types SET Name = \"Luis\" WHERE MediaTypeId = 7";
    //    dbCommand.CommandText = sqlQuery;
    //    //Para poder contar datos de las filas tenemos que poner
    //    dbCommand.ExecuteScalar();
    //    //ExecuteScalar me devuelve un objeto 
    //    //Cerramos la DB
    //    dbCommand.Dispose();
    //    dbCommand = null;
    //    dbConnection.Close();
    //    dbConnection = null;
    //}

    //public void BotonNascar(string tabla)
    //{
    //    AbrirDB("RankingBaseDatos.db");
    //    ComandoSelect("*", tabla);
    //    CerrarDB();
    //}
    //public void BotonBreifford(string tabla)
    //{
    //    AbrirDB("RankingBaseDatos.db");
    //    ComandoSelect("*", tabla);
    //    CerrarDB();
    //}
    //public void BotonSenach(string tabla)
    //{
    //    AbrirDB("RankingBaseDatos.db");
    //    ComandoSelect("*", tabla);
    //    CerrarDB();
    //}

    public void Inserta(string tabla, float dato3)
    {
        AbrirDB("RankingBaseDatos.db");
        //AbrirDB("RankingBaseDatos.db","RankingNascar");
        INSERT(tabla, dato3);
        //CerrarDB();
    }

    public void Selecccionar(string tabla)
    {
        AbrirDB("RankingBaseDatos.db");
        ComandoSelect("*", tabla);
        CerrarDB();
    }
}


