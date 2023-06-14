# Lava-Quest
 
## Aplicación Web

### Páginas
#### Cada página incluye la parte de Front-End (.cshtml) y Back-End (.cshtml.cs)

**CrearExamen**: Después de nombrar el examen en la página de NomExamen, el usuario es redirigido a esta página, en la cual el usuario puede agregar, eliminar, o modificar las preguntas del examen que se está creando.

**CuentaAlumno**: Después de que el alumno crea su cuenta en la aplicación web, será redirigido a esta página estática, en la cual le dice al alumno que debe ingresar al juego con esta cuenta, y no tendrá acceso a la plataforma, ya que es solo para maestros.

**Index**: La página principal de la aplicación web. De aquí se puede crear un examen nuevo, aplicar un examen existente, modificar la información de la cuenta, y visualizar los exámenes aplicados y los resultados de los alumnos en dichos exámenes.

**IniciarSesion**: Es la primera página que se ve al ingresar a la aplicación web. Si el usuario ya ha creado una cuenta, puede ingresar su correo y su contraseña y se le dará acceso a la plataforma.

**ModificarUsuario**: En esta página el usuario puede modificar el nombre y contraseña que ingresó al crear su cuenta. El usuario no podrá modificar su correo o su tipo de cuenta (Alumno o maestro)

**MostrarCodigo**: Después de seleccionar el examen que se quiere aplicar, se desplegará esta página, en la que se mostrará el código que el maestro le dará a los alumnos para que puedan contestar el examen.

**NomExamen**: Al dar click en el botón de "Crear Examen" que se encuentra en la página principal, el usuario llegará a esta página, en la cual tendrá que poner un nombre al examen nuevo.

**Registro**: Si el usuario aún no tiene cuenta o desea crear una cuenta nueva, se hará en esta página. Pedirá al usuario su nombre completo, correo electrónico, y una contraseña que tendrá que ingresar 2 veces, esto para verificar que el usuario la haya escrito correctamente.

**Resultados**: Después de seleccionar un examen en la página principal, el usuario será redirigido a esta página, en la cual el maestro podrá ver los resultados del examen seleccionado.

**UsarExistente**: Al dar click en el botón de "Usar Existente" en la página principal, el usuario será redirigido a esta página, en la cual podrá seleccionar el examen que quiera aplicar.

## Unity

**BarraProgreso**: Administra la barra de progreso que se muestra en la escena "InGame" que cambia dependiendo de la cantidad de  preguntas.

**CodeError**: Deshabilita las alertas de error de codigo de examen.

**GameManager**: guarda todos los valores usados durante el juego.

**LavaQuestAPIController**: Realiza la conexión con la API REST que, a su vez está conectada con la base de datos. De esta manera se verificaran los datos de ingreso del alumno, se obtendrán las preguntas y respuestas del exámen a responder y se enviarán los resultados del exámen una vez contestado. 

**LoginStarter**: Deshabilita las alertas de error de usuario y contraseña.

**ManejadorPreguntas**: Asigna un índice a cada piedra sobre la que el jugador se puede parar. Luego escoge uno de estos índices de manera aleatoria para marcarla como la piedra correcta y ubica el texto de dicha respuesta sobre esta. Luego escoge tres índices para designarlos como respuestas incorrectas y les coloca encima el texto correspondiente. 

**ManejadorRespuestas**: Verifica constantemente con qué esta colisionando el jugador para reconocer si esta parado sobre una roca correcta o incorrecta, cuando recoge un corazón y cuando toca la lava. 

**MovimientoJugador**: Habilita que el jugador se pueda mover en base a las teclas presionadas W(arriba) A(izquierda) S(abajo) D(derecha). 

**MusinInGame**: Habilita que la musica no se reinicie cada vez que se cambie de pregunda dentro del cuestrionario.

**MusicPass**: Permite que la musica no se corte mientras que el usuario se encuentra en el menu principal.

**PlayerState**: Guarda y administra el estado del jugador dependiendo de la cantidad de veces que se ha quemado.

**Resultado**: Es una clase con la misma estructura de la tabla resultado de la base de datos. Con esta se crea un objeto con el puntaje y datos del alumno y exámen y se envía a la base de datos por medio de la API 

**SceneChange**: Permite que al asignarse a un boton, se pueda cambiar de escena a la escena asignada. 

**ScoreManager**: Al finalizar el juego, permite que se vea como el puntaje se incremente de cero a el puntaje final. 

**Temporizador**: Desactiva y activa los textos de resultado de "Quemado" y "Correcto" Contiene un contador que al finalizarse, limita el movimiento del jugador, desactiva las piedras, suma puntos en caso de que la respuesta sea correcta y agrega muertes en caso de que sea incorrecta.

**Tiempo**: Controlador del temporizador mostrado en la interfaz gráfica para que el alumno pueda visualizar cuanto tiempo tiene hasta que las piedras desaparezcan. 

