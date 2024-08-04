##PROYECTO FINAL - TALLER DE LENGUAJES - FRANCO PIZZI##
Tematica del juego:
El juego de consola que cree, Battle-Football, es un juego de batallas de futbol, en la cual cada personaje posee un determinado tipo (G.O.A.T-★★★★★-★★★★-★★★), el cual determina el minimo de las estadisticas con la que estos podran empezar. A su vez al iniciar el juego, se pregunta si se quiere que se utilicen los personajes ya guardados en personajes.json (si es que existen) o que se generen de forma aleatoria (el personaje propio puede ser generado de forma manual o aleatoria), y son posteriormente guardados en el archivo "personajes.Json". En el juego se nos va mostrando cada enfrentamiento, el daño que causa el ataante y la vida del defensor. Si el personaje que esta atacando es el nuestro, nos saldra una opcion para soltar un chiste, en el que se ejecuta la api de chistes implementada. Luego de cada enfrentamiento ganado, se le otorgan 10 puntos de salud extra al ganador. 
El personaje que gana la final, gana el tan preciado Balon De Oro, y es sumado a "historialGanadores.json" los cuales se muestran al final.
Api:
Se uso una api de bromas, la cual es generada con el siguiente link "https://v2.jokeapi.dev/joke/Any?lang=es". La cual si se ejecuta correctamente nos entregara una simple broma en español, que le agrega un poco de gracia al combate, sino entrega que no esta disponible.
Archivos del programa:
api.cs: Aqui se encuentran las clases y el metodo para usar la api de bromas.
peleas.cs: Aqui se encuentra el codigo para correr las peleas entre los jugadores de futbol(ataque,defensa,etc)
personajesJson.cs: Aqui se encuentra todas las persistencias de datos, ya sea el guardado y lectura de personajes, ganadores, etc
personajes.cs: Aqui se encuentra todo el codigo referido a la creacion de datos de los personajes(Fabrica)
Uso del juego: El uso del programa es bastante basico e intuituvo, se basa en:
1. Se ejecuta el programa.
2. Se selecciona si se quiere utilizar personajes guardados o generar de forma aleatoria.
3. Se selecciona si se quiere generar el personaje propio de forma manual o aleatoria.
4. Se selecciona si se quiere utilizar la api de bromas o no.
A disfrutar!
