using SistemaEstacionamento.Model;

public class Program
{

    private static void Main(string[] args)
    {
        List<Cliente> clientes = new List<Cliente>();
        List<Veiculo> veiculos = new List<Veiculo>();
        List<Aluguel> alugueis = new List<Aluguel>();
        var relacaoCarroAlugado = new List<dynamic>();


        while (true)
        {
            Console.Clear();
            Console.WriteLine("Digite uma das opções abaixo:");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Cadastrar veículo");
            Console.WriteLine("3 - Listar Cliente");
            Console.WriteLine("4 - Listar veículo");
            Console.WriteLine("5 - Listar aluguel");
            Console.WriteLine("6 - Alugar");
            Console.WriteLine("7 - Sair");

            var opcao = Console.ReadLine();
            var sair = false;

            switch (opcao)
            {
                case "1":
                    Console.Clear();

                    Cliente cliente = new Cliente();
                    Console.WriteLine("Digite o nome do cliente");
                    var nome = Console.ReadLine();
                    Console.WriteLine("Digite o cpf do cliente");
                    var cpf = Console.ReadLine();

                    cliente = new Cliente
                    {
                        Id = new Guid(),
                        Nome = nome,
                        CPF = cpf
                    };

                    clientes.Add(cliente);

                    Console.WriteLine("Cliente cadastrado com sucesso ...");

                    Thread.Sleep(1000);
                    break;
                case "2":

                    Veiculo veiculo = new Veiculo();
                    Console.WriteLine("Digite a marca");
                    var marca = Console.ReadLine();
                    Console.WriteLine("Digite o modelo");
                    var modelo = Console.ReadLine();
                    Console.WriteLine("Digite a placa");
                    var placa = Console.ReadLine();

                    veiculo = new Veiculo
                    {
                        Marca = marca,
                        Modelo = modelo,
                        Placa = placa
                    };

                    veiculos.Add(veiculo);

                    Console.WriteLine("Veiculo cadastrado com sucesso ...");
                    Thread.Sleep(1000);

                    break;
                case "3":
                    Console.WriteLine("=== Lista de clientes =====");

                    foreach (var i in clientes)
                    {
                        Console.WriteLine($"Cliente: " + i.Nome);
                    }
                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;
                case "4":
                    Console.WriteLine("=== Lista de veiculos =====");

                    foreach (var i in veiculos)
                    {
                        Console.WriteLine($"Cliente: " + i.Modelo);
                    }
                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;
                case "5":
                    Console.WriteLine("=== Relação de carros alugados =====");

                    foreach (var i in alugueis)
                    {

                        dynamic CarroAlugado = new
                        {
                            Nome = clientes.Where(x => x.Id == i.Id_cliente).FirstOrDefault().Nome,
                            Placa = veiculos.Where(x => x.Id == i.Id_carro).FirstOrDefault().Placa,
                            Modelo = veiculos.Where(x => x.Id == i.Id_carro).FirstOrDefault().Modelo,
                            Horario = i.Horario
                        };

                        relacaoCarroAlugado.Add(CarroAlugado);

                    }
                    foreach (var i in relacaoCarroAlugado)
                    {
                        Console.WriteLine("Cliente: " + i.Nome);
                        Console.WriteLine("Placa: " + i.Placa);
                        Console.WriteLine("Modelo: " + i.Modelo);
                        Console.WriteLine("Horario do aluguel: " + i.Horario);

                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;
                case "6":
                    Console.WriteLine("Digite o cpf do cliente");
                    var cpfAluguel = Console.ReadLine();

                    Console.WriteLine("Digite a placa do carro");
                    var placaAlugel = Console.ReadLine();

                    var aluguel = new Aluguel
                    {
                        Id = new Guid(),
                        Id_cliente = clientes.Where(x=> x.CPF == cpfAluguel).Select(y=> y.Id).FirstOrDefault(),
                        Id_carro = veiculos.Where(x => x.Placa == placaAlugel).Select(y => y.Id).FirstOrDefault(),
                        Horario = DateTime.Now
                    };

                    alugueis.Add(aluguel);

                    Console.WriteLine("Veiculo cadastrado com sucesso ...");
                    Thread.Sleep(1000);

                    break;

                default:
                    sair = true;
                    break;
            }

            if (sair) break;
        }


    }

}