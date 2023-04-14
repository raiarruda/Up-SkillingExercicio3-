/* sistema de gestão de entradas e saídas de um veiculo (estacionamento) */

internal class Program
{
    private static void Main(string[] args)
    {
        var clientes = new List<dynamic>();
        var veiculos = new List<dynamic>();
        var movimentacao = new List<dynamic>();
        double vlrPorMinuto = 0.15;

        while (true)
        {
            //Console.Clear();
            Console.WriteLine("Digite uma das opções abaixo:");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Cadastrar Veiculo");
            Console.WriteLine("4 - Listar Veiculos");
            Console.WriteLine("5 - Registrar entrada");
            Console.WriteLine("6 - Registrar saida");
            Console.WriteLine("7 - Exibir movimentacao");
            Console.WriteLine("0 - Sair");

            var opcao = Console.ReadLine();
            var sair = false;

            switch (opcao)
            {
                case "1":
                    //Console.Clear();

                    Console.WriteLine("Digite o nome do cliente");
                    var nome = Console.ReadLine();

                    Console.WriteLine("Digite o cpf do cliente");
                    var cpf = Console.ReadLine();

                    Guid id = Guid.NewGuid();

                    dynamic cliente = new
                    {
                        ID = id,
                        Nome = nome,
                        CPF = cpf
                    };

                    clientes.Add(cliente);

                    Console.WriteLine("Cliente cadastrado com sucesso ...");
                    Thread.Sleep(1000);
                break;

                case "2":
                    Console.WriteLine("===== Lista de clientes =====");
                    foreach (var cli in clientes)
                    {
                        Console.WriteLine($"ID: {cli.ID}");
                        Console.WriteLine($"Nome: {cli.Nome}");
                        Console.WriteLine($"CPF: {cli.CPF}");
                        Console.WriteLine("-----------------------------");
                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();

                break;

                case "3":
                    //Console.Clear();

                    Console.WriteLine("Digite a marca do veiculo");
                    var marca = Console.ReadLine();

                    Console.WriteLine("Digite o modelo do veiculo");
                    var modelo = Console.ReadLine();

                    Console.WriteLine("Digite a placa do veiculo");
                    var placa = Console.ReadLine();

                    Console.WriteLine("Digite o cpf do dono do veiculo: ");
                    string busca = Console.ReadLine() ?? "";
                    var idCliente = buscaIdCliente(busca);

                    dynamic veiculo = new
                    {
                        IDCliente = idCliente,
                        Marca = marca,
                        Modelo = modelo,
                        Placa = placa
                    };

                    veiculos.Add(veiculo);

                    Console.WriteLine("Veiculo cadastrado com sucesso ...");
                    Thread.Sleep(1000);
                break;

                case "4":
                    //Console.Clear();

                    Console.WriteLine("===== Lista de veiculos =====");
                    foreach (var vei in veiculos)
                    {
                        Console.WriteLine($"Marca: {vei.Marca}");
                        Console.WriteLine($"Modelo: {vei.Modelo}");
                        Console.WriteLine($"Placa: {vei.Placa}");
                        Console.WriteLine($"Cliente: {buscaNomeCliente(vei.IDCliente)}");
                        Console.WriteLine("-----------------------------");
                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();
                    
                break;

                case "5":
                    //Console.Clear();

                    Console.WriteLine("Informe o cpf do cliente: ");
                    var idc = buscaIdCliente(Console.ReadLine() ?? "");

                    Console.WriteLine("Informe a hora de entrada (HH:MM): ");
                    var he = Convert.ToDateTime(Console.ReadLine());

                    var hs = "";
                    var vf = "";

                    dynamic registro = new
                    {
                        IdCliente = idc,
                        horaEntrada = he,
                        horaSaida = hs,
                        valorFinal = vf
                    };

                    movimentacao.Add(registro);

                    Console.WriteLine("Entrada registrada ...");
                    Thread.Sleep(1000);
                break;

                case "6":
                    //Console.Clear();

                    Console.WriteLine("Informe o CPF do cliente: ");
                    string s = buscaIdCliente(Console.ReadLine() ?? "");

                    Console.WriteLine("Informe a hora da saida (HH:MM): ");
                    hs = Console.ReadLine();
                                        
                    for(int i = 0; i < movimentacao.Count; i++){
                        if (movimentacao[i].IdCliente == s && movimentacao[i].horaSaida == ""){
                            Console.WriteLine("entrou no if");
                            
                            movimentacao[i].GetType().GetProperty("horaSaida").SetValue(movimentacao[i], hs);

                            //movimentacao[i].horaSaida = hs;
                        }
                    }
                    
                    Console.WriteLine("Saida registrada ...");
                    Thread.Sleep(1000);
                break;

                case "7":
                    foreach (var reg in movimentacao)
                    {
                        Console.WriteLine($"""
                                Nro registro: {reg.idRegistro}
                                Cliente: {buscaNomeCliente(reg.IdCliente)}
                                Entrada: {reg.horaEntrada}
                                Saida: {reg.horaSaida}
                                Valor Final: {reg.valorFinal}
                                -----------------------------
                                """);
                    }

                    Console.WriteLine("Pressione Enter para continuar ...");
                    Console.ReadKey();
                
                break;

                default:
                    sair = true;
                break;
            }

            if (sair) break;
        }


        /* metodos */
        /* metodo pra buscar o id do cliente usando o cpf, pra vincular ao veiculo */
        string buscaIdCliente(string s)
        {
            bool achou = false;
            while (!achou)
            {
                for (int i = 0; i < clientes.Count; i++)
                {
                    if (clientes[i].CPF == s)
                    {
                        achou = true;
                        return Convert.ToString(clientes[i].ID);
                    }
                }
            }
            return "Cliente não existe!";
        }

        /* metodo pra buscar o nome do cliente usando o id, pra mostrar na lista de veiculos */
        string buscaNomeCliente(string s)
        {
            bool achou = false;
            while (!achou)
            {
                for (int i = 0; i < clientes.Count; i++)
                {
                    if (Convert.ToString(clientes[i].ID) == s)
                    {
                        achou = true;
                        return Convert.ToString(clientes[i].Nome);
                    }
                }
            }
            return "Cliente não existe!";
        }
    }
}
