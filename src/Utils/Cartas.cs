using System.Media;

namespace Chinchon.src.Utils {
    internal class Cartas {
        // Campos
        private readonly List<string> mazo;

        // Constructor, crea las cartas
        public Cartas() { 
            // Palos (BASTOS, OROS, ESPADAS, COPAS)
            var palos = new List<string> { " BASTOS", " OROS", " ESPADAS", " COPAS" };

            // Los números [1, 12]
            var numeros = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

            // Crear la baraja
            var baraja = new List<string>();

            // Añadir las cartas
            baraja.AddRange(
                from palo in palos
                from num in numeros
                select num + palo
            );

            // El mazo es la baraja
            this.mazo = baraja;
        }

        // Barajear las cartas con el algoritmo de Fisher-Yates 
        public void Barajear() {
            // Reproducir un sonido de barajeo: https://pixabay.com/sound-effects/riffle-card-shuffle-104313/
            // Para dar más inmersión
            string rutaSonido = Path.Combine(Application.StartupPath, "assets", "audio", "barajeo.wav");

            SoundPlayer soundPlayer = new(rutaSonido);
            soundPlayer.Play();

            var random = new Random();

            for (int i = 0; i < mazo.Count; i++) {
                int j = random.Next(i + 1);
                (mazo[i], mazo[j]) = (mazo[j], mazo[i]);
            }
        }

        // Repartir X cartas entre Y jugadores
        public List<List<string>> RepartirCartas(int cartas, int jugadores) {
            // Comprobar que hay suficientes cartas
            if (cartas * jugadores > mazo.Count)
                throw new InvalidOperationException("No hay suficentes cartas para repartir.");

            // Inicializar la lista con todas las manos
            var manos = Enumerable.Range(0, jugadores)
                .Select(_ => new List<string>())
                .ToList();

            // Repartimos las cartas
            for (int i = 0; i < cartas; i++) {
                for (int j = 0; j < jugadores; j++) {
                    manos[j].Add(mazo[0]);
                    mazo.RemoveAt(0);
                }
            }

            return manos;
        }

        // Crear la pila de descarte
        public string CrearPilaDescarte() {
            string carta = mazo[0];
            mazo.RemoveAt(0);
            return carta;
        }

        // Robamos una carta
        public string RobarCarta() {
            // Asegura que existan cartas en el mazo
            if (mazo.Count == 0)
                throw new InvalidOperationException("No quedan cartas en el mazo");

            string carta = mazo[0];
            mazo.RemoveAt(0);
            return carta;
        }
    }
}