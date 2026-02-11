using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque
{
    internal class Program
    {
        static string[] opcoes = { "Sair", "Adicionar Produto", "Remover Produto", "Atualizar Quantidade", "Listar Estoque" };
        static Dictionary<int, Produto> estoque = new Dictionary<int, Produto>();
        class Produto
        {
            private int id;
            private string name;
            private float preco;
            private int quantidade;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public float Preco
            {
                get { return preco; }
                set { preco = value; }
            }
            public int Quantidade
            {
                get { return quantidade; }
                set { quantidade = value; }
            }

            public Produto(int id, string name, float preco, int quantidade)
            {
                this.id = id;
                this.name = name;
                this.preco = preco;
                this.quantidade = quantidade;
            }

            public bool AumentarEstoque(int quantidadeAdicionada)
            {
                this.quantidade += quantidadeAdicionada;
                return true;
            }

            public bool DiminuirEstoque(int quantidadeRemovida)
            {
                if (this.quantidade < quantidadeRemovida) { return false; }
                else { this.quantidade -= quantidadeRemovida; return true; }
            }
        }
        static void Main(string[] args)
        {
            bool usandoSistema = true;
            while (usandoSistema)
            {
                MostrarMenu();
                int opcaoUsuario = LerValidarEscolhaUsuario();

                if (opcaoUsuario == 0) { usandoSistema = false; }
                if (opcaoUsuario == 1) { AdicionarNovoProduto(); }
                if (opcaoUsuario == 2) { RemoverProduto(); }
                if (opcaoUsuario == 3) { AtualizarQuantidadeProduto(); }
                if (opcaoUsuario == 4) { ListarProdutos(); }
            }
            Console.WriteLine("Saindo...");
            Console.ReadLine();
        }

        static void ListarProdutos()
        {
            MostrarSeparador();
            foreach (KeyValuePair<int, Produto> produto in estoque)
            {
                Console.WriteLine($"ID: {produto.Key}");
                Console.WriteLine($"Produto: {produto.Value.Name}");
                Console.WriteLine($"Preco: {produto.Value.Preco}");
                Console.WriteLine($"Quantidade no Estoque: {produto.Value.Quantidade}");
                MostrarSeparador();
            }
        }

        static void MostrarSeparador()
        {
            Console.WriteLine("-------------------------------");
        }

        static void AtualizarQuantidadeProduto()
        {
            bool sucesso = false;

            int produtoId = LerIntInput("Insira o id do produto");
            if (!VerificarExistenciaIdNoEstoque(produtoId)) { Console.WriteLine("Produto Inexistente"); return; }

            Produto produto = estoque[produtoId];
            while (!sucesso)
            {
                string userInput = LerStringInput("Insira se vendeu ou recebeu o produto [ + ] ou [ - ]");
                if (userInput != "+" && userInput != "-") { Console.WriteLine("Valor incorreto."); continue; }

                int quantidadeAlterada = LerIntInput("Insira a quantidade alterada");
                if (userInput == "+") { sucesso = produto.AumentarEstoque(quantidadeAlterada); }
                else if (userInput == "-") { sucesso = produto.DiminuirEstoque(quantidadeAlterada); }

                if (!sucesso) { Console.WriteLine("Não foi possivel alterar a quantidade do produto");  }
            }
            Console.WriteLine("Quantidade do Produto atualizado com sucesso.");
        }

        static bool VerificarExistenciaIdNoEstoque(int id)
        {
            try
            {
                Produto produto = estoque[id];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static void AdicionarNovoProdutoNoEstoque(Produto novoProduto, int id)
        {
            estoque.Add(id, novoProduto);
            Console.WriteLine($"Produto {novoProduto.Name} adicionado com sucesso!");
        }

        static void RemoverProduto()
        {
            int produtoId = LerIntInput("Insira o ID do produto a ser removido");
            if (RemoverProdutoEstoque(produtoId))
            {
                Console.WriteLine($"Produto com ID {produtoId} removido com sucesso.");
            }
            else
            {
                Console.WriteLine($"Não foi encontrado o produto com ID {produtoId}");
            }
        }

        static bool RemoverProdutoEstoque(int produtoId)
        {
            return estoque.Remove(produtoId);
        }

        static Produto CriarNovoProduto(int id, string name, float preco, int quantidade)
        {
            return new Produto(id, name, preco, quantidade);
        }

        static void AdicionarNovoProduto()
        {
            string name = LerStringInput("Insira o Nome do Produto");
            float preco = LerFloatInput("Insira o Preco do Produto");
            int quantidade = LerIntInput("Insira a Quantidade do Produto");
            int id = CriarNovoId();

            Produto novoProduto = CriarNovoProduto(id, name, preco, quantidade);
            AdicionarNovoProdutoNoEstoque(novoProduto, id);
        }

        static int CriarNovoId()
        {
            if (estoque.Count == 0) { return 1; }
            else { return estoque.Last().Key +1; }
        }

        static int ValidarEscolhaUsuario(string userInput)
        {
            if (int.TryParse(userInput, out int intInput))
            {
                if (intInput < opcoes.Length - 1 || intInput > 0)
                {
                    return intInput;
                }
            }
            Console.WriteLine("Opção inválida.");
            return -1;
        }

        static int LerValidarEscolhaUsuario()
        {
            bool opcaoValida = false;
            while(!opcaoValida)
            {
                string userInput = LerStringInput("Insira sua escolha");
                int userIntInput = ValidarEscolhaUsuario(userInput);
                if (userIntInput != -1) { return userIntInput; }
            }
            return -1;
        }

        static void MostrarMenu()
        {
            Console.WriteLine();
            for (int i = 0; i < opcoes.Length; i++)
            {
                Console.WriteLine($"{i} - {opcoes[i]}");
            }
            Console.WriteLine();
        }

        static int LerIntInput(string message)
        {
            bool valorValido = false;
            while (!valorValido)
            {
                Console.WriteLine($"\n{message}: ");
                if (int.TryParse(Console.ReadLine(), out int intInput))
                {
                    return intInput;
                }
                else
                {
                    Console.WriteLine("Valor inválido.");
                }
            }
            return -1;
        }

        static float LerFloatInput(string message)
        {
            bool valorValido = false;
            while (!valorValido)
            {
                Console.WriteLine($"\n{message}: ");
                if (float.TryParse(Console.ReadLine(), out float floatInput))
                {
                    return floatInput;
                }
                else
                {
                    Console.WriteLine("Valor inválido.");
                }
            }
            return -1;
        }

        static string LerStringInput(string message)
        {
            bool valorValido = false;
            while (!valorValido)
            {
                Console.WriteLine($"\n{message}: ");
                string userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Valor inválido.");
                }
            }
            return "";
        }
    }
}
