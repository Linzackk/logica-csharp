using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroAlunos
{
    internal class Program
    {
        /*
         * Criar classe Aluno
         *      Nome, Idade, Nota
         * Armazenar em lista
         *
         * Sistema permite:
         *      Cadastrar aluno
         *      Listar alunos
         *      Média das notas
         *      Buscar aluno pelo nome
         *      Sair
         */
        static string[] opcoes = { "Sair", "Cadastrar", "Listar Alunos", "Média Notas", "Buscar por Nome" };
        static List<Aluno> alunos = new List<Aluno>();
        
        class Aluno
        {
            private int id;
            private string name;
            private int age;
            private float nota;
            public string Name
            {
                get => name;
                set => name = value;
            }

            public int Id
            {
                get => id;
            }

            public int Age
            {
                get => age;
                set => age = value;
            }

            public float Nota
            {
                get => nota;
                set => nota = value;
            }

            public Aluno(int id, string name, int age, float nota)
            {
                this.id = id;
                this.name = name;
                this.age = age;
                this.nota = nota;
            }

            public override string ToString()
            {
                return $"\nId: {this.id}\nNome: {this.name}\nIdade: {this.age}\nNota: {this.nota}";
            }
        }
        static void Main(string[] args)
        {
            bool useSystem = true;

            while (useSystem) 
            {
                ShowMenu();
                int userOption = GetUserOption();
                
                if (userOption == 0) 
                { 
                    Console.WriteLine("Saindo..."); 
                    useSystem = false; 
                }
                if (userOption == 1) 
                {
                    CadastrarAluno();
                }
                if (userOption == 2)
                {
                    ListarAlunos();
                }
                if (userOption == 3)
                {
                    CalcularMediaAlunos();
                }
                if (userOption == 4)
                {
                    Aluno aluno = BuscarAlunoPorNome(ReadStringInput("nome"));
                    if (aluno.Id < 0) { Console.WriteLine("Aluno Não encontrado."); }
                    else { Console.WriteLine(aluno);  }
                }
            }
            
        }
        static void ShowMenu()
        {
            Console.WriteLine();
            for  (int i = 0; i < opcoes.Length; i++)
            {
                Console.WriteLine($"{i} - {opcoes[i]}");
            }
            Console.WriteLine();
        }
        
        static int GetUserOption()
        {
            bool validOption = false;
            while (!validOption)
            {
                Console.WriteLine("Insira sua escolha");
                string option = Console.ReadLine();

                if (int.TryParse(option, out int optionOut))
                {
                    if (optionOut >= 0 && optionOut < opcoes.Length)
                    {
                        return optionOut;
                    }
                }
                Console.WriteLine("Escolha inválida.");
            }
            return -1;
        }
        static void CadastrarAluno()
        {
            int id = createId();
            string name = ReadStringInput("nome");
            int idade = ReadIntInput("idade");
            float nota = ReadFloatInput("nota");

            Aluno novoAluno = new Aluno(id, name, idade, nota);
            alunos.Add(novoAluno);
            Console.WriteLine("Novo aluno Criado: ");
            Console.WriteLine(novoAluno);
        }

        static int ReadIntInput(string message)
        {
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine($"Insira um valor para {message}");
                if (int.TryParse(Console.ReadLine(), out int userInput)) { return userInput; }
                Console.WriteLine("Valor Inválido, tente novamente.");
            }
            return -1;
        }

        static string ReadStringInput(string message)
        {
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine($"Insira um valor para {message}");
                string userInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(userInput)) { return userInput; }
                Console.WriteLine("Valor Inválido, tente novamente.");
            }
            return "";
        }

        static float ReadFloatInput(string message)
        {
            bool validInput = false;
            while (!validInput)
            {
                    Console.WriteLine($"Insira um valor para {message}");
                if (float.TryParse(Console.ReadLine(), out float userInput)) 
                { 
                    if (userInput >= 0f && userInput <= 10f ) { return userInput; }
                }
                Console.WriteLine("Valor Inválido, tente novamente.");
            }
            return -1;
        }

        static int createId()
        {
            if (alunos.Count  == 0) { return 1; }
            else { return alunos[alunos.Count -1].Id; }
        }

        static void ListarAlunos()
        {
            foreach (Aluno aluno in alunos)
            {
                Console.WriteLine($"{aluno}\n");
            }
        }

        static void CalcularMediaAlunos()
        {
            float totalSum = 0;
            foreach (Aluno aluno in alunos)
            {
                totalSum += aluno.Nota;
            }
            Console.WriteLine($"A Média dos alunos é de: {totalSum / alunos.Count}");
        }

        static Aluno BuscarAlunoPorNome(string name)
        {
            foreach (Aluno aluno in alunos)
            {
                if (aluno.Name.Equals(name))
                {
                    return aluno;
                }
            }
            return new Aluno(-1, "not found", -1, -1);
        }
    }
}
