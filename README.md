# Chinchón

Una aplicación para Windows hecha con WinForms y C# para jugar al chinchón contra una computadora.

*Nota: Esta aplicación es una demo y todavía tiene áreas que mejorar.*

**Necesitas [dotnet](https://dotnet.microsoft.com/en-us/download) para poder usar esta aplicación.**

## ¿Cómo jugar?

- Haz click en el botón que pone "Jugar".

![Main menu image](assets/resources/main_menu.png)

- Reordena las cartas dentro de tu mano, haciendo click izquierdo, arrastra, y suelta.

![Reorder your cards](assets/resources/reorder.gif)

- Roba del mazo, haz click en el mazo y descarta una de tus cartas; haciendo click, arrastrando a la pila de descarte, y suelta.

![Draw a card from the deck](assets/resources/draw_from_deck.gif)

- Roba de la pila de descarte, arrastra desde la pila a tu mano, y descarta una de tus cartas.

![Draw a card from the discard pile](assets/resources/draw_from_pile.gif)

- Y a la victoria:

![Win](assets/resources/win.png)

## ¿Cómo lo descargo?

- En el apartado de "Releases":
	- Descarga el .zip más reciente.
	- Extrae el .zip a una carpeta.
	- Ejecuta el ejecutable.

![Execute the executable](assets/resources/executable.png)

## ¿Cómo lo compilo de forma local?

- Clona el repositorio a una carpeta local:
```bash
  git clone https://github.com/TasteLessTree/Chinchon.git
```

- Entra en dicha carpeta.

- Abre la solución (.sln) con Visual Studio.
 
![Open the solution file](assets/resources/open.gif)

- Comprueba que el proyecto compila:
	- En el explorardor de soluciones.
	- Haz click derecho en: Solución "Chinchon".
	- Compilar solución (ctrl. + shift + B).

![Compile the solution](assets/resources/compile.gif)

- Ejecuta el código.

![Execute the code](assets/resources/execute.gif)

### Otra opción

- Una vez tengas el repositorio clonado, ejecuta este comando donde se encuentre la solución.

```powershell
  dotnet run
```

## Para un futuro

- Mejorar la UI.

- Añadir más sonidos al interactuar con una carta.

- La posibilidad de volver a jugar una vez se ha terminado la partida.

- Añadir un contador de puntuaciones. Ejemplo: perder por exceder el límite de puntos.

- La posibilidad de jugar contra una o más computadoras.

- Implementar el resto de reglas de "Tutorial".

## Autor

[TasteLessTree](https://github.com/TasteLessTree)

## Licencia

[Apache License, Version 2.0](https://www.apache.org/licenses/LICENSE-2.0)
