using System.Text.Json;
using System.Web;
class Program
{
    static void Main()
    {
        var asciiArtService = new AsciiArtService();  
        /*string asciiArt = await asciiArtService.GetAsciiArtAsync(text);

        if (asciiArt != null)
        {
            Console.WriteLine(asciiArt);
        }*/  
        string asciiArt = await asciiArtService.GetAsciiArtAsync("Football-Battle");
        Console.WriteLine(asciiArt);
        string nombreArchivo = "personajes.json"; 
        PersonajesJson personajesJson = new PersonajesJson();
        List<Personaje> personajes;
        if (personajesJson.Existe(nombreArchivo))
        {
            personajes = personajesJson.LeerPersonajes(nombreArchivo);
            Console.WriteLine("Personajes cargados desde el archivo.");
        }
        else
        {
            personajes = new List<Personaje>();
            Console.WriteLine("El archivo de personajes no existe. Se generarán nuevos personajes.");
        }

        if (personajes.Count == 0)
        {
            FabricaDePersonajes fabrica = new FabricaDePersonajes();
            for (int i = 0; i < 10; i++)
            {  
                personajes.Add(fabrica.CrearPersonajeAleatorio());
            }
            personajesJson.GuardarPersonajes(personajes, nombreArchivo);
            Console.WriteLine("10 personajes generados y guardados en el archivo.");
        }

        // Mostrar los datos y características de los personajes cargados o generados
        MostrarPersonajes(personajes);
    }

    static void MostrarPersonajes(List<Personaje> personajes)
    {
        foreach (var personaje in personajes)
        {
            personaje.MostrarInformacion();
            Console.WriteLine("-----------------------------");
        }
    }
}


public class Personaje{
    public Caracteristicas CaracteristicaPersonaje{get; private set;}
    public Datos DatosPersonaje{ get;private set; }

    public Personaje(Caracteristicas caracteristicaPersonaje, Datos datosPersonaje){
        CaracteristicaPersonaje = caracteristicaPersonaje;
        DatosPersonaje = datosPersonaje;
    }
    public void MostrarInformacion(Datos Datos, Caracteristicas Caracteristicas)
    {
        Console.WriteLine($"Nombre: {Datos.Nombre} \n");
        Console.WriteLine($"Apodo: {Datos.Apodo} \n");
        Console.WriteLine($"Tipo: {Datos.Tipo} \n");
        Console.WriteLine($"Fecha de Nacimiento: {Datos.FechaDeNacimiento.ToShortDateString()} \n");
        Console.WriteLine($"Edad: {Datos.Edad} \n");
        Console.WriteLine($"Velocidad: {Caracteristicas.Velocidad} \n");
        Console.WriteLine($"Destreza: {Caracteristicas.Destreza} \n");
        Console.WriteLine($"Fuerza: {Caracteristicas.Fuerza} \n");
        Console.WriteLine($"Nivel: {Caracteristicas.Nivel} \n");
        Console.WriteLine($"Armadura: {Caracteristicas.Armadura} \n");
        Console.WriteLine($"Salud: {Caracteristicas.Salud} \n");
    }
}
public class Caracteristicas{
    public int Velocidad { get; set; }  // Rango: 1 a 10
    public int Destreza { get; set; }   // Rango: 1 a 5
    public int Fuerza { get; set; }     // Rango: 1 a 10
    public int Nivel { get; set; }      // Rango: 1 a 10
    public int Armadura { get; set; }   // Rango: 1 a 10
    public int Salud { get; set; } = 100; // Salud inicial de 100

    //Constructura 
    public Caracteristicas(int velocidad, int destreza, int fuerza, int nivel, int armadura, int salud){
        Velocidad = velocidad;
        Destreza = destreza;
        Fuerza = fuerza;
        Nivel = nivel;
        Armadura = armadura;
        Salud = salud;
    }
}
public class Datos{
    public string Tipo { get; set; }
    public string Nombre { get; set; }
    public string Apodo { get; set; }
    public DateTime FechaNac{ get; set; }
    public int Edad{ get; set; }

    //Constructoria
    public Datos(string tipo, string nombre, string apodo, DateTime fechaNac, int edad){
        Tipo = tipo;
        Nombre = nombre;
        Apodo = apodo;
        FechaNac = fechaNac;
        Edad = edad;
    }

}

public class FabricaDePersonajes
{
    private readonly Random random = new Random();

    public Personaje CrearPersonajeAleatorio()
    {
        string tipo = GenerarTipoAleatorio();
        Caracteristicas caracteristicas = GenerarCaracteristicasAleatorias(tipo);
        Datos datos = GenerarDatosPersonaje(tipo);
        return new Personaje(caracteristicas, datos);
    }

    public Personaje CrearPersonajeManual()
    {
        Console.WriteLine("Ingrese el tipo de personaje (G.O.A.T, ★★★★★, ★★★★, ★★★):");
        string tipo = Console.ReadLine();
        
        Console.WriteLine("Ingrese el nombre del personaje:");
        string nombre = Console.ReadLine();
        
        Console.WriteLine("Ingrese el apodo del personaje:");
        string apodo = Console.ReadLine();
        
        Console.WriteLine("Ingrese la fecha de nacimiento del personaje (YYYY-MM-DD):");
        DateTime fechaNac = DateTime.Parse(Console.ReadLine());
        
        Console.WriteLine("Ingrese la edad del personaje:");
        int edad = int.Parse(Console.ReadLine());

        Datos datos = new Datos(tipo, nombre, apodo, fechaNac, edad);
        Caracteristicas caracteristicas = GenerarCaracteristicasAleatorias(tipo);
        return new Personaje(caracteristicas, datos);
    }

    private string GenerarTipoAleatorio()
    {
        int probabilidad = random.Next(0, 50);
        if (probabilidad == 0)
        {
            return "G.O.A.T";
        }
        else if (probabilidad < 10)
        {
            return "★★★★★";
        }
        else if (probabilidad < 25)
        {
            return "★★★★";
        }
        else
        {
            return "★★★";
        }
    }

    private Caracteristicas GenerarCaracteristicasAleatorias(string tipo)
    {
        switch (tipo)
        {
            case "G.O.A.T":
                return new Caracteristicas(
                    random.Next(9, 11), // Velocidad
                    random.Next(4, 6),  // Destreza
                    random.Next(9, 11), // Fuerza
                    random.Next(9, 11), // Nivel
                    random.Next(9, 11), // Armadura
                    100);
            case "★★★★★":
                return new Caracteristicas(
                    random.Next(6, 11), // Velocidad
                    random.Next(3, 6),  // Destreza
                    random.Next(6, 11), // Fuerza
                    random.Next(6, 11), // Nivel
                    random.Next(6, 11), // Armadura
                    100);
            case "★★★★":
                return new Caracteristicas(
                    random.Next(3, 11), // Velocidad
                    random.Next(2, 6),  // Destreza
                    random.Next(3, 11), // Fuerza
                    random.Next(3, 11), // Nivel
                    random.Next(3, 11), // Armadura
                    100);
            case "★★★":
                return new Caracteristicas(
                    random.Next(1, 11), // Velocidad
                    random.Next(1, 6),  // Destreza
                    random.Next(1, 11), // Fuerza
                    random.Next(1, 11), // Nivel
                    random.Next(1, 11), // Armadura
                    100);
            default:
                throw new ArgumentException("Tipo de personaje no válido");
        }
    }

    public Datos GenerarDatosPersonaje(string tipo)
    {
        switch (tipo)
        {
            case "G.O.A.T":
                return GenerarDatosGOAT();
            case "★★★★★":
                return GenerarDatos5Estrellas();
            case "★★★★":
                return GenerarDatos4Estrellas();
            case "★★★":
                return GenerarDatos3Estrellas();
            default:
                throw new ArgumentException("Tipo de personaje no válido");
        }
    }

    private Datos GenerarDatosGOAT()
    {
        int probabilidad = random.Next(0, 3);
        switch (probabilidad)
        {
            case 0:
                return new Datos("G.O.A.T", "MESSI", "LA PULGUITA", new DateTime(1987, 6, 24), 36);
            case 1:
                return new Datos("G.O.A.T", "CRISTIANO", "CR7", new DateTime(1985, 2, 5), 39);
            default:
                return new Datos("G.O.A.T", "NEYMAR", "NEY", new DateTime(1992, 2, 5), 32);
        }
    }

    private Datos GenerarDatos5Estrellas()
    {
        int probabilidad = random.Next(0, 4);
        switch (probabilidad)
        {
            case 0:
                return new Datos("★★★★★", "MBAPPE", "KIKI", new DateTime(1998, 12, 20), 25);
            case 1:
                return new Datos("★★★★★", "HAALAND", "THE TERMINATOR", new DateTime(2000, 7, 21), 23);
            case 2:
                return new Datos("★★★★★", "VINICIUS", "VINI", new DateTime(2000, 7, 12), 24);
            default:
                return new Datos("★★★★★", "BELLINGHAM", "BELLIGOAL", new DateTime(2003, 6, 29), 21);
        }
    }

    private Datos GenerarDatos4Estrellas()
    {
        string[] nombres4Estrellas = { "LEWANDOWSKI", "KANE", "DE BRUYNE", "SALAH" };
        string[] apodos4Estrellas = { "LEWY", "HURRIKANE", "KDB", "MO" };
        int indice = random.Next(0, nombres4Estrellas.Length);
        return new Datos("★★★★", nombres4Estrellas[indice], apodos4Estrellas[indice], DateTime.Now.AddYears(-random.Next(18, 35)), random.Next(18, 35));
    }

    private Datos GenerarDatos3Estrellas()
    {
        string[] nombres3Estrellas = { "LUKAKU", "GRIEZMANN", "CARRASCAL", "COURTOIS" };
        string[] apodos3Estrellas = { "TRONKAKU", "GRIZI", "CARRASCA", "THIBAUT" };
        int indice = random.Next(0, nombres3Estrellas.Length);
        return new Datos("★★★", nombres3Estrellas[indice], apodos3Estrellas[indice], DateTime.Now.AddYears(-random.Next(18, 35)), random.Next(18, 35));
    }
}

//Persistencia de datos (Lectura y guardado de Json) 
//Primera Parte
public class PersonajesJson
{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)
    {
        string jsonString = JsonSerializer.Serialize(personajes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(nombreArchivo, jsonString);
        Console.WriteLine("Información guardada en " + nombreArchivo);
    }

    public List<Personaje> LeerPersonajes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Personaje>>(jsonString);
        }
        else
        {
            Console.WriteLine("Archivo no encontrado.");
            return new List<Personaje>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

//Persistencia de datos (Lectura y guardado de Json) 
//Segunda Parte
public class Historial
{
    public Personaje Ganador { get; set; }
    public string FechaPartida { get; set; }
    public string DetallesPartida { get; set; }

    public Historial(Personaje ganador, string fechaPartida, string detallesPartida)
    {
        Ganador = ganador;
        FechaPartida = fechaPartida;
        DetallesPartida = detallesPartida;
    }
}

public class HistorialJson
{
    public void GuardarGanador(Historial historial, string nombreArchivo)
    {
        string jsonString = JsonSerializer.Serialize(historial, new JsonSerializerOptions { WriteIndented = true });
        if (!File.Exists(nombreArchivo))
        {
            File.WriteAllText(nombreArchivo, "[" + Environment.NewLine + jsonString + Environment.NewLine + "]");
        }
        else
        {
            string currentContent = File.ReadAllText(nombreArchivo);
            currentContent = currentContent.TrimEnd(']');
            if (currentContent.Length > 1)
            {
                currentContent += "," + Environment.NewLine;
            }
            currentContent += jsonString + Environment.NewLine + "]";
            File.WriteAllText(nombreArchivo, currentContent);
        }
        Console.WriteLine("Información guardada en " + nombreArchivo);
    }

    public List<Historial> LeerGanadores(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            string jsonString = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Historial>>(jsonString);
        }
        else
        {
            Console.WriteLine("Archivo no encontrado.");
            return new List<Historial>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
    }
}

//API de ascii art
public class AsciiArtService
{
    private readonly string apiUrl = "http://artii.herokuapp.com/make?text=";

    public async Task<string> GetAsciiArtAsync(string text)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{apiUrl}{Uri.EscapeDataString(text)}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo arte ASCII: {ex.Message}");
                return null;
            }
        }
    }

    public async Task SaveAsciiArtToFileAsync(string text, string asciiArt, string filePath)
    {
        var apiResponse = new ApiResponse
        {
            Name = text,
            AsciiArt = asciiArt
        };

        string jsonString = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions { WriteIndented = true });

        if (!File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, "[" + Environment.NewLine + jsonString + Environment.NewLine + "]");
        }
        else
        {
            string currentContent = await File.ReadAllTextAsync(filePath);
            currentContent = currentContent.TrimEnd(']');
            if (currentContent.Length > 1)
            {
                currentContent += "," + Environment.NewLine;
            }
            currentContent += jsonString + Environment.NewLine + "]";
            await File.WriteAllTextAsync(filePath, currentContent);
        }
    }

    public class ApiResponse
    {
        public string Name { get; set; }
        public string AsciiArt { get; set; }
    }
}