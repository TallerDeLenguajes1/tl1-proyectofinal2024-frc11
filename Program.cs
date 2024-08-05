class Program
{
    public static async Task Main(string[] args)
    {
        string nombreArchivo = "personajes.json";
        string archivoHistorialGanadores = "historialGanadores.json";
        PersonajesJson personajesJson = new PersonajesJson();
        List<Personaje> personajes = new List<Personaje>();
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        List<Personaje> historialGanadores = personajesJson.LeerHistorialGanadores(archivoHistorialGanadores);
        Console.Clear();
        Console.WriteLine("Bienvenido Football-Battle! \n");
        if (personajesJson.Existe(nombreArchivo))
        {
            string? opcion;
            do
            {
                Console.WriteLine("Se han encontrado personajes de archivo, desea crear nuevos o utilizarlos? (N/U)");
                opcion = Console.ReadLine();
            } while (opcion?.ToUpper() != "U" && opcion?.ToUpper() != "N");
            if (opcion?.ToUpper() == "N")
            {
                string? opcion2;
                do
                {
                    Console.WriteLine("Desea crear su personaje de forma manual o asignado de forma aleatoria? (M/A)");
                    opcion2 = Console.ReadLine();
                } while (opcion2?.ToUpper() != "A" && opcion2?.ToUpper() != "M");
                if (opcion2?.ToUpper() == "M")
                {
                    var personajePropio = fabrica.CrearPersonajeManual();
                    personajePropio.EsPropio = true;
                    personajes.Add(personajePropio);
                    for (int i = 0; i < 9; i++)
                    {
                        personajes.Add(fabrica.CrearPersonajeAleatorio());
                    }
                }
                else
                {
                    var personajePropio = fabrica.CrearPersonajeAleatorio();
                    personajePropio.EsPropio = true;
                    personajes.Add(personajePropio);
                    for (int i = 0; i < 9; i++)
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
            string? opcion2;
            do
            {
                Console.WriteLine("Desea crear su personaje de forma manual o asignado de forma aleatoria? (M/A)");
                opcion2 = Console.ReadLine();
            } while (opcion2?.ToUpper() != "A" && opcion2?.ToUpper() != "M");
            if (opcion2?.ToUpper() == "M")
            {
                var personajePropio = fabrica.CrearPersonajeManual();
                personajePropio.EsPropio = true;
                personajes.Add(personajePropio);
                for (int i = 0; i < 9; i++)
                {
                    personajes.Add(fabrica.CrearPersonajeAleatorio());
                }
            }
            else
            {
                var personajePropio = fabrica.CrearPersonajeAleatorio();
                personajePropio.EsPropio = true;
                personajes.Add(personajePropio);
                for (int i = 0; i < 9; i++)
                {
                    personajes.Add(fabrica.CrearPersonajeAleatorio());
                }
            }
            personajesJson.GuardarPersonajes(personajes, nombreArchivo);
            Console.WriteLine("10 personajes generados y guardados en el archivo.");
        }

        MostrarPersonajes(personajes);
        Pelea pelea = new Pelea();
        while (personajes.Count > 1)
        {
            List<(Personaje, Personaje)> enfrentamientos = CrearEnfrentamientos(personajes);
            Console.WriteLine("Enfrentamientos:");
            foreach (var (atacante, defensor) in enfrentamientos)
            {
                Console.WriteLine($"{atacante.DatosPersonaje.Nombre} vs {defensor.DatosPersonaje.Nombre}");

                if (atacante.EsPropio)
                {
                    string? respuesta;
                    do
                    {
                        Console.WriteLine("Es tu turno. Deseas contar un chiste a tu oponente? (S/N)");
                        respuesta = Console.ReadLine();
                    } while (respuesta?.ToUpper() != "S" && respuesta?.ToUpper() != "N");
                    if (respuesta?.ToUpper() == "S")
                    {
                        string chiste = await APIChistes.GetChiste();
                        Console.WriteLine($"Tu chiste: {chiste}");
                    }
                }
                var ganador = pelea.Pelear(atacante, defensor);
                if (ganador != null)
                {
                    personajes.Remove(atacante == ganador ? defensor : atacante);
                    if ((atacante == ganador ? defensor : atacante).EsPropio)
                    {
                        Console.WriteLine("Tu personaje fue eliminado.");
                    }
                }
            }
        }

        guardarGanador(archivoHistorialGanadores, personajesJson, personajes, historialGanadores);

        MostrarHistorialGanadores(historialGanadores);
    }

    private static void guardarGanador(string archivoHistorialGanadores, PersonajesJson personajesJson, List<Personaje> personajes, List<Personaje> historialGanadores)
    {
        if (personajes.Count == 1)
        {
            var ganadorFinal = personajes[0];
            historialGanadores.Add(ganadorFinal);
            Console.WriteLine($"¡El ganador del Balón de Oro es {ganadorFinal.DatosPersonaje.Nombre}!");
            personajesJson.GuardarHistorialGanadores(historialGanadores, archivoHistorialGanadores);
        }
    }

    static void MostrarPersonajes(List<Personaje> personajes){
        foreach (var personaje in personajes){
            personaje.MostrarInformacion();
            Console.WriteLine("-----------------------------");
        }
    }

    static List<(Personaje, Personaje)> CrearEnfrentamientos(List<Personaje> personajes){
        List<(Personaje, Personaje)> enfrentamientos = new List<(Personaje, Personaje)>();
        for (int i = 0; i < personajes.Count; i += 2){
            if (i + 1 < personajes.Count){
                enfrentamientos.Add((personajes[i], personajes[i + 1]));
            }
        }
        return enfrentamientos;
    }

    static void MostrarHistorialGanadores(List<Personaje> historialGanadores){
        Console.WriteLine("Historial de ganadores:");
        foreach (var ganador in historialGanadores){
            Console.WriteLine($"Nombre:{ganador.DatosPersonaje.Nombre}");
            Console.WriteLine($"Tipo:{ganador.DatosPersonaje.Tipo}");
        }
    }
}