using System.Text.Json;
//Persistencia de datos (Lectura y guardado de Json) 
//Primera Parte
public class PersonajesJson{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo){
        string jsonString = JsonSerializer.Serialize(personajes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(nombreArchivo, jsonString);
        Console.WriteLine("Información guardada en " + nombreArchivo);
    }

    public List<Personaje> LeerPersonajes(string nombreArchivo){
        if (File.Exists(nombreArchivo)){
            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Personaje>>(jsonString);
        }
        else{
            Console.WriteLine("Archivo no encontrado.");
            return new List<Personaje>();
        }
    }

    public bool Existe(string nombreArchivo){
        if (File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0){
            return true;
        }
        else{
            return false;
        }
    }
     public void GuardarHistorialGanadores(List<Personaje> historialGanadores, string nombreArchivo){
        string jsonString = JsonSerializer.Serialize(historialGanadores, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(nombreArchivo, jsonString);
        Console.WriteLine("Historial de ganadores guardado en " + nombreArchivo);
    }

    public List<Personaje> LeerHistorialGanadores(string nombreArchivo){
    if (File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0)
    {
        string jsonString = File.ReadAllText(nombreArchivo);
        try{
            return JsonSerializer.Deserialize<List<Personaje>>(jsonString) ?? new List<Personaje>();
        }
        catch (JsonException){
            Console.WriteLine("Error al deserializar el archivo JSON. Asegúrate de que el contenido sea válido.");
            return new List<Personaje>();
        }
    }
    else{
        Console.WriteLine("Archivo no encontrado o vacío.");
        return new List<Personaje>();
    }
}
}

//Persistencia de datos (Lectura y guardado de Json) 
//Segunda Parte
public class Historial{
    public Personaje Ganador { get; set; }
    public string FechaPartida { get; set; }
    public string DetallesPartida { get; set; }

    public Historial(Personaje ganador, string fechaPartida, string detallesPartida){
        Ganador = ganador;
        FechaPartida = fechaPartida;
        DetallesPartida = detallesPartida;
    }
}

public class HistorialJson{
    public void GuardarGanador(Historial historial, string nombreArchivo){
        string jsonString = JsonSerializer.Serialize(historial, new JsonSerializerOptions { WriteIndented = true });
        if (!File.Exists(nombreArchivo)){
            File.WriteAllText(nombreArchivo, "[" + Environment.NewLine + jsonString + Environment.NewLine + "]");
        }
        else{
            string currentContent = File.ReadAllText(nombreArchivo);
            currentContent = currentContent.TrimEnd(']');
            if (currentContent.Length > 1){
                currentContent += "," + Environment.NewLine;
            }
            currentContent += jsonString + Environment.NewLine + "]";
            File.WriteAllText(nombreArchivo, currentContent);
        }
        Console.WriteLine("Información guardada en " + nombreArchivo);
    }

    public List<Historial> LeerGanadores(string nombreArchivo){
        if (File.Exists(nombreArchivo)){
            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Historial>>(jsonString);
        }
        else{
            Console.WriteLine("Archivo no encontrado.");
            return new List<Historial>();
        }
    }

    public bool Existe(string nombreArchivo){
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }
}