using System.Text.Json;
using System.Web;
int main(){
   
   
}
public class Personaje{
    public Caracteristicas CaracteristicaPersonaje{get; private set;};
    public Datos DatosPersonaje{ get;private set; };

    public Personaje(Caracteristicas caracteristicaPersonaje, Datos datosPersonaje;){
        CaracteristicaPersonaje = caracteristicaPersonaje;
        DatosPersonaje = datosPersonaje;
    }
    public void MostrarInformacion()
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

public class FabricaDePersonajes{
    private readonly Random random = new Random();
    private readonly string[] TipoDePersonaje={"G.O.A.T", "★★★★★", "★★★★", "★★★"};
    public 

}