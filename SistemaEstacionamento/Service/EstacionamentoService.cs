using SistemaEstacionamento.Model;
using SistemaEstacionamento.Repository;

namespace SistemaEstacionamento.Service
{
    public interface IEstacionamentoService
    {
        Cliente BuscaClientePorId(int id);
        public void CadastrarVeiculo();
        public void CadastrarCliente();
        List<Veiculo> ListVeiculos();
        public List<Cliente> ListaClientes();
        public Cliente BuscaClientePorCpf(string cpf);
    }

    public class EstacionamentoService: IEstacionamentoService
    {
        //private readonly EstacionamentoRepository _estacionamentoRepository;
        private readonly IEstacionamentoRepository _repository;

        public EstacionamentoService(IEstacionamentoRepository repository)
        {
            _repository = repository;
        }

        public void CadastrarVeiculo()
        {
            Console.Clear();

            Veiculo veiculo = new Veiculo();

            Console.WriteLine("Digite o cpf do cliente");
            var cpf = Console.ReadLine();

            Console.WriteLine("Digite a marca");
            var marca = Console.ReadLine();

            Console.WriteLine("Digite o modelo");
            var modelo = Console.ReadLine();

            Console.WriteLine("Digite a placa");
            var placa = Console.ReadLine();

            var cliente = BuscaClientePorCpf(cpf);

            veiculo = new Veiculo
            {
                Id_cliente = cliente.Id,
                Marca = marca,
                Modelo = modelo,
                Placa = placa
            };

            _repository.InserirVeiculo(veiculo);
            Console.WriteLine("Veiculo cadastrado com sucesso ...");
        }

        public Cliente BuscaClientePorId(int id)
        {
           return _repository.BuscaClientePorId(id);
        }
        public void CadastrarCliente()
        {
            Console.Clear();

            Cliente cliente = new Cliente();

            Console.WriteLine("Digite o nome do cliente");
            var nome = Console.ReadLine();
            Console.WriteLine("Digite o cpf do cliente");
            var cpf = Console.ReadLine();

            cliente = new Cliente
            {
                Nome = nome,
                CPF = cpf
            };

            _repository.InserirCliente(cliente);
            Console.WriteLine("Cliente cadastrado com sucesso ...");
        }

        public List<Cliente> ListaClientes()
        {
            return _repository.ListaClientes();
        }

        public Cliente BuscaClientePorCpf(string cpf)
        {
            return _repository.GetByCpf(cpf);
        }

        public List<Veiculo> ListVeiculos() 
        {
            return _repository.ListVeiculos();
        }
    }

}
