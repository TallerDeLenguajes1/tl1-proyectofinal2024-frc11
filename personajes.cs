public class Personaje{
    public Caracteristicas CaracteristicaPersonaje{get; private set;}
    public Datos DatosPersonaje{ get;private set; }
    public bool EsPropio { get; set; } 

    public Personaje(Caracteristicas caracteristicaPersonaje, Datos datosPersonaje){
        CaracteristicaPersonaje = caracteristicaPersonaje;
        DatosPersonaje = datosPersonaje;
    }
   public void MostrarInformacion(){
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

public class FabricaDePersonajes{
    private readonly Random random = new Random();

    public Personaje CrearPersonajeAleatorio(){
        string tipo = GenerarTipoAleatorio();
        Caracteristicas caracteristicas = GenerarCaracteristicasAleatorias(tipo);
        Datos datos = GenerarDatosPersonaje(tipo);
        return new Personaje(caracteristicas, datos);
    }

 public Personaje CrearPersonajeManual(){
    string ? tipo = "";
    while (true){
        Console.WriteLine("Ingrese el tipo de personaje (G.O.A.T, ★★★★★, ★★★★, ★★★):");
        tipo = Console.ReadLine();
        if (tipo == "G.O.A.T" || tipo == "★★★★★" || tipo == "★★★★" || tipo == "★★★"){
            break;
        }
        Console.WriteLine("Tipo inválido. Por favor, ingrese uno de los tipos válidos.");
    }

    Console.WriteLine("Ingrese el nombre del personaje:");
    string ? nombre = Console.ReadLine();

    Console.WriteLine("Ingrese el apodo del personaje:");
    string ? apodo = Console.ReadLine();

    DateTime fechaNac;
    while (true){
        Console.WriteLine("Ingrese la fecha de nacimiento del personaje (AAAA-MM-DD):");
        if (DateTime.TryParse(Console.ReadLine(), out fechaNac)){
            break;
        }
        Console.WriteLine("Fecha inválida. Por favor, ingrese una fecha en el formato correcto.");
    }

   int edad = CalcularEdad(fechaNac);
    
    Datos datos = new Datos(tipo, nombre, apodo, fechaNac, edad);
    Caracteristicas caracteristicas = GenerarCaracteristicasAleatorias(tipo);
    return new Personaje(caracteristicas, datos);

}
private int CalcularEdad(DateTime fechaNac){
    DateTime hoy = DateTime.Today;
    int edad = hoy.Year - fechaNac.Year;
    
    if (fechaNac.Date > hoy.AddYears(-edad)){
        edad--;
    }
    
    return edad;
}
    private string GenerarTipoAleatorio(){
        int probabilidad = random.Next(0, 50);
        if (probabilidad == 0){
            return "G.O.A.T";
        }
        else if (probabilidad < 10){
            return "★★★★★";
        }
        else if (probabilidad < 25){
            return "★★★★";
        }
        else{
            return "★★★";
        }
    }

    private Caracteristicas GenerarCaracteristicasAleatorias(string tipo){
        switch (tipo){
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

    public Datos GenerarDatosPersonaje(string tipo){
        switch (tipo){
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

    private Datos GenerarDatosGOAT(){
        int probabilidad = random.Next(0, 3);
        switch (probabilidad){
            case 0:
                return new Datos("G.O.A.T", "MESSI", "LA PULGUITA", new DateTime(1987, 6, 24), 36);
            case 1:
                return new Datos("G.O.A.T", "CRISTIANO", "CR7", new DateTime(1985, 2, 5), 39);
            default:
                return new Datos("G.O.A.T", "NEYMAR", "NEY", new DateTime(1992, 2, 5), 32);
        }
    }

    private Datos GenerarDatos5Estrellas(){
    int probabilidad = random.Next(0, 4);
    switch (probabilidad){
        case 0:
            return new Datos("★★★★★", "MBAPPE", "KIKI", new DateTime(1998, 12, 20), 25);
        case 1:
            return new Datos("★★★★★", "HAALAND", "ANDROIDE", new DateTime(2000, 7, 21), 23);
        case 2:
            return new Datos("★★★★★", "VINICIUS", "VINI", new DateTime(2000, 7, 12), 24);
        default:
            return new Datos("★★★★★", "BELLINGHAM", "BELLIGOAL", new DateTime(2003, 6, 29), 21);
    }
}

private Datos GenerarDatos4Estrellas(){
    int probabilidad = random.Next(0, 4);
    switch (probabilidad){
        case 0:
            return new Datos("★★★★", "LEWANDOWSKI", "LEWY", new DateTime(1988, 8, 21), 35);
        case 1:
            return new Datos("★★★★", "KANE", "0 TITLES", new DateTime(1993, 7, 28), 31);
        case 2:
            return new Datos("★★★★", "DE BRUYNE", "KDB", new DateTime(1991, 6, 28), 33);
        default:
            return new Datos("★★★★", "SALAH", "MO", new DateTime(1992, 6, 15), 32);
    }
}

private Datos GenerarDatos3Estrellas(){
    int probabilidad = random.Next(0, 4);
    switch (probabilidad){
        case 0:
            return new Datos("★★★", "LUKAKU", "TRONKAKU", new DateTime(1993, 5, 13), 31);
        case 1:
            return new Datos("★★★", "GRIEZMANN", "GRIZI", new DateTime(1991, 3, 21), 33);
        case 2:
            return new Datos("★★★", "CARRASCAL", "CARRASCA", new DateTime(1994, 2, 20), 30);
        default:
            return new Datos("★★★", "COURTOIS", "THIBAUT", new DateTime(1992, 5, 11), 32);
    }
}}
