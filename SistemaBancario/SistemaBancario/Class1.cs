using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class Conta
    {
        private int numero;
        public int Numero
        {
            get => numero;
            set => numero = value;
        }

        private string titular;
        public string Titular
        {
            get => titular;
        }

        private double saldo;
        public double Saldo
        {
            get => saldo;
            set => saldo = value;
        }

        public Conta(int numero, string titular, double saldo)
        {
            this.numero = numero;
            this.titular = titular;
            this.saldo = saldo;
        }

        public Conta(int numero, string titular)
        {
            this.numero = numero;
            this.titular = titular;
        }

        public void Transferir(List<Conta> contas)
        {
            double valorDaTransferencia = Program.LerEntradaDouble("Valor da Transferencia");
            int numeroContaParaTransferir = Program.LerEntradaInt("Numero da Conta");

            if (Saldo < valorDaTransferencia) { Console.WriteLine("Sua conta não possui o valor para transação."); return; }
            
            int index = Program.EncontrarIndexConta(numeroContaParaTransferir);
            if (index == -1) { Console.WriteLine("Conta alvo inexistente."); return; }

            Saldo -= valorDaTransferencia;
            contas[index].Saldo += valorDaTransferencia;
        }

        public void Depositar()
        {
            double valorDoDeposito = Program.LerEntradaDouble("Valor do Deposito");
            if (valorDoDeposito <= 0) { Console.WriteLine("Não é permitido depositar numeros negativos ou nada"); return; }
            else { Saldo += valorDoDeposito; }
        }

        public void Sacar()
        {
            double valorDoSaque = Program.LerEntradaDouble("Valor do Saque");
            if (valorDoSaque <= 0 || valorDoSaque > Saldo) { Console.WriteLine("Não foi possivel realizar o saque."); return; }
            else { Saldo -= valorDoSaque; }
        }

        public void MostrarSaldo()
        {
            Program.MostrarSeparacao();
            Console.WriteLine($"Saldo: {Saldo}");
            Program.MostrarSeparacao();
        }
    }
}
