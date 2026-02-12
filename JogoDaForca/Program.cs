using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaForca
{
    internal class Program
    {
        static string[] palavras = {
            "janela", "caderno", "montanha", "estrada", "oceano",
            "floresta", "cidade", "amizade", "coragem", "esperanca",
            "livro", "caneta", "trabalho", "familia", "tempo",
            "chuva", "vento", "sol", "lua", "estrela",
            "ponte", "jardim", "rio", "praia", "cachoeira",
            "sorriso", "viagem", "musica", "historia", "energia"
        };

        static string[] opcoes = { "sair", "jogar" };

        static void Main(string[] args)
        {
            bool continuarJogando = true;

            while (continuarJogando)
            {
                MostrarMenu();
                int escolhaUsuario = LerEscolhaUsuario();
                if (escolhaUsuario == 0) { continuarJogando = false; }
                else if (escolhaUsuario == 1) { ComecarJogo(); }
            }
        }

        static void ComecarJogo()
        {
            List<char> letrasTentadas = new List<char>();
            string palavraSecreta = SortearPalavra();
            bool palavraEncontrada = false;
            char[] progresso = new char[palavraSecreta.Length];
            int letrasEncontradas = 0;

            while (!palavraEncontrada)
            {
                MostrarChutes(letrasTentadas);
                MostrarProgresso(progresso);
                if (letrasEncontradas == palavraSecreta.Length) { palavraEncontrada = true; break; }

                char chute = LerEntradaChar();
                if (!letrasTentadas.Contains(chute)) 
                {
                    letrasTentadas.Add(chute);
                    for (int i = 0; i < palavraSecreta.Length; i++)
                    {
                        if (palavraSecreta[i] == char.ToLower(chute)) 
                        { 
                            progresso[i] = chute;
                            letrasEncontradas += 1;
                        };
                    };
                    
                };
                
            }
            MostrarSeparacao();
            Console.WriteLine($"Parabens, você descobriu a palavra {palavraSecreta} em  {letrasTentadas.Count} chutes.");
            MostrarSeparacao();
        }

        static void MostrarProgresso(char[] progresso)
        {
            Console.Write("\n" +
                "[");
            foreach (char letra in progresso)
            {
                if (letra == '\0') { Console.Write(" _ "); }
                else { Console.Write($" {letra} "); }
            }
            Console.Write("]\n");
            MostrarSeparacao();
        }

        static void MostrarChutes(List<char> chutes)
        {
            Console.Write("\n" +
                "[");
            foreach (char letra in chutes)
            {
                if (letra == '\0') { Console.Write(" _ "); }
                else { Console.Write($" {letra} "); }
            }
            Console.Write("]\n");
            MostrarSeparacao();
        }

        static string SortearPalavra()
        {
            Random random = new Random();
            int numeroSorteado = random.Next(0, palavras.Length);
            return palavras[numeroSorteado];
        }

        static void MostrarSeparacao()
        {
            Console.WriteLine("------------------------------");
        }

        static void MostrarMenu()
        {
            MostrarSeparacao();
            for (int i = 0; i < opcoes.Length; i++)
            {
                Console.WriteLine($"[ {i} ] - {opcoes[i]}");
            }
            MostrarSeparacao();
        }

        static int LerEscolhaUsuario()
        {
            bool escolhaValida = false;
            while (!escolhaValida)
            {
                int escolha = LerEntradaInt("escolha");
                if (escolha < 0 &&  escolha > opcoes.Length) { Console.WriteLine("Escolha Inválida."); }
                else { return escolha; }
            }
            return -1;
        }

        static int LerEntradaInt(string message)
        {
            bool entradaValida = false;
            while (!entradaValida)
            {
                Console.WriteLine($"Insira um valor para {message}:");
                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    return escolha;
                }

            }
            return -1;
        }

        static char LerEntradaChar()
        {
            bool entradaValida = false;
            while (!entradaValida)
            {
                Console.WriteLine("Insira uma letra para chutar: ");
                if (char.TryParse(Console.ReadLine(), out char chute))
                {
                    return char.ToUpper(chute);
                }
                else { Console.WriteLine("Entrada inválida."); }
            }
            return ';';

        }
    }
}
