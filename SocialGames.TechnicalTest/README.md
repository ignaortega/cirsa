# Introducción
La solución consta de 3 proyectos.
    - SocialGames.TechnicalTest.ApiService: ASP.NET Core Web Application.
    - SocialGames.TechnicalTest.Api: Class Library (.NET Standard) para gestionar una API REST.
    - SocialGames.TechnicalTest.Games: Class Library (.NET Standard) para la lógica de negocio de los juegos.

# Tareas a realizar
1. Logar en un fichero de texto todas las peticiones a la API donde quede registrado la hora, la petición y el tiempo de repuesta.
Por ejemplo: 2018-02-14 10:31:30.6427 TRACE /api/games/eltesorodejava/play 00:00:00.5586915

2. Logar en otro fichero de texto todos los errores.

3. El método Play del controlador de juegos debe:
    - Validar que el id de juego sea "eltesorodejava".
    - Llamar a un servicio de juegos que retorne una lista de elementos CharIndex con el carácter y el indice del id de juego hasta que encuentre una 'o'. La respuesta debe tener un delay de 500ms.
      Ejemplo de retorno: 
        [
            {
                "Char": "e",
                "Index": 0
            },
            {
                "Char": "l",
                "Index": 1
            },
            {
                "Char": "t",
                "Index": 2
            },
            {
                "Char": "e",
                "Index": 3
            },
            {
                "Char": "s",
                "Index": 4
            }
        ]
    - Devolver la respuesta obtenida.
    - Todo el proceso ha de ser asíncrono.
    - En todos los casos se debe enviar un status code coherente.