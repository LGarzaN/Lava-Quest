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

##Unity

**BarraProgreso**: Administra la barra de progreso que se muestra en la escena "InGame" que cambia dependiendo de la cantidad de  preguntas.

**CodeError**: Deshabilita las alertas de error de codigo de examen.

**GameManager**: guarda todos los valores usados durante el juego.

**LavaQuestAPIController**: 

**LoginStarter**: Deshabilita las alertas de error de usuario y contraseña.

**ManejadorPreguntas**: 

**ManejadorRespuestas**: 

**MovimientoJugador**: Habilita que el jugador se pueda mover. 

**MusinInGame**: Habilita que la musica no se reinicie cada vez que se cambie de pregunda dentro del cuestrionario.

**MusicPass**: Permite que la musica no se corte mientras que el usuario se encuentra en el menu principal.

**PlayerState**: Guarda y administra el estado del jugador dependiendo de la cantidad de veces que se ha quemado.

**Resultado**: 

**SceneChange**: Permite que al asignarse a un boton, se pueda

**ScoreManager**: 

**Temporizador**: 

**Tiempo**: 

