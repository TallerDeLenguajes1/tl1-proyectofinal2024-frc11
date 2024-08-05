//Class de la pelea
public class Pelea{
    private readonly Random random = new Random();

    public Personaje Pelear(Personaje p1, Personaje p2){
        int saludI1 = p1.CaracteristicaPersonaje.Salud;
        int saludI2 = p2.CaracteristicaPersonaje.Salud;
        while (p1.CaracteristicaPersonaje.Salud > 0 && p2.CaracteristicaPersonaje.Salud > 0){
            Atacar(p1, p2);
            if (p2.CaracteristicaPersonaje.Salud <= 0){
                Console.WriteLine($"{p2.DatosPersonaje.Nombre} ha sido derrotado.");
                p1.CaracteristicaPersonaje.Salud = saludI1 + 10; 
                return p1;
            }

            Atacar(p2, p1);
            if (p1.CaracteristicaPersonaje.Salud <= 0){
                Console.WriteLine($"{p1.DatosPersonaje.Nombre} ha sido derrotado.");
                p2.CaracteristicaPersonaje.Salud = saludI2 + 10; 
                return p2;
            }
        }
        return null; //evita fallos
    }

    private void Atacar(Personaje ataca, Personaje defiende){
        int ataque = ataca.CaracteristicaPersonaje.Destreza * ataca.CaracteristicaPersonaje.Fuerza * ataca.CaracteristicaPersonaje.Nivel;
        int efectividad = random.Next(1, 100);
        int defensa = defiende.CaracteristicaPersonaje.Armadura * defiende.CaracteristicaPersonaje.Velocidad;
        const int constAjuste = 500;
        int daño = ((ataque * efectividad) - defensa) / constAjuste;
        defiende.CaracteristicaPersonaje.Salud -= daño;
        if (efectividad >= 50){
            Console.WriteLine("Ataque crítico! \n");
        }
        Console.WriteLine($"{ataca.DatosPersonaje.Nombre} ataca a {defiende.DatosPersonaje.Nombre} y causa {daño} de daño. Salud restante de {defiende.DatosPersonaje.Nombre}: {defiende.CaracteristicaPersonaje.Salud}");
        Console.WriteLine("Presiona cualquier tecla para continuar...");
        Console.ReadKey();
    }
}