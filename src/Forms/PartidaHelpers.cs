namespace Chinchon.src.forms {
    internal static class PartidaHelpers {

        // Dada una mano, comprueba si la carta sin casar es <= 5, o si están todas las cartas casadas
        public static bool PuedeCerrar(List<string> mano) {
            if (EsChinchon(mano)) return true;

            if (EsMenosDiez(mano)) return true;

            // No nos iteresa el valorSuelto así que usamos el descartador (_)
            if (EsCartaSueltaMenorCinco(mano, out _)) return true;

            return false;
        }

        // Calcular la puntuación de la mano del jugador
        public static int CalcularPuntos(List<string> mano) {
            // Chinchón son 0 puntos
            if (EsChinchon(mano)) return 0;

            // Menos diez son -10 puntos
            if (EsMenosDiez(mano)) return -10;


            // Si no es ninguna, se usa el valor de la carta sin casar
            EsCartaSueltaMenorCinco(mano, out int valorSuelto);
            int puntuacion = valorSuelto;

            return puntuacion;
        }

        // Comprobar si la carta que no se ha casado es <= 5
        // "EsLaCartaSinCasarMenorOIgualACinco" era demasiado largo y lo he cambiado a "EsCartaSueltaMenorCinco"
        private static bool EsCartaSueltaMenorCinco(List<string> mano, out int valorSuelto) {
            valorSuelto = 0; // Por defecto, usamos "out" para calcular el puntaje

            var grupos = ObtenerGrupos(mano);

            // Comprobar seis cartas casadas y una suelta <= 5
            if (grupos.Sum(g => g.Count) == 6 && mano.Count == 7) {
                var indicesAgrupados = grupos.SelectMany(g => g).ToList();
                var cartaSuelta = mano.Where((c, i) => !indicesAgrupados.Contains(i)).FirstOrDefault();

                if (cartaSuelta != null) {
                    valorSuelto = int.Parse(cartaSuelta.Split(' ')[0]);

                    if (valorSuelto <= 5) return true;
                }
            }

            // DEBUG:
            // Console.WriteLine($"Carta sin casar(valor suleto): {valorSuelto}");

            return false;
        }

        // Comprobar si es chinchón
        private static bool EsChinchon(List<string> mano) {
            // Agrupa cartas por número y por palo
            var numeros = mano.Select(c => int.Parse(c.Split(' ')[0])).ToList();
            var palos = mano.Select(c => c.Split(' ')[1]).Distinct();

            // Comprobar si es escalera de 7 cartas del mismo palo (chinchón)
            foreach (var palo in palos) {
                var numerosDePalo = mano
                    .Where(c => c.EndsWith(palo))
                    .Select(c => int.Parse(c.Split(' ')[0]))
                    .OrderBy(n => n)
                    .ToList();

                // ¡Chinchón!
                if (numerosDePalo.Count == 7 && EsEscalera(numerosDePalo)) return true;
            }

            return false;
        }

        // Comprobar si es una escalera de números consecutivos
        private static bool EsEscalera(List<int> numeros) {
            numeros.Sort();

            for (int i = 1; i < numeros.Count; i++) {
                if (numeros[i] != numeros[i - 1] + 1) return false;
            }
            return true;
        }

        // Comprobar si es menos diez (-10)
        private static bool EsMenosDiez(List<string> mano) {
            // Comprobar si es menos 10 (4 cartas casadas por un lado, 3 cartas casadas por el otro)
            var grupos = ObtenerGrupos(mano);

            if (grupos.Any(g => g.Count == 4) &&
                grupos.Any(g => g.Count == 3) &&
                grupos.Sum(g => g.Count) == 7) return true;

            return false;
        }

        // Devuelve una lista de grupos de índices de cartas agrupadas (por número o escalera)
        private static List<List<int>> ObtenerGrupos(List<string> mano) {
            var grupos = new List<List<int>>();

            // Buscar tríos y cuartetos
            var numeros = mano.Select(c => int.Parse(c.Split(' ')[0])).ToList();

            for (int n = 1; n <= 12; n++) {
                var indices = numeros
                    .Select((num, idx) => num == n ? idx : -1)
                    .Where(idx => idx != -1)
                    .ToList();
                if (indices.Count >= 3)
                    grupos.Add(indices);
            }

            // Buscar escaleras por palo
            var palos = mano.Select(c => c.Split(' ')[1]).Distinct();

            foreach (var palo in palos) {
                var cartasDePalo = mano
                    .Select((c, idx) => new { c, idx })
                    .Where(x => x.c.EndsWith(palo))
                    .Select(x => new { Num = int.Parse(x.c.Split(' ')[0]), x.idx })
                    .OrderBy(x => x.Num)
                    .ToList();

                for (int i = 0; i <= cartasDePalo.Count - 3; i++) {
                    var escalera = new List<int> { cartasDePalo[i].idx };
                    int actual = cartasDePalo[i].Num;

                    for (int j = i + 1; j < cartasDePalo.Count; j++) {

                        if (cartasDePalo[j].Num == actual + 1) {
                            escalera.Add(cartasDePalo[j].idx);
                            actual++;
                        } else if (cartasDePalo[j].Num > actual + 1) break;
                    }
                    if (escalera.Count >= 3) grupos.Add(escalera);
                }
            }

            return grupos;
        }
    }
}