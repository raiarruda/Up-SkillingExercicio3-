using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaEstacionamento.Data;
using SistemaEstacionamento.Model;
using SistemaEstacionamento.Repository;
using SistemaEstacionamento.Service;

public class Program
{
    public static IEstacionamentoService _estacionamentoService;

    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        _estacionamentoService = host.Services.GetService<IEstacionamentoService>();


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
            Console.WriteLine("7 - Devolução");
            Console.WriteLine("8 - Sair");

            var opcao = Console.ReadLine();
            var sair = false;

            switch (opcao)
            {
                case "1":

                    _estacionamentoService.CadastrarCliente();
                    Console.WriteLine("Pressione para continuar.");
                    Console.ReadKey();
                    break;
                case "2":

                    _estacionamentoService.CadastrarVeiculo();
                    Console.WriteLine("Pressione para continuar.");
                    Console.ReadKey();

                    break;
                case "3":
                    Console.WriteLine("=== Lista de clientes =====");

                   var clientes =  _estacionamentoService.ListaClientes();
                    foreach (var i in clientes)
                    {
                        Console.WriteLine($"Cliente: " + i.Nome);
                    }
                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;
                case "4":
                    Console.WriteLine("=== Lista de veiculos =====");

                    var veiculos = _estacionamentoService.ListVeiculos();
                    foreach (var veiculo in veiculos)
                    {
                        Console.WriteLine($@"
                            Cliente = {_estacionamentoService.BuscaClientePorId(veiculo.Id_cliente).Nome}
                            Marca = {veiculo.Marca}
                            Modelo  = {veiculo.Modelo}
                            Placa  = {veiculo.Placa}
                        ");
                    }
                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;
                case "5":
                    Console.WriteLine("=== Relação de carros alugados =====");

                    //foreach (var i in alugueis)
                    //{

                    //    dynamic CarroAlugado = new
                    //    {
                    //        Nome = clientes.Where(x => x.Id == i.Id_cliente).FirstOrDefault().Nome,
                    //        Placa = veiculos.Where(x => x.Id == i.Id_carro).FirstOrDefault().Placa,
                    //        Modelo = veiculos.Where(x => x.Id == i.Id_carro).FirstOrDefault().Modelo,
                    //        Horario = i.HorarioEntrada
                    //    };

                    //    relacaoCarroAlugado.Add(CarroAlugado);

                    //}
                    //foreach (var i in relacaoCarroAlugado)
                    //{
                    //    Console.WriteLine("Cliente: " + i.Nome);
                    //    Console.WriteLine("Placa: " + i.Placa);
                    //    Console.WriteLine("Modelo: " + i.Modelo);
                    //    Console.WriteLine("Horario do aluguel: " + i.Horario);

                    //}

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                    break;
                case "6":
                    Console.WriteLine("Digite o cpf do cliente");
                    var cpfAluguel = Console.ReadLine();

                    Console.WriteLine("Digite a placa do carro");
                    var placaAlugel = Console.ReadLine();

                    //var aluguel = new EntradaSaida
                    //{
                    //    Id = new Guid(),
                    //    Id_cliente = clientes.Where(x => x.CPF == cpfAluguel).Select(y => y.Id).FirstOrDefault(),
                    //    Id_carro = veiculos.Where(x => x.Placa == placaAlugel).Select(y => y.Id).FirstOrDefault(),
                    //    HorarioEntrada = DateTime.Now
                    //};

                    //alugueis.Add(aluguel);

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

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            // Configuração da injeção de dependência.
            services.AddSingleton<IDatabaseConfig, DatabaseConfig>();
            services.AddTransient<IEstacionamentoRepository, EstacionamentoRepository>();
            services.AddTransient<IEstacionamentoService, EstacionamentoService>();
        });
}








