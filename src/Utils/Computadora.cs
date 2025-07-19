using Chinchon.src.forms;

namespace Chinchon.src.Utils {
    internal class Computadora {
        private readonly Cartas mazo;
        private readonly Func<string> obtenerTopePilaDescarte;
        private List<string> mano;

        // Constructor
        public Computadora(Cartas mazo, Func<string> obtenerTopePilaDescarte) {
            this.mazo = mazo;
            this.obtenerTopePilaDescarte = obtenerTopePilaDescarte;
            mano = new List<string>();
        }

        // Métodos
        // Recibir mano
        public void RecibirManoInicial(IEnumerable<string> cartas) {
            mano = new List<string>(cartas);
        }

        // Robar del mazo o de la pila de descarte
        private string RobarDelMazo() {
            string carta = mazo.RobarCarta();
            mano.Add(carta);
            return carta;
        }

        private string RobarPilaDescarte() {
            string carta = obtenerTopePilaDescarte();
            mano.Add(carta);
            return carta;
        }

        // Descartar una carta
        private string ElegirCartaDescarte() {
            string mejorCarta = mano[0];
            int mejorPuntos = int.MaxValue;

            // Por cada carta, simula quitarla y calcula los puntos restantes
            foreach (var carta in new List<string>(mano)) {
                var copia = new List<string>(mano);
                copia.Remove(carta);
                int puntos = PartidaHelpers.CalcularPuntos(copia);

                if (puntos < mejorPuntos) {
                    mejorPuntos = puntos;
                    mejorCarta = carta;
                }
            }

            // Realizar el descarte
            mano.Remove(mejorCarta);
            return mejorCarta;
        }

        // Jugar el turno
        // Devulve una tupla:
        // - Con la carta que roba (cartaRobada) ya sea del mazo o de la pila de descarte
        // - Con la carta que deja (cartaDescartada) una carta de su mano
        // - Con una bandera adicional indicando si tras el descarte puede cerrar
        public (string cartaRobada, string cartaDescartada, bool cierra) JugarTurno() {
            // Mirar la carta en la pila de descarte
            string tope = obtenerTopePilaDescarte();
            int puntosActuales = PartidaHelpers.CalcularPuntos(mano);

            // Decidir si robar el mazo o de la pila de descarte
            bool tomarDescarte = true;

            if (!string.IsNullOrEmpty(tope)) {
                // Simular añadir la carta a la mano
                var manoConCarta = new List<string>(mano) { tope };
                int puntosConCarta = PartidaHelpers.CalcularPuntos(manoConCarta);

                // Toma el descarte si los puntos con esa carta son menos
                // Que los puntos actuales
                tomarDescarte = puntosConCarta < puntosActuales;
            }

            string cartaRobada = tomarDescarte ? RobarPilaDescarte() : RobarDelMazo();

            // Elegir la carta a descartar
            string cartaDescartada = ElegirCartaDescarte();

            // Comprobar si puede cerrar
            bool cierra = PartidaHelpers.PuedeCerrar(mano);

            return (cartaRobada, cartaDescartada, cierra);
        }
    }
}