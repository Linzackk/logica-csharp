using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class Program
    {
        static string[] opcoes = new string[4] { "Sair", "Entrar na Conta", "Criar Conta", "Listar Contas" };
        static string[] opcoesConta = new string[5] { "Sair", "Depositar", "Sacar", "Transferir", "Saldo" };
        static List<Conta> contas = new List<Conta>();
        public static void Main(string[] args)
        {
            bool usandoSistema = true;
            bool logado = false;
            Conta contaLogada = null;
            while (usandoSistema)
            {
                if (!logado)
                {
                    MostrarMenu(opcoes);
                    int escolha = LerEscolhaUsuario();
                    if (escolha == 0) { usandoSistema = false; }
                    else if (escolha == 1)
                    {
                        int numeroConta = LerEntradaInt("numero da conta");
                        int indexConta = LogarConta(numeroConta);
                        if (indexConta != -1) { contaLogada = contas[indexConta]; logado = true; }
                    }
                    else if (escolha == 2) { AbrirConta(); }
                    else if (escolha == 3) { ListarContas(); }
                }
                else
                {
                    Console.WriteLine($"Conta de: {contaLogada.Titular}");
                    MostrarMenu(opcoesConta);
                    int escolha = LerEscolhaUsuario();
                    if (escolha == 0) { contaLogada = null; logado = false; }
                    else if (escolha == 1) { contaLogada.Depositar(); }
                    else if (escolha == 2) { contaLogada.Sacar(); }
                    else if (escolha == 3) { contaLogada.Transferir(contas); }
                    else if (escolha == 4) { contaLogada.MostrarSaldo(); }
                }
            }
        }

        public static int EncontrarIndexConta(int contaNumero)
        {
            for (int i = 0; i < contas.Count; i++)
            {
                Conta conta = contas[i];
                if (conta.Numero == contaNumero) { return i; }
            }
            return -1;
        }

        public static void MostrarMenu(string[] menu)
        {
            MostrarSeparacao();
            for (int i = 0; i < menu.Length ;i++) { Console.WriteLine($"[ {i} ] - {menu[i]}"); }
            MostrarSeparacao();
        }

        public static void MostrarSeparacao() { Console.WriteLine("------------------------------"); }

        public static void AbrirConta()
        {
            Conta novaConta = CriarContaUsuario();
            contas.Add(novaConta);
            Console.WriteLine("Conta aberta com Sucesso.");
        }

        public static int GerarNumeroConta()
        {
            if (contas.Count == 0) { return 1; }
            else { return contas[contas.Count - 1].Numero + 1; }
        }
        public static Conta CriarContaUsuario()
        {
            
            string nome = LerEntradaString("nome do proprietario da conta");
            double saldo = LerEntradaDouble("saldo da conta");
            int numero = GerarNumeroConta();
            
            return new Conta(numero, nome, saldo);
        }

        public static int LerEntradaInt(string message)
        {
            bool entradaValida = false;
            while (!entradaValida)
            {
                Console.WriteLine($"Insira um valor para {message}:");
                if (int.TryParse(Console.ReadLine(), out int entradaUsuario)) { return entradaUsuario; }
            }
            return -1;
        }

        public static double LerEntradaDouble(string message)
        {
            bool entradaValida = false;
            while (!entradaValida)
            {
                Console.WriteLine($"Insira um valor para {message}:");
                if (double.TryParse(Console.ReadLine(), out double entradaUsuario)) { return entradaUsuario; }
            }
            return -1;
        }

        public static string LerEntradaString(string message)
        {
            bool entradaValida = false;
            while (!entradaValida)
            {
                Console.WriteLine($"Insira um valor para {message}:");
                string entradaUsuario = Console.ReadLine();

                if (!string.IsNullOrEmpty(entradaUsuario)) { return entradaUsuario; }
            }
            return string.Empty;
        }

        public static int LerEscolhaUsuario()
        {
            bool escolhaValida = false;
            while (!escolhaValida)
            {
                int escolha = LerEntradaInt("escolha");
                if (escolha < 0 && escolha >= contas.Count) { Console.WriteLine("Escolha inválida.");  }
                else { return escolha; }
            }
            return -1;
            
        }
        public static void ListarContas()
        {
            MostrarSeparacao();
            if (contas.Count == 0) { Console.WriteLine("Nenhuma Conta cadastrada."); }
            else 
            { 
                foreach (Conta conta in contas)
                {
                    Console.WriteLine(
                        $"Conta Número: {conta.Numero}\n" +
                        $"Titular: {conta.Titular}\n" +
                        $"Saldo Atual: {conta.Saldo}\n"
                    );
                }
            }
            MostrarSeparacao();
        }

        public static int LogarConta(int numeroConta)
        {
            for (int i = 0; i < contas.Count; i++)
            {
                if (contas[i].Numero == numeroConta)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
