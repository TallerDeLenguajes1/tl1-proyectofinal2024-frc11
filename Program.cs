using System.Text.Json;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
class Program{

     private static AsciiArtService _asciiArtService = new AsciiArtService();

    public static async Task Main(string[] args)
    {
        string nombreArchivo = "personajes.json"; 
        PersonajesJson personajesJson = new PersonajesJson();
        List<Personaje> personajes = new List<Personaje>();
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        try
        {
            string mensajeASCII = "Football-War";
            string art = await _asciiArtService.GetAsciiArtAsync(mensajeASCII);
            Console.WriteLine(art);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        if (personajesJson.Existe(nombreArchivo))
        {
            Console.WriteLine("Se han encontrado personajes de archivo, desea crear nuevos o utilizarlos? \n 0-Nuevos \n1-Precargados");
            int opcion = Convert.ToInt32(Console.ReadLine());
            if (opcion == 0)
            {
                Console.WriteLine("Desea crear su personaje de forma manual o asignado de forma aleatoria?\n 0-Manual \n1-Aleatorio");
                int opcion2 = Convert.ToInt32(Console.ReadLine());
                if (opcion2 == 0)
                {
                    personajes.Add(fabrica.CrearPersonajeManual());
                    for (int i = 0; i < 9; i++)
                    {
                        personajes.Add(fabrica.CrearPersonajeAleatorio());
                    }
                }
                else
                {
                    for (int i = 0; i < 10; i++)
                    {
                        personajes.Add(fabrica.CrearPersonajeAleatorio());
                    }
                }
                personajesJson.GuardarPersonajes(personajes, nombreArchivo);
                Console.WriteLine("10 personajes generados y guardados en el archivo.");
            }
            else
            {
                personajes = personajesJson.LeerPersonajes(nombreArchivo);
                Console.WriteLine("Personajes cargados desde el archivo.");
            }
        }
        else
        {
            Console.WriteLine("El archivo de personajes no existe. Se generarán nuevos personajes.\n");
            Console.WriteLine("Desea crear su personaje de forma manual o asignado de forma aleatoria?\n 0-Manual \n1-Aleatorio");
            int opcion2 = Convert.ToInt32(Console.ReadLine());
            if (opcion2 == 0)
            {
                personajes.Add(fabrica.CrearPersonajeManual());
                for (int i = 0; i < 9; i++)
                {
                    personajes.Add(fabrica.CrearPersonajeAleatorio());
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    personajes.Add(fabrica.CrearPersonajeAleatorio());
                }
            }
            personajesJson.GuardarPersonajes(personajes, nombreArchivo);
            Console.WriteLine("10 personajes generados y guardados en el archivo.");
        }
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
   public void MostrarInformacion()
    {
        Console.WriteLine($"Nombre: {DatosPersonaje.Nombre}");
        Console.WriteLine($"Apodo: {DatosPersonaje.Apodo}");
        Console.WriteLine($"Tipo: {DatosPersonaje.Tipo}");
        Console.WriteLine($"Fecha de Nacimiento: {DatosPersonaje.FechaNac.ToShortDateString()}");
        Console.WriteLine($"Edad: {DatosPersonaje.Edad}");
        Console.WriteLine($"Velocidad: {CaracteristicaPersonaje.Velocidad}");
        Console.WriteLine($"Destreza: {CaracteristicaPersonaje.Destreza}");
        Console.WriteLine($"Fuerza: {CaracteristicaPersonaje.Fuerza}");
        Console.WriteLine($"Nivel: {CaracteristicaPersonaje.Nivel}");
        Console.WriteLine($"Armadura: {CaracteristicaPersonaje.Armadura}");
        Console.WriteLine($"Salud: {CaracteristicaPersonaje.Salud}");
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
    string tipo = "";
    while (true)
    {
        Console.WriteLine("Ingrese el tipo de personaje (G.O.A.T, ★★★★★, ★★★★, ★★★):");
        tipo = Console.ReadLine();
        if (tipo == "G.O.A.T" || tipo == "★★★★★" || tipo == "★★★★" || tipo == "★★★")
        {
            break;
        }
        Console.WriteLine("Tipo inválido. Por favor, ingrese uno de los tipos válidos.");
    }

    Console.WriteLine("Ingrese el nombre del personaje:");
    string nombre = Console.ReadLine();

    Console.WriteLine("Ingrese el apodo del personaje:");
    string apodo = Console.ReadLine();

    DateTime fechaNac;
    while (true)
    {
        Console.WriteLine("Ingrese la fecha de nacimiento del personaje (YYYY-MM-DD):");
        if (DateTime.TryParse(Console.ReadLine(), out fechaNac))
        {
            break;
        }
        Console.WriteLine("Fecha inválida. Por favor, ingrese una fecha en el formato correcto.");
    }

   int edad = CalcularEdad(fechaNac);
    
    Datos datos = new Datos(tipo, nombre, apodo, fechaNac, edad);
    Caracteristicas caracteristicas = GenerarCaracteristicasAleatorias(tipo);
    return new Personaje(caracteristicas, datos);

}
private int CalcularEdad(DateTime fechaNac)
{
    DateTime hoy = DateTime.Today;
    int edad = hoy.Year - fechaNac.Year;
    
    if (fechaNac.Date > hoy.AddYears(-edad))
    {
        edad--;
    }
    
    return edad;
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
    private readonly HttpClient _httpClient;

    public AsciiArtService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetAsciiArtAsync(string text)
    {
        string url = $"https://artii.herokuapp.com/make?text={Uri.EscapeDataString(text)}";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al obtener el arte ASCII. Código de estado: {response.StatusCode}, Respuesta: {responseBody}");
        }
    }
}